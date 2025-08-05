using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Inventory;

public class SlotPanelManager : MonoBehaviour
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

    void Awake()
    {
        inventory = Object.FindFirstObjectByType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("[SlotPanelManager] Inventory not found.");
            return;
        }

        RegisterSlotButtons();
        SyncIconsFromEquippedItems();
    }

    void RegisterSlotButtons()
    {
        foreach (SlotReference slot in equipmentSlots)
        {
            GameObject found = GameObject.Find("Slot_" + slot.slotName);
            if (found == null) continue;

            slot.slotButton = found.GetComponent<Button>();
            slot.iconImage = found.GetComponentInChildren<Image>();

            if (slot.slotButton != null)
            {
                string capturedSlot = slot.slotName;
                slot.slotButton.onClick.AddListener(() => OnSlotClicked(capturedSlot));
            }
        }
    }

    void OnSlotClicked(string slotName)
    {
        Item heldItem = inventory.rightHand ?? inventory.leftHand;
        if (heldItem == null || !IsItemValidForSlot(heldItem, slotName)) return;

        if (inventory.EquipItem(heldItem))
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

    void UpdateSlotIcon(string slotName, Item item)
    {
        SlotReference slotRef = equipmentSlots.Find(s => s.slotName.Equals(slotName, System.StringComparison.OrdinalIgnoreCase));
        if (slotRef == null || slotRef.iconImage == null) return;

        Sprite iconToUse = item.EquippedSprite ?? item.icon ?? defaultIcon;
        slotRef.iconImage.sprite = iconToUse;
        slotRef.iconImage.enabled = true;
    }

    void SyncIconsFromEquippedItems()
    {
        foreach (var gear in inventory.GetEquippedGear())
        {
            if (gear?.data == null) continue;
            string slot = gear.clothingSlot.ToString().ToLower();
            UpdateSlotIcon(slot, gear);
        }
    }
}
