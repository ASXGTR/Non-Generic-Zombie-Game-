// 🔒 Deprecated GearSlot.cs
// Logic modularized into GearSlotUI.cs and supporting systems on 2025-08-05
// Retained for reference only. Do not use in runtime.

using Game.Inventory;
using Game.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

/// <summary>
/// UI slot for gear. Handles drag-drop and hover tooltip.
/// </summary>
public class GearSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Slot Type")]
    public ClothingSlot slotType;

    [Header("UI References")]
    public Image icon;
    public TMP_Text itemNameText;
    public GameObject conditionBar;

    private Item equippedItem;

    /// <summary>
    /// Sets the slot’s visuals and stores item reference.
    /// </summary>
    public void SetItem(Item item)
    {
        equippedItem = item;
        bool valid = item != null && item.data != null;

        if (icon != null)
        {
            icon.sprite = valid ? item.data.Icon : null;
            icon.enabled = valid;
        }

        if (itemNameText != null)
            itemNameText.text = valid ? item.data.itemName : "";

        if (conditionBar != null)
            conditionBar.SetActive(valid); // Hook durability later

        // 🔁 Sync main UI (optional, to harmonize visuals across slots)
        EquipmentUIManager eqUI = FindFirstObjectByType<EquipmentUIManager>();
        if (eqUI != null)
            eqUI.UpdateSlotIcon(slotType.ToString(), item);
    }

    /// <summary>
    /// Triggered when a draggable item is dropped onto this slot.
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem dragged = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (dragged?.item?.data == null)
        {
            Debug.Log("[GearSlot] Dropped item is null or incomplete.");
            return;
        }

        if (dragged.item.data.clothingSlot != slotType)
        {
            Debug.LogWarning($"[GearSlot] Item '{dragged.item.itemName}' does not match slot '{slotType}'.");
            return;
        }

        SetItem(dragged.item);

        // 🔧 Equip logic forwarding
        Inventory inventory = FindFirstObjectByType<Inventory>();
        inventory?.EquipItem(dragged.item);
    }

    /// <summary>
    /// Fires tooltip when pointer enters the slot.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedItem != null && equippedItem.data != null)
        {
            TooltipUI tooltip = FindFirstObjectByType<TooltipUI>();
            tooltip?.SetContent(equippedItem.data.itemName, equippedItem.data.TooltipText);
        }
    }

    /// <summary>
    /// Hides tooltip when pointer exits.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI tooltip = FindFirstObjectByType<TooltipUI>();
        tooltip?.Hide();
    }

    /// <summary>
    /// Returns the clothing slot type.
    /// </summary>
    public ClothingSlot GetSlotType() => slotType;
}
