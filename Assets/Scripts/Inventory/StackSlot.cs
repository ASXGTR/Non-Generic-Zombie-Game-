// File: Assets/Scripts/Inventory/StackSlot.cs

using Core.Shared.Models;
using UnityEngine;

namespace Game.Inventory
{
    /// <summary>
    /// Serializable stack slot for item and quantity tracking.
    /// Enforces stack limits from ItemData.
    /// </summary>
    [System.Serializable]
    public class StackSlot
    {
        [SerializeField] public ItemInstance item;
        [SerializeField] public int quantity;

        /// <summary>Returns true if the slot is empty or quantity is zero.</summary>
        public bool IsEmpty => item == null || quantity <= 0;

        /// <summary>Returns true if the item is stackable.</summary>
        public bool IsStackable => item != null && item.IsStackable;

        /// <summary>Returns the max stack size from item data.</summary>
        public int MaxStackSize => item?.Data?.MaxStackSize ?? 1;

        /// <summary>Synchronizes the item's internal quantity with the slot quantity.</summary>
        public void SyncQuantity()
        {
            if (item != null)
                item.Quantity = quantity;
        }

        /// <summary>Adds quantity to the slot, respecting max stack size.</summary>
        public void Add(int amount)
        {
            if (item == null || amount <= 0) return;

            quantity = Mathf.Min(quantity + amount, MaxStackSize);
            SyncQuantity();
        }

        /// <summary>Removes quantity from the slot. Clears if quantity hits zero.</summary>
        public void Remove(int amount)
        {
            if (item == null || amount <= 0) return;

            quantity = Mathf.Max(0, quantity - amount);
            if (quantity == 0) Clear();
            else SyncQuantity();
        }

        /// <summary>Sets a new item and quantity, enforcing stack limit.</summary>
        public void Set(ItemInstance newItem, int newQuantity)
        {
            item = newItem;
            quantity = Mathf.Clamp(newQuantity, 0, MaxStackSize);
            SyncQuantity();
        }

        /// <summary>Clears the slot completely.</summary>
        public void Clear()
        {
            item = null;
            quantity = 0;
        }

        public override string ToString() =>
            IsEmpty ? "Empty Slot" : $"{item.ItemName} x{quantity}";
    }
}
