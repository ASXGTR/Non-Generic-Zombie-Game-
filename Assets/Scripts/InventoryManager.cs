using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public List<Item> allItems = new List<Item>();

        /// <summary>
        /// Attempts to transfer an item to the given slot type.
        /// </summary>
        public bool TryTransferItem(Item item, SlotType targetSlot)
        {
            if (item == null)
            {
                Debug.LogWarning("⚠️ Tried to transfer a null item.");
                return false;
            }

            if (!CanPlaceInSlot(item, targetSlot))
            {
                Debug.LogWarning($"❌ Cannot place item '{item.itemName}' in slot '{targetSlot}'");
                return false;
            }

            Debug.Log($"✅ Item '{item.itemName}' successfully transferred to '{targetSlot}'");
            return true;
        }

        /// <summary>
        /// Validates if a given item can legally be placed in a specific slot type.
        /// </summary>
        public bool CanPlaceInSlot(Item item, SlotType slotType)
        {
            if (item == null)
                return false;

            switch (item.itemType)
            {
                case ItemType.Handheld:
                    return slotType == SlotType.Utility;
                case ItemType.Consumable:
                    return slotType == SlotType.Food;
                case ItemType.Clothing:
                    return slotType == SlotType.Clothing;
                case ItemType.Holster:
                    return slotType == SlotType.Holster;
                case ItemType.Bag:
                    return slotType == SlotType.General;
            }

            // 🚫 Anti-nesting safeguard
            if (IsContainer(item) &&
                (slotType == SlotType.General || slotType == SlotType.Clothing))
            {
                Debug.LogWarning($"🛑 Blocked nesting container item '{item.itemName}' in gear slot.");
                return false;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the item is a container (based on flags or naming).
        /// </summary>
        private bool IsContainer(Item item)
        {
            if (item == null) return false;
            if (item.isContainer) return true;
            if (item.storageCapacity > 0) return true;

            string name = item.itemName?.ToLower();
            return name != null &&
                   (name.Contains("bag") || name.Contains("backpack") || name.Contains("pouch"));
        }

        /// <summary>
        /// Calculates total weight of the provided item list.
        /// </summary>
        public float GetTotalWeight(List<Item> items)
        {
            float total = 0f;
            foreach (var item in items)
            {
                total += item != null && item.weight > 0f ? item.weight : 1f;
            }
            return total;
        }

        /// <summary>
        /// Checks whether current weight exceeds allowed capacity.
        /// </summary>
        public bool IsOverCapacity(float currentWeight, float maxCapacity)
        {
            return currentWeight > maxCapacity;
        }

        /// <summary>
        /// Returns all items that are valid for the specified slot type.
        /// </summary>
        public List<Item> GetItemsBySlotType(SlotType slotType)
        {
            List<Item> filtered = new List<Item>();
            foreach (Item item in allItems)
            {
                if (CanPlaceInSlot(item, slotType))
                    filtered.Add(item);
            }
            return filtered;
        }

        /// <summary>
        /// Registers an item into the global item pool.
        /// </summary>
        public void RegisterItem(Item item)
        {
            if (item != null && !allItems.Contains(item))
            {
                allItems.Add(item);
                Debug.Log($"👜 Registered item: {item.itemName}");
            }
        }

        /// <summary>
        /// Removes item from global pool.
        /// </summary>
        public void RemoveItem(Item item)
        {
            if (item != null && allItems.Contains(item))
            {
                allItems.Remove(item);
                Debug.Log($"🗑 Removed item: {item.itemName}");
            }
        }
    }
}
