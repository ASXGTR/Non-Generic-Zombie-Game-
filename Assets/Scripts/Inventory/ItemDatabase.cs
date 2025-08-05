using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemDatabase : MonoBehaviour
{
    [Header("📦 Assign all your ItemData ScriptableObjects here")]
    public List<ItemData> allItemDataAssets = new();

    [HideInInspector] public List<ItemData> allItems = new();

    private void Awake()
    {
        RefreshDatabase();
    }

    /// <summary>
    /// Syncs runtime-accessible list with editor-assigned assets.
    /// </summary>
    public void RefreshDatabase()
    {
        allItems = allItemDataAssets.FindAll(i => i != null);
    }

    /// <summary>
    /// Get item by display name.
    /// </summary>
    public Item GetItemByName(string itemName)
    {
        var data = allItems.Find(i => i.ItemName == itemName);
        if (data != null) return new Item(data);

        Debug.LogWarning($"⚠️ Item '{itemName}' not found.");
        return null;
    }

    /// <summary>
    /// Get item by unique ID string.
    /// </summary>
    public Item GetItemByID(string id)
    {
        var data = allItems.Find(i => i.Id == id);
        if (data != null) return new Item(data);

        Debug.LogWarning($"⚠️ Item with ID '{id}' not found.");
        return null;
    }

    /// <summary>
    /// Try safe fetch by name using 'out' pattern.
    /// </summary>
    public bool TryGetItemByName(string itemName, out Item item)
    {
        item = null;
        var data = allItems.Find(i => i.ItemName == itemName);
        if (data != null)
        {
            item = new Item(data);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Return all item names.
    /// </summary>
    public List<string> GetAllItemNames()
    {
        List<string> names = new();
        foreach (var data in allItems)
            names.Add(data.ItemName);
        return names;
    }

    /// <summary>
    /// Get all items matching a rarity level.
    /// </summary>
    public List<ItemData> GetItemsByRarity(Rarity rarity)
    {
        return allItems.FindAll(i => i.rarity == rarity);
    }

    /// <summary>
    /// Get all items that contain a specific tag.
    /// </summary>
    public List<ItemData> GetItemsWithTag(string tag)
    {
        return allItems.FindAll(i => i.HasTag(tag));
    }

    /// <summary>
    /// Print a basic summary of all registered items.
    /// </summary>
    public void PrintItemSummary()
    {
        foreach (var data in allItems)
        {
            string summary = $"🧾 {data.ItemName} | Type: {data.itemType} | Weight: {data.weight} | Storage: {data.storageCapacity}";
            Debug.Log(summary);
        }
    }
}
