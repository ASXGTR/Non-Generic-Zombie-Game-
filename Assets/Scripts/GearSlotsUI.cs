using Game.Inventory;
using Game.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

/// <summary>
/// Displays equipped gear in the HUD with hover tooltip.
/// </summary>
public class GearSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Slot Identification")]
    public GearSlotType slotType;

    [Header("UI References")]
    [SerializeField] private Image gearIconImage;
    [SerializeField] private Text gearNameLabel;

    [Header("Default Slot Sprite")]
    [SerializeField] private Sprite defaultSlotSprite;

    private Item currentItem;

    /// <summary>
    /// Initializes slot visuals with the given item.
    /// </summary>
    public void Initialize(Item item)
    {
        if (item == null || item.data == null)
        {
            ClearSlot();
            return;
        }

        currentItem = item;

        Sprite iconToUse = item.EquippedSprite ?? item.icon ?? defaultSlotSprite;

        if (gearIconImage != null)
        {
            gearIconImage.sprite = iconToUse;
            gearIconImage.color = Color.white;
        }

        if (gearNameLabel != null)
        {
            gearNameLabel.text = string.IsNullOrEmpty(item.ItemName) ? "Unnamed Gear" : item.ItemName;

            gearNameLabel.color = RarityColorUtility != null
                ? RarityColorUtility.GetColor(item.rarity)
                : Color.white;
        }
    }

    /// <summary>
    /// Clears slot visuals and hides tooltip.
    /// </summary>
    public void ClearSlot()
    {
        currentItem = null;

        if (gearIconImage != null)
        {
            gearIconImage.sprite = defaultSlotSprite;
            gearIconImage.color = Color.white;
        }

        if (gearNameLabel != null)
        {
            gearNameLabel.text = "";
            gearNameLabel.color = Color.white;
        }

        TooltipUI.Instance?.HideTooltip(true);
    }

    public Item GetCurrentItem() => currentItem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem != null && currentItem.data != null)
            TooltipUI.Instance?.ShowTooltip(currentItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.Instance?.HideTooltip();
    }
}
