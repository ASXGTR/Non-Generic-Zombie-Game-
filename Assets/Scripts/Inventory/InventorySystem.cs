using Core.Shared.Enums;
using Core.Shared.Models;
using Game.Inventory;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

namespace Game.Inventory
{
    /// <summary>
    /// Global inventory service for stacking, slot compatibility, and item registry.
    /// </summary>
    [AddComponentMenu("Inventory/Inventory System")]
    public class InventorySystem : MonoBehaviour
    {
        [Header("System Settings")]
        [Tooltip("Maximum number of slots available in inventory")]
        [SerializeField] private int maxSlots = 20;

        [Tooltip("Current stacked items across inventory")]
        [SerializeField] private List<InventorySlot> slots = new();

        public bool AddItem(InventoryItem item, int amount)
        {
            if (item == null)
            {
                Debug.LogWarning("[InventorySystem] ⚠️ Tried to add null item.");
                return false;
            }

            if (!CanPlaceInSlot(item, item.assignedSlotType))
            {
                Debug.LogWarning($"[InventorySystem] ❌ Cannot place '{item.ItemName}' in '{item.assignedSlotType}'");
                return false;
            }

            if (IsItemRegistered(item))
            {
                Debug.LogWarning($"[InventorySystem] ⛔ Item '{item.ItemName}' already exists.");
                return false;
            }

            foreach (var slot in slots)
            {
                if (slot.item == item && slot.quantity < item.maxStack)
                {
                    slot.Add(amount);
                    Debug.Log($"[InventorySystem] ➕ Stacked '{item.ItemName}' x{amount}");
                    return true;
                }
            }

            if (slots.Count < maxSlots)
            {
                slots.Add(new InventorySlot { item = item, quantity = amount });
                Debug.Log($"[InventorySystem] ✅ Added new '{item.ItemName}' x{amount}");
                return true;
            }

            Debug.LogWarning("[InventorySystem] ❌ Inventory full.");
            return false;
        }

        public bool TryTransferItem(InventoryItem item, SlotType targetSlot)
        {
            if (item == null)
            {
                Debug.LogWarning("[InventorySystem] ⚠️ Tried to transfer null item.");
                return false;
            }

            if (!CanPlaceInSlot(item, targetSlot))
            {
                Debug.LogWarning($"[InventorySystem] ❌ Transfer blocked: '{item.ItemName}' → '{targetSlot}'");
                return false;
            }

            if (IsItemRegistered(item))
            {
                Debug.LogWarning($"[InventorySystem] ⛔ Item '{item.ItemName}' already in inventory.");
                return false;
            }

            slots.Add(new InventorySlot { item = item, quantity = 1 });
            Debug.Log($"[InventorySystem] 🔄 Transferred '{item.ItemName}' to '{targetSlot}'");
            return true;
        }

        public bool CanPlaceInSlot(InventoryItem item, SlotType slotType)
        {
            if (item == null) return false;

            if (item.IsContainer() && (slotType == SlotType.Clothing || slotType == SlotType.General))
            {
                Debug.LogWarning($"[InventorySystem] 🛑 Blocked nested container: '{item.ItemName}'");
                return false;
            }

            return ValidateSlotCompatibility(item.itemType, slotType);
        }

        private bool ValidateSlotCompatibility(ItemTypeEnum itemType, SlotType slotType) => itemType switch
        {
            ItemTypeEnum.Handheld => slotType == SlotType.Utility,
            ItemTypeEnum.Consumable => slotType == SlotType.Food,
            ItemTypeEnum.Clothing => slotType == SlotType.Clothing,
            ItemTypeEnum.Holster => slotType == SlotType.Holster,
            ItemTypeEnum.Bag => slotType == SlotType.General,
            _ => false,
        };

        public bool IsItemRegistered(InventoryItem item) =>
            item != null && slots.Exists(s => s.item == item);

        public void ClearInventory()
        {
            slots.Clear();
            Debug.Log("[InventorySystem] 🧹 Inventory wiped.");
        }
    }
}
