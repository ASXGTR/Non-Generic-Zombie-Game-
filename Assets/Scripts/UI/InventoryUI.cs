using Core.Shared.Models;
using Game.Inventory;
using Game.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform SlotContainer;
    public GameObject SlotPrefab;
    public GameObject SelectionFrame;

    private List<InventoryUISlot> activeSlots = new();
    private int selectedIndex = 0;

    void Start()
    {
        RefreshUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveSelection(1);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveSelection(-1);
    }

    public void RefreshUI()
    {
        ClearSlots();

        var items = InventoryManager.Instance.GetAllItems();
        foreach (var item in items)
        {
            GameObject slotGO = Instantiate(SlotPrefab, SlotContainer);
            InventoryUISlot slot = slotGO.GetComponent<InventoryUISlot>();
            slot.Setup(item, () => UseItem(item), () => DropItem(item));
            activeSlots.Add(slot);
        }

        UpdateSelectionFrame();
    }

    private void ClearSlots()
    {
        foreach (Transform child in SlotContainer)
        {
            Destroy(child.gameObject);
        }
        activeSlots.Clear();
    }

    private void MoveSelection(int direction)
    {
        selectedIndex = Mathf.Clamp(selectedIndex + direction, 0, activeSlots.Count - 1);
        UpdateSelectionFrame();
    }

    private void UpdateSelectionFrame()
    {
        if (activeSlots.Count == 0) return;
        SelectionFrame.transform.position = activeSlots[selectedIndex].transform.position;
    }

    private void UseItem(ItemInstance item)
    {
        if (item.Data != null && item.Data.HasTag("Consumable"))
        {
            InventoryManager.Instance.RemoveItem(item.ItemName);
            RefreshUI();
        }
    }

    private void DropItem(ItemInstance item)
    {
        InventoryManager.Instance.RemoveItem(item.ItemName);
        RefreshUI();
    }
}
