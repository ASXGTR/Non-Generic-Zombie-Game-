// File: Assets/Scripts/Inventory/EquipmentManager.cs
using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;

namespace Game.Inventory
{
    public class EquipmentManager : MonoBehaviour
    {
        [Header("Equipment Slots")]
        public Dictionary<string, InventoryItem> equippedItems = new();

        public delegate void EquipmentEvent(InventoryItem item);
        public event EquipmentEvent OnEquip;
        public event EquipmentEvent OnUnequip;

        public void EquipItem(string slotName, InventoryItem item)
        {
            if (item == null || string.IsNullOrEmpty(slotName)) return;

            equippedItems[slotName] = item;
            OnEquip?.Invoke(item);
            Debug.Log($"Equipped {item.ItemName} to {slotName}");
        }

        public void UnequipItem(string slotName)
        {
            if (!equippedItems.ContainsKey(slotName)) return;

            var removed = equippedItems[slotName];
            equippedItems.Remove(slotName);
            OnUnequip?.Invoke(removed);
            Debug.Log($"Unequipped {removed.ItemName} from {slotName}");
        }

        public InventoryItem GetEquippedItem(string slotName)
        {
            equippedItems.TryGetValue(slotName, out var item);
            return item;
        }

        public bool IsEquipped(string itemName)
        {
            foreach (var kvp in equippedItems)
            {
                if (kvp.Value != null && kvp.Value.ItemName == itemName)
                    return true;
            }
            return false;
        }

        public Dictionary<string, InventoryItem> GetAllEquippedItems() =>
            new(equippedItems);
    }
}
