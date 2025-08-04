using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    /// <summary>
    /// Global inventory logic for runtime validation and item registry.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        [Header("Runtime Inventory Registry")]
        public List<InventoryItem> allItems = new();

        public bool TryTransferItem(InventoryItem item, SlotType targetSlot)
        {
            if (item == null)
            {
                Debug.LogWarning("[InventoryManager] ⚠️ Tried to transfer a null item.");
                return false;
            }

            if (!CanPlaceInSlot(item, targetSlot))
            {
                Debug.LogWarning($"[InventoryManager] ❌ Cannot place '{item.ItemName}' into '{targetSlot}'");
                return false;
            }

            if (IsItemRegistered(item))
            {
                Debug.LogWarning($"[InventoryManager] ⛔ Item '{item.ItemName}' already registered.");
                return false;
            }

            allItems.Add(item);
            Debug.Log($"[InventoryManager] ✅ Transferred '{item.ItemName}' to '{targetSlot}'");
            return true;
        }

        public bool CanPlaceInSlot(InventoryItem item, SlotType slotType)
        {
            if (item == null)
                return false;

            if (item.IsContainer() &&
                (slotType == SlotType.Clothing || slotType == SlotType.General))
            {
                Debug.LogWarning($"[InventoryManager] 🛑 Blocked container nesting: '{item.ItemName}'");
                return false;
            }

            return ValidateSlotCompatibility(item.itemType, slotType);
        }

        private bool ValidateSlotCompatibility(ItemType itemType, SlotType slotType)
        {
            return itemType switch
            {
                ItemType.Handheld => slotType == SlotType.Utility,
                ItemType.Consumable => slotType == SlotType.Food,
                ItemType.Clothing => slotType == SlotType.Clothing,
                ItemType.Holster => slotType == SlotType.Holster,
                ItemType.Bag => slotType == SlotType.General,
                _ => false,
            };
        }

        public bool IsItemRegistered(InventoryItem item) => item != null && allItems.Contains(item);

        public void ClearInventory()
        {
            allItems.Clear();
            Debug.Log("[InventoryManager] 🧹 Inventory cleared.");
        }
    }
}
