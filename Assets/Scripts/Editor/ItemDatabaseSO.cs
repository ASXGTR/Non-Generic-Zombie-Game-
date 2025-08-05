using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database")]
    public class ItemDatabaseSO : ScriptableObject
    {
        [Header("📦 Assign all your ItemData assets")]
        public List<ItemData> allItems = new();

        /// <summary>
        /// Find item by display name.
        /// </summary>
        public ItemData GetByName(string name)
        {
            var result = allItems.Find(i => i != null && i.ItemName == name);
            if (result == null)
                Debug.LogWarning($"[ItemDatabaseSO] ❌ Item with name '{name}' not found.");
            return result;
        }

        /// <summary>
        /// Find item by unique ID string.
        /// </summary>
        public ItemData GetByID(string id)
        {
            var result = allItems.Find(i => i != null && i.Id == id);
            if (result == null)
                Debug.LogWarning($"[ItemDatabaseSO] ❌ Item with ID '{id}' not found.");
            return result;
        }

        /// <summary>
        /// Check if item name exists in database.
        /// </summary>
        public bool Contains(string name)
        {
            return allItems.Exists(i => i != null && i.ItemName == name);
        }

        /// <summary>
        /// Get items tagged with a specific keyword.
        /// </summary>
        public List<ItemData> GetItemsWithTag(string tag)
        {
            return allItems.FindAll(i => i != null && i.HasTag(tag));
        }

        /// <summary>
        /// Refresh item list (for editor tooling).
        /// </summary>
        public void ValidateDatabase()
        {
            int nulls = allItems.RemoveAll(i => i == null);
            Debug.Log($"[ItemDatabaseSO] 🧹 Removed {nulls} null entries. Valid items: {allItems.Count}");
        }

        /// <summary>
        /// Print a summary of item types and IDs.
        /// </summary>
        public void PrintSummary()
        {
            foreach (var item in allItems)
            {
                if (item == null) continue;
                Debug.Log($"📄 {item.ItemName} | ID: {item.Id} | Type: {item.itemType} | Weight: {item.weight}");
            }
        }
    }
}
