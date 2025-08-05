using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    // 🔒 Deprecated InventoryItem.cs
    // Merged into modular InventoryItem.cs on 2025-08-05
    // Retained for reference only. Do not use in runtime.

    /// <summary>
    /// Represents a modular item container with optional internal storage, transfer rules, and stat traits.
    /// </summary>
    public class InventoryItem : MonoBehaviour
    {
        [Header("Item Metadata")]
        public string ItemName;
        public Sprite icon;
        public ItemType itemType;
        public string category;
        public float weight = 1f;
        public bool isContainer;
        public int storageCapacity;

        [Header("Data Reference")]
        public ItemData data; // 🔧 ItemData source for tooltip, icon, tags, etc.

        [Header("Container Contents")]
        private readonly List<InventoryItem> internalStorage = new();

        /// <summary>
        /// Returns tooltip text for this item.
        /// </summary>
        public string TooltipText
        {
            get
            {
                string typeInfo = itemType == ItemType.None ? "Unknown Type" : itemType.ToString();
                string storageNote = IsContainer()
                    ? $"Container ({internalStorage.Count}/{storageCapacity})"
                    : "Single Item";
                return $"{ItemName}\nType: {typeInfo}\nCategory: {category}\nWeight: {GetTotalWeight():0.##}\n{storageNote}";
            }
        }

        /// <summary>
        /// Copies data from ItemData and populates this instance.
        /// </summary>
        public void LoadFromData(ItemData source)
        {
            if (source == null)
            {
                Debug.LogWarning("[InventoryItem] LoadFromData called with null reference.");
                return;
            }

            data = source;

            ItemName = source.ItemName;
            icon = source.icon;
            itemType = source.itemType;
            category = source.category;
            weight = source.weight;
            isContainer = source.isContainer;
            storageCapacity = source.storageCapacity;
        }

        /// <summary>
        /// Returns true if item can be stored in another container.
        /// </summary>
        public bool CanBeStored() => internalStorage.Count == 0;

        /// <summary>
        /// Returns total weight of this item, including contents.
        /// </summary>
        public float GetTotalWeight()
        {
            float total = weight > 0f ? weight : 1f;

            foreach (var item in internalStorage)
                total += item != null ? item.GetTotalWeight() : 1f;

            return total;
        }

        /// <summary>
        /// Checks whether this item behaves as a container.
        /// </summary>
        public bool IsContainer()
        {
            if (isContainer || storageCapacity > 0)
                return true;

            string name = ItemName != null ? ItemName.ToLowerInvariant() : string.Empty;
            return name.Contains("bag") || name.Contains("backpack") || name.Contains("pouch");
        }

        /// <summary>
        /// Returns internal storage list.
        /// </summary>
        public List<InventoryItem> GetInternalStorage() => internalStorage;
    }
}
