using Core.Shared.Models;
﻿// File: Assets/Scripts/Inventory/InventoryManager.cs
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private List<StackSlot> inventoryGrid = new();

        public void AddItem(InventoryItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.ItemName)) return;

            var existingSlot = inventoryGrid.FirstOrDefault(slot => slot.item != null && slot.item.ItemName == item.ItemName);
            if (existingSlot != null && item.IsStackable)
            {
                existingSlot.Add(1);
                Debug.Log($"[InventoryManager] 🔁 Stacked item: {item.ItemName} ➕ Quantity now: {existingSlot.quantity}");
                return;
            }

            var emptySlot = inventoryGrid.FirstOrDefault(slot => slot.IsEmpty);
            if (emptySlot != null)
            {
                emptySlot.item = item;
                emptySlot.quantity = 1;
                Debug.Log($"[InventoryManager] ✅ Item added: {item.ItemName}");
            }
            else
            {
                Debug.LogWarning("[InventoryManager] ⚠️ No empty inventory slots available.");
            }
        }

        public void RemoveItem(string itemName)
        {
            var slot = inventoryGrid.FirstOrDefault(s => s.item != null && s.item.ItemName == itemName);
            if (slot != null)
            {
                slot.item = null;
                slot.quantity = 0;
                Debug.Log($"[InventoryManager] 🗑️ Item removed: {itemName}");
            }
            else
            {
                Debug.LogWarning($"[InventoryManager] ❌ Item not found: {itemName}");
            }
        }

        public bool HasItem(string itemName) =>
            inventoryGrid.Any(slot => slot.item != null && slot.item.ItemName == itemName);

        public InventoryItem GetItem(string itemName) =>
            inventoryGrid.FirstOrDefault(slot => slot.item != null && slot.item.ItemName == itemName)?.item;

        public List<InventoryItem> GetAllItems() =>
            inventoryGrid.Where(slot => slot.item != null).Select(slot => slot.item).ToList();

        public int CountItemByName(string itemName) =>
            inventoryGrid.Where(slot => slot.item != null && slot.item.ItemName == itemName).Sum(slot => slot.quantity);

        public void RemoveItemsByName(string itemName, int quantity)
        {
            var remaining = quantity;
            foreach (var slot in inventoryGrid.Where(s => s.item != null && s.item.ItemName == itemName))
            {
                if (remaining <= 0) break;

                if (slot.quantity <= remaining)
                {
                    remaining -= slot.quantity;
                    slot.item = null;
                    slot.quantity = 0;
                }
                else
                {
                    slot.quantity -= remaining;
                    remaining = 0;
                }
            }

            Debug.Log($"[InventoryManager] 🧹 Removed {quantity - remaining}x '{itemName}'");
        }
    }
}
