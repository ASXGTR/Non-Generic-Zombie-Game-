// InventoryUIController.cs
using Game.Inventory;
using Game.UI;
using InventorySystem;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < storedItems.Count; i++)
        {
            GameObject slotGO = Instantiate(slotPrefab, contentParent);
            ItemSlotUI slot = slotGO.GetComponent<ItemSlotUI>();
            if (slot != null)
                slot.SetItem(storedItems[i]);
        }

        Debug.Log($"🔧 UI regenerated with {storedItems.Count} slots.");
    }

    private void ClearSlots()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);
    }
}
