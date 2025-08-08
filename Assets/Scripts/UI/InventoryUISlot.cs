using Core.Shared.Models;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryUISlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Button useButton;
    [SerializeField] private Button dropButton;

    private ItemInstance item;

    /// <summary>Sets the item and updates visuals.</summary>
    public void SetItem(ItemInstance newItem, int quantity = 1)
    {
        item = newItem;

        if (item != null && item.Data != null)
        {
            icon.sprite = item.Data.icon;
            icon.enabled = true;
            quantityText.text = quantity > 1 ? quantity.ToString() : "";
        }
        else
        {
            ClearSlot();
        }
    }

    /// <summary>Initializes the slot with item and action callbacks.</summary>
    public void Setup(ItemInstance newItem, System.Action onUse, System.Action onDrop)
    {
        SetItem(newItem);

        if (useButton != null)
        {
            useButton.onClick.RemoveAllListeners();
            useButton.onClick.AddListener(() => onUse?.Invoke());
        }

        if (dropButton != null)
        {
            dropButton.onClick.RemoveAllListeners();
            dropButton.onClick.AddListener(() => onDrop?.Invoke());
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";

        if (useButton != null) useButton.onClick.RemoveAllListeners();
        if (dropButton != null) dropButton.onClick.RemoveAllListeners();
    }

    public ItemInstance GetItem() => item;
}
