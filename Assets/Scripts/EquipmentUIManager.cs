using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

/// <summary>
/// Manages equipment UI buttons and icon visuals based on inventory slots.
/// </summary>
public class EquipmentUIManager : MonoBehaviour
{
    [System.Serializable]
    public class SlotReference
    {
        public string slotName;
        public Button slotButton;
        public Image iconImage;
    }

    [Header("Equipment Slots")]
    public List<SlotReference> equipmentSlots = new();

    [Header("Default Icon")]
    public Sprite defaultIcon;

    private Inventory inventory;

    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("[EquipmentUIManager] Inventory not found.");
            return;
        }

        RegisterSlotButtons();
        SyncIconsFromEquippedItems();
        InitializeStoragePanels();
    }

    void RegisterSlotButtons()
    {
        foreach (SlotReference slot in equipmentSlots)
        {
            string buttonObjectName = "Slot_" + slot.slotName;
            GameObject found = GameObject.Find(buttonObjectName);

            if (found == null)
            {
                Debug.LogWarning($"[EquipmentUIManager] Slot button '{buttonObjectName}' not found.");
                continue;
            }

            slot.slotButton = found.GetComponent<Button>();
            if (slot.slotButton != null)
            {
                string capturedSlot = slot.slotName;
                slot.slotButton.onClick.AddListener(() => OnSlotClicked(capturedSlot));
                Debug.Log($"[EquipmentUIManager] Registered slot button: {capturedSlot}");
            }

            slot.iconImage = found.GetComponentInChildren<Image>();
        }
    }

    void OnSlotClicked(string slotName)
    {
        Debug.Log($"[EquipmentUIManager] Slot clicked: {slotName}");

        Item heldItem = inventory.rightHand ?? inventory.leftHand;
        if (heldItem == null)
        {
            Debug.Log("[EquipmentUIManager] No item in hand.");
            return;
        }

        if (!IsItemValidForSlot(heldItem, slotName))
        {
            Debug.Log($"[EquipmentUIManager] Item '{heldItem.itemName}' invalid for slot '{slotName}'.");
            return;
        }

        if (EquipToSlot(slotName, heldItem))
            UpdateSlotIcon(slotName, heldItem);
    }

    bool IsItemValidForSlot(Item item, string slotName)
    {
        string lowerSlot = slotName.ToLower();

        return item.itemType switch
        {
            ItemType.Clothing => item.clothingSlot.ToString().ToLower() == lowerSlot,
            ItemType.Bag => lowerSlot == "backpack",
            ItemType.Holster => lowerSlot == "holster",
            _ => false
        };
    }

    bool EquipToSlot(string slotName, Item item)
    {
        bool equipped = inventory.EquipItem(item);
        Debug.Log(equipped
            ? $"[EquipmentUIManager] Equipped '{item.itemName}' to {slotName}."
            : $"[EquipmentUIManager] Failed to equip '{item.itemName}' to {slotName}.");

        return equipped;
    }

    void UpdateSlotIcon(string slotName, Item item)
    {
        SlotReference slotRef = equipmentSlots.Find(s => s.slotName.Equals(slotName, System.StringComparison.OrdinalIgnoreCase));
        if (slotRef == null || slotRef.iconImage == null)
        {
            Debug.LogWarning($"[EquipmentUIManager] Slot icon update failed for '{slotName}'.");
            return;
        }

        Sprite iconToUse = item.EquippedSprite ?? item.icon ?? defaultIcon;
        slotRef.iconImage.sprite = iconToUse;
        slotRef.iconImage.enabled = true;
    }

    void SyncIconsFromEquippedItems()
    {
        foreach (var gear in inventory.GetEquippedGear())
        {
            if (gear == null || gear.data == null) continue;

            string slot = gear.clothingSlot.ToString().ToLower();
            UpdateSlotIcon(slot, gear);
            Debug.Log($"[EquipmentUIManager] Synced icon for equipped: {gear.itemName}");
        }
    }

    void InitializeStoragePanels()
    {
        foreach (var gear in inventory.GetEquippedGear())
        {
            if (gear == null || !gear.isContainer || gear.storageCapacity <= 0) continue;

            Debug.Log($"[EquipmentUIManager] Initializing storage for '{gear.itemName}' with {gear.storageCapacity} slots.");
            // TODO: Instantiate HUD container prefab via manager or panel spawner
        }
    }

    public int GetTotalStorageCapacity()
    {
        int total = 0;
        foreach (var gear in inventory.GetEquippedGear())
        {
            if (gear != null)
                total += gear.storageCapacity;
        }
        return total;
    }
}
