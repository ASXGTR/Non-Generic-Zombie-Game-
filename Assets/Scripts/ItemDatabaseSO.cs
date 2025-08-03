using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;  // Added namespace for ItemData, enums, etc.

[CreateAssetMenu(fileName = "ItemDatabaseSO", menuName = "Inventory/Item Database")]
public class ItemDatabaseSO : ScriptableObject
{
    public List<ItemData> allItems = new List<ItemData>();

    /// <summary>
    /// Retrieves an item by its unique numeric ID.
    /// </summary>
    public ItemData FindItemById(int id)
    {
        return allItems.Find(item => item.id == id.ToString());
    }

    /// <summary>
    /// Retrieves an item by its name.
    /// </summary>
    public ItemData FindItemByName(string name)
    {
        return allItems.Find(item => item.itemName.Equals(name, System.StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Retrieves all items of a specific type.
    /// </summary>
    public List<ItemData> FindItemsByType(ItemType type)
    {
        return allItems.FindAll(item => item.itemType == type);
    }

    /// <summary>
    /// Retrieves all items that have a specific tag.
    /// </summary>
    public List<ItemData> FindItemsByTag(string tag)
    {
        return allItems.FindAll(item => item.tags.Contains(tag));
    }
}
