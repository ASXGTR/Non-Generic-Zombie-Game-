// File: Assets/Scripts/Inventory/InventorySystem.cs

using Core.Shared;
using Core.Shared.Enums;
using Core.Shared.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [AddComponentMenu("Inventory/Inventory System")]
    public class InventorySystem : MonoBehaviour
    {
        [Header("System Settings")]
        [Tooltip("Maximum number of slots available in inventory")]
        [SerializeField] private int maxSlots = 20;

        [Tooltip("Current stacked items across inventory")]
        [SerializeField] private List<StackSlot> slots = new();

        public bool AddItem(ItemInstance item, int amount)
        {
            if (item == null || item.Data == null)
            {
                Debug.LogWarning("[InventorySystem] ⚠️ Tried to add null item.");
                return false;
            }

            var itemName = item.Data.ItemName;

            if (!CanPlaceInSlot(item, item.Data.slotTypes?.Count > 0 && item.Data.slotTypes[0] is ItemSlot s ? s : default))
            {
                Debug.LogWarning($"[InventorySystem] ❌ Cannot place '{itemName}' in assigned slot.");
                return false;
            }

            if (IsItemRegistered(item))
            {
                Debug.LogWarning($"[InventorySystem] ⛔ Item '{itemName}' already exists.");
                return false;
            }

            foreach (var slot in slots)
            {
                if (slot.item == item && slot.quantity < item.Data.MaxStackSize)
                {
                    slot.Add(amount);
                    Debug.Log($"[InventorySystem] ➕ Stacked '{itemName}' x{amount}");
                    return true;
                }
            }

            if (slots.Count < maxSlots)
            {
                slots.Add(new StackSlot { item = item, quantity = amount });
                Debug.Log($"[InventorySystem] ✅ Added new '{itemName}' x{amount}");
                return true;
            }

            Debug.LogWarning("[InventorySystem] ❌ Inventory full.");
            return false;
        }

        public bool TryTransferItem(ItemInstance item, ItemSlot targetSlot)
        {
            if (item == null || item.Data == null)
            {
                Debug.LogWarning("[InventorySystem] ⚠️ Tried to transfer null item.");
                return false;
            }

            var itemName = item.Data.ItemName;

            if (!CanPlaceInSlot(item, targetSlot))
            {
                Debug.LogWarning($"[InventorySystem] ❌ Transfer blocked: '{itemName}' → '{targetSlot}'");
                return false;
            }

            if (IsItemRegistered(item))
            {
                Debug.LogWarning($"[InventorySystem] ⛔ Item '{itemName}' already in inventory.");
                return false;
            }

            slots.Add(new StackSlot { item = item, quantity = 1 });
            Debug.Log($"[InventorySystem] 🔄 Transferred '{itemName}' to '{targetSlot}'");
            return true;
        }

        public bool CanPlaceInSlot(ItemInstance item, ItemSlot slotType)
        {
            if (item == null || item.Data == null) return false;

            var itemName = item.Data.ItemName;

            if (item.IsContainer && (slotType == ItemSlot.Pocket || slotType == ItemSlot.Back))
            {
                Debug.LogWarning($"[InventorySystem] 🛑 Blocked nested container: '{itemName}'");
                return false;
            }

            return ValidateSlotCompatibility(item.Data.Type, slotType);
        }

        private bool ValidateSlotCompatibility(ItemTypeEnum itemType, ItemSlot slotType) => itemType switch
        {
            ItemTypeEnum.Handheld => slotType == ItemSlot.Hands,
            ItemTypeEnum.Clothing => slotType is ItemSlot.Head or ItemSlot.Body or ItemSlot.Legs or ItemSlot.Feet,
            ItemTypeEnum.Bag => slotType == ItemSlot.Back,
            ItemTypeEnum.Consumable => slotType == ItemSlot.Pocket,
            _ => false,
        };

        public bool IsItemRegistered(ItemInstance item) =>
            item != null && slots.Exists(s => s.item == item);

        public void ClearInventory()
        {
            slots.Clear();
            Debug.Log("[InventorySystem] 🧹 Inventory wiped.");
        }
    }
}
