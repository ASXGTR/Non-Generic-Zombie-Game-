// File: Assets/Scripts/Core/Shared/InventorySlot.cs

using Core.Shared.Enums;
using Core.Shared.Models;

namespace Core.Shared
{
    /// <summary>
    /// Represents a gear slot with an assigned item.
    /// Wraps ItemSlot enum with item instance and occupancy logic.
    /// </summary>
    [System.Serializable]
    public class InventorySlot
    {
        public ItemSlot slotType;
        public ItemInstance item;

        public bool isOccupied => item != null;

        public InventorySlot(ItemSlot type)
        {
            slotType = type;
            item = null;
        }

        public void Assign(ItemInstance newItem)
        {
            item = newItem;
        }

        public void Clear()
        {
            item = null;
        }
    }
}
