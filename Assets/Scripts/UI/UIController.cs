using Core.Shared.Models;
using Game.Inventory;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Inventory UI")]
    public GameObject InventoryPanel;
    public Transform SlotContainer;
    public GameObject SlotPrefab;

    private bool isInventoryVisible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryVisible = !isInventoryVisible;
        InventoryPanel.SetActive(isInventoryVisible);

        if (isInventoryVisible)
        {
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in SlotContainer)
        {
            Destroy(child.gameObject);
        }

        var items = InventoryManager.Instance.GetAllItems(); // Should return List<ItemInstance>
        foreach (var item in items)
        {
            GameObject slotGO = Instantiate(SlotPrefab, SlotContainer);
            InventoryUISlot slot = slotGO.GetComponent<InventoryUISlot>();
            slot.Setup(item, () => UseItem(item), () => DropItem(item));
        }
    }

    private void UseItem(ItemInstance item)
    {
        if (item != null && item.HasTag("Consumable"))
        {
            InventoryManager.Instance.RemoveItem(item.Data.ItemName);
            UpdateInventoryUI();
        }
    }

    private void DropItem(ItemInstance item)
    {
        if (item?.Data != null)
        {
            InventoryManager.Instance.RemoveItem(item.Data.ItemName);
            UpdateInventoryUI();
        }
    }
}
