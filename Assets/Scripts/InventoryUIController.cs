using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;
using Game.UI;

public class InventoryUIController : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent;
    public GameObject slotPrefab;

    [Header("Inventory Reference")]
    public InventoryManager inventoryManager;
    public InventoryUIManager uiManager;

    public void Populate(List<Item> items)
    {
        ClearSlots();

        foreach (var item in items)
        {
            GameObject slotGO = Instantiate(slotPrefab, contentParent);
            ItemSlotUI slot = slotGO.GetComponent<ItemSlotUI>();
            if (slot != null)
                slot.SetItem(item);
        }
    }

    public void RegenerateFromInventory()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryUIController: Missing InventoryManager reference.");
            return;
        }

        ClearSlots();

        List<Item> storedItems = inventoryManager.allItems;
        int capacity = storedItems.Count;

        for (int i = 0; i < capacity; i++)
        {
            GameObject slotGO = Instantiate(slotPrefab, contentParent);
            ItemSlotUI slot = slotGO.GetComponent<ItemSlotUI>();
            if (slot != null)
                slot.SetItem(storedItems[i]);
        }

        Debug.Log($"🔧 UI regenerated with {capacity} slots.");
    }

    private void ClearSlots()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);
    }
}
