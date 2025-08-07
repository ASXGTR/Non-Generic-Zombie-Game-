using Core.Shared.Enums;
using Core.Shared.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemDatabase : MonoBehaviour
    {
        [Header("📦 Assign all your ItemData ScriptableObjects here")]
        public List<ItemData> allItemDataAssets = new();

        [HideInInspector] public List<ItemData> allItems = new();

        private void Awake()
        {
            RefreshDatabase();
        }

        public void RefreshDatabase()
        {
            allItems = allItemDataAssets.FindAll(i => i != null);
        }

        public ItemData GetItemByName(string itemName)
        {
            var data = allItems.Find(i => i.ItemName == itemName);
            if (data != null) return data;

            Debug.LogWarning($"⚠️ Item '{itemName}' not found.");
            return null;
        }

        public ItemData GetItemByID(string id)
        {
            var data = allItems.Find(i => i.Id == id);
            if (data != null) return data;

            Debug.LogWarning($"⚠️ Item with ID '{id}' not found.");
            return null;
        }

        public bool TryGetItemByName(string itemName, out ItemData item)
        {
            item = allItems.Find(i => i.ItemName == itemName);
            return item != null;
        }

        public List<string> GetAllItemNames()
        {
            List<string> names = new();
            foreach (var data in allItems)
                names.Add(data.ItemName);
            return names;
        }

        public List<ItemData> GetItemsByRarity(ItemRarity rarity)
        {
            return allItems.FindAll(i => i.Rarity == rarity);
        }

        public List<ItemData> GetItemsWithTag(string tag)
        {
            return allItems.FindAll(i => i.HasTag(tag));
        }

        public void PrintItemSummary()
        {
            foreach (var data in allItems)
            {
                string summary = $"🧾 {data.ItemName} | Type: {data.Type} | Weight: {data.Weight} | Storage: {data.StorageCapacity}";
                Debug.Log(summary);
            }
        }
    }
}
