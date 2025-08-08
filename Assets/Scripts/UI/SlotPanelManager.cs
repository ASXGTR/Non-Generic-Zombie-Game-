using Core.Shared.Models;
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

    private EquipmentManager equipmentManager;

    void Awake()
    {
        equipmentManager = Object.FindFirstObjectByType<EquipmentManager>();
        if (equipmentManager == null)
        {
            Debug.LogError("[SlotPanelManager] EquipmentManager not found.");
            return;
        }

        RegisterSlotButtons();
        bool success = SyncIconsFromEquippedItems(); // ✅ Fix for CS0029
        if (!success)
            Debug.LogWarning("[SlotPanelManager] One or more icons failed to sync.");
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
        ItemInstance heldItem = GetHeldItem(); // Replace with your actual logic
        if (heldItem == null || !IsItemValidForSlot(heldItem, slotName)) return;

        equipmentManager.EquipItem(slotName, heldItem); // ✅ No longer treated as bool

        bool updated = UpdateSlotIcon(slotName, heldItem);
        if (!updated)
            Debug.LogWarning($"[SlotPanelManager] Failed to update icon for slot: {slotName}");
    }

    bool IsItemValidForSlot(ItemInstance item, string slotName)
    {
        if (item.Data == null || item.Data.slotTypes == null) return false;
        return item.Data.slotTypes.Exists(s => s.ToString().ToLower() == slotName.ToLower());
    }

    bool UpdateSlotIcon(string slotName, ItemInstance item)
    {
        SlotReference slotRef = equipmentSlots.Find(s => s.slotName.Equals(slotName, System.StringComparison.OrdinalIgnoreCase));
        if (slotRef == null || slotRef.iconImage == null || item.Data == null) return false;

        Sprite iconToUse = item.Data.equippedSprite ?? item.Data.icon ?? defaultIcon;
        slotRef.iconImage.sprite = iconToUse;
        slotRef.iconImage.enabled = true;
        return true;
    }

    bool SyncIconsFromEquippedItems()
    {
        bool allSuccessful = true;

        foreach (var kvp in equipmentManager.GetAllEquippedItems())
        {
            var gear = kvp.Value;
            if (gear?.Data == null) continue;

            bool updated = UpdateSlotIcon(kvp.Key, gear);
            if (!updated)
            {
                Debug.LogWarning($"[SlotPanelManager] Failed to sync icon for slot: {kvp.Key}");
                allSuccessful = false;
            }
        }

        return allSuccessful;
    }

    ItemInstance GetHeldItem()
    {
        // Placeholder: replace with your actual logic to get the item the player is holding
        return null;
    }
}
