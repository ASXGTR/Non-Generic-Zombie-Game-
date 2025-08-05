using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    /// <summary>
    /// Represents a live inventory item instance with runtime traits, prefab logic, and internal storage.
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

        [Header("Item Source")]
        [SerializeField] private InventoryItemData itemData;

        [Header("Container Logic")]
        [SerializeField] private Transform storageAnchor;
        [SerializeField] private List<InventoryItem> internalStorage = new();

        /// <summary>
        /// Returns the tooltip for UI hover display.
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
        /// Assigns item data from ScriptableObject and populates instance fields.
        /// </summary>
        public void LoadFromData(InventoryItemData source)
        {
            if (source == null)
            {
                Debug.LogWarning("[InventoryItem] LoadFromData called with null reference.");
                return;
            }

            itemData = source;

            ItemName = source.ItemName;
            icon = source.icon;
            itemType = source.itemType;
            category = source.category;
            weight = source.weight;
            isContainer = source.isContainer;
            storageCapacity = source.storageCapacity;
        }

        /// <summary>
        /// Returns true if this item can be moved into other containers.
        /// </summary>
        public bool CanBeStored() => internalStorage.Count == 0;

        /// <summary>
        /// Recursively calculates weight including contained items.
        /// </summary>
        public float GetTotalWeight()
        {
            float total = weight > 0f ? weight : 1f;

            foreach (InventoryItem item in internalStorage)
                total += item != null ? item.GetTotalWeight() : 1f;

            return total;
        }

        /// <summary>
        /// Identifies container items based on tag, name, or capacity.
        /// </summary>
        public bool IsContainer()
        {
            if (isContainer || storageCapacity > 0)
                return true;

            string name = ItemName != null ? ItemName.ToLowerInvariant() : string.Empty;
            return name.Contains("bag") || name.Contains("box") || name.Contains("backpack") || name.Contains("pouch");
        }

        /// <summary>
        /// Returns internal storage list.
        /// </summary>
        public List<InventoryItem> GetInternalStorage() => internalStorage;

        /// <summary>
        /// Optional: Adds item to internal container.
        /// </summary>
        public bool TryStoreItem(InventoryItem item)
        {
            if (!IsContainer())
            {
                Debug.LogWarning($"[InventoryItem] '{ItemName}' is not a container.");
                return false;
            }

            if (internalStorage.Count >= storageCapacity)
            {
                Debug.LogWarning($"[InventoryItem] Container full: {internalStorage.Count}/{storageCapacity}");
                return false;
            }

            internalStorage.Add(item);
            if (storageAnchor != null)
            {
                item.transform.SetParent(storageAnchor, false);
                item.transform.localPosition = Vector3.zero;
            }

            return true;
        }

        /// <summary>
        /// Removes an item from internal storage.
        /// </summary>
        public bool RemoveStoredItem(InventoryItem item)
        {
            return internalStorage.Remove(item);
        }
    }
}
