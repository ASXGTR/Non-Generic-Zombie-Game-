// File: Assets/Scripts/Core/Shared/Models/ItemInstance.cs

using Core.Shared.Enums;
using System.Collections.Generic;

namespace Core.Shared.Models
{
    /// <summary>
    /// Runtime instance of an item, wrapping static ItemData with dynamic state.
    /// </summary>
    public class ItemInstance
    {
        /// <summary>
        /// Reference to the static item definition.
        /// </summary>
        public ItemData Data;

        /// <summary>
        /// Whether the item is currently equipped.
        /// </summary>
        public bool IsEquipped;

        /// <summary>
        /// Runtime internal storage for container items.
        /// </summary>
        private readonly List<ItemInstance> internalStorage = new();
        public IReadOnlyList<ItemInstance> InternalStorage => internalStorage;

        // ──────────────────────────────────────
        // 🔎 Accessors for static data
        // ──────────────────────────────────────

        public string ItemName => Data?.ItemName ?? "Unnamed";
        public int StorageCapacity => Data?.StorageCapacity ?? 0;
        public ItemTypeEnum Type => Data?.Type ?? default;
        public bool IsContainer => Data?.isContainer ?? false;
        public bool HasStorage => IsContainer && internalStorage.Count < StorageCapacity;
        public bool IsStackable => Data?.IsStackable ?? false;

        public List<ItemSlot> SlotTypes => Data?.slotTypes ?? new();

        /// <summary>
        /// Adds an item to internal storage if there's capacity.
        /// </summary>
        public bool TryAddToStorage(ItemInstance item)
        {
            if (!HasStorage || item == null) return false;
            internalStorage.Add(item);
            return true;
        }

        /// <summary>
        /// Ensures internal storage is initialized (noop here since it's readonly).
        /// Included for semantic clarity.
        /// </summary>
        public void EnsureStorageInitialized()
        {
            // Storage is always initialized via readonly field.
            // This method exists for semantic parity with older code.
        }

        /// <summary>
        /// Returns a readable string representation of the item.
        /// </summary>
        public override string ToString() =>
            $"{ItemName} {(IsEquipped ? "[Equipped]" : "")}";
    }
}
