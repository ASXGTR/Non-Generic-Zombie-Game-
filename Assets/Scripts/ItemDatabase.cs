using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;

public class ItemDatabase : MonoBehaviour
{
    [Header("Assign all your ItemData ScriptableObjects here")]
    public List<ItemData> allItemDataAssets = new List<ItemData>();

    // 🔍 This field makes it accessible to external editor tools
    [HideInInspector] public List<ItemData> allItems = new List<ItemData>();

    private void Awake()
    {
        // Sync hidden allItems list with visible one for runtime/editor use
        allItems = allItemDataAssets;
    }

    /// <summary>
    /// Get runtime Item by display name.
    /// </summary>
    public Item GetItemByName(string itemName)
    {
        ItemData data = allItemDataAssets.Find(i => i.itemName == itemName);
        if (data != null)
            return new Item(data);

        Debug.LogWarning($"⚠️ ItemDatabase: Item '{itemName}' not found!");
        return null;
    }

    /// <summary>
    /// Try to get item safely with 'out' pattern.
    /// </summary>
    public bool TryGetItemByName(string itemName, out Item item)
    {
        item = null;
        ItemData data = allItemDataAssets.Find(i => i.itemName == itemName);
        if (data != null)
        {
            item = new Item(data);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Get item by unique ID string.
    /// </summary>
    public Item GetItemByID(string id)
    {
        ItemData data = allItemDataAssets.Find(i => i.id == id);
        if (data != null)
            return new Item(data);

        Debug.LogWarning($"⚠️ ItemDatabase: Item with ID '{id}' not found.");
        return null;
    }

    /// <summary>
    /// Get all item names in database.
    /// </summary>
    public List<string> GetAllItemNames()
    {
        List<string> names = new List<string>();
        foreach (var data in allItemDataAssets)
            names.Add(data.itemName);
        return names;
    }

    /// <summary>
    /// Quick debug output showing item name + type + weight.
    /// </summary>
    public void PrintItemSummary()
    {
        foreach (var data in allItemDataAssets)
        {
            string summary = $"🧾 {data.itemName} | Type: {data.itemType} | Weight: {data.weight} | Storage: {data.storageCapacity}";
            Debug.Log(summary);
        }
    }
}
