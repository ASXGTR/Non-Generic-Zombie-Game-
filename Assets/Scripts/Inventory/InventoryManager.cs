using Game.Inventory;
using Game.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> inventoryGrid = new List<InventorySlot>();

    public void AddItem(InventoryItem item)
    {
        if (item == null || HasItem(item.name)) return;

        InventorySlot emptySlot = inventoryGrid.FirstOrDefault(slot => slot.Item == null);
        if (emptySlot != null)
        {
            emptySlot.AssignItem(item);
            Debug.Log($"Item added: {item.name}");
        }
        else
        {
            Debug.LogWarning("No empty inventory slots available.");
        }
    }

    public void RemoveItem(string itemName)
    {
        InventorySlot slot = inventoryGrid.FirstOrDefault(s => s.Item != null && s.Item.name == itemName);
        if (slot != null)
        {
            slot.ClearSlot();
            Debug.Log($"Item removed: {itemName}");
        }
        else
        {
            Debug.LogWarning($"Item not found: {itemName}");
        }
    }

    public bool HasItem(string itemName)
    {
        return inventoryGrid.Any(slot => slot.Item != null && slot.Item.name == itemName);
    }

    public InventoryItem GetItem(string itemName)
    {
        return inventoryGrid
            .Where(slot => slot.Item != null && slot.Item.name == itemName)
            .Select(slot => slot.Item)
            .FirstOrDefault();
    }

    public List<InventoryItem> GetAllItems()
    {
        return inventoryGrid
            .Where(slot => slot.Item != null)
            .Select(slot => slot.Item)
            .ToList();
    }
}
