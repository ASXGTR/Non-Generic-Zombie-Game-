using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Game.Inventory;

public class GearSlot : MonoBehaviour, IDropHandler
{
    [Header("Slot Type")]
    public ClothingSlot slotType; // Set in Inspector: Chest, Pants, etc.

    [Header("UI References")]
    public Image icon;
    public TMP_Text itemNameText;
    public GameObject conditionBar; // Optional, can add slider later

    private Item equippedItem;

    public void SetItem(Item item)
    {
        equippedItem = item;

        if (item != null && item.data != null)
        {
            if (icon != null)
            {
                icon.sprite = item.data.Icon;
                icon.enabled = true;
            }

            if (itemNameText != null)
                itemNameText.text = item.data.itemName;

            if (conditionBar != null)
                conditionBar.SetActive(true); // Add fill logic later
        }
        else
        {
            if (icon != null)
            {
                icon.sprite = null;
                icon.enabled = false;
            }

            if (itemNameText != null)
                itemNameText.text = "";

            if (conditionBar != null)
                conditionBar.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggedItem = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (draggedItem != null && draggedItem.item.data != null &&
            draggedItem.item.data.clothingSlot == slotType)
        {
            SetItem(draggedItem.item);

            // TODO: Equip in Inventory
            // e.g. inventory.EquipClothing(draggedItem.item);
        }
    }

    public ClothingSlot GetSlotType()
    {
        return slotType;
    }
}
