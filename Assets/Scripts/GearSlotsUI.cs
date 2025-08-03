using UnityEngine;
using UnityEngine.UI;
using Game.Inventory;

public class GearSlotUI : MonoBehaviour
{
    [Header("Slot Identification")]
    public GearSlotType slotType;

    [Header("UI References")]
    [SerializeField] private Image gearIconImage;
    [SerializeField] private Text gearNameLabel;

    [Header("Default Slot Sprite")]
    [SerializeField] private Sprite defaultSlotSprite;

    private ItemData currentItem;

    public void Initialize(ItemData item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }

        currentItem = item;

        // ✅ Use public property for icon access
        Sprite iconToUse = item.Icon != null ? item.Icon : defaultSlotSprite;

        if (gearIconImage != null)
        {
            gearIconImage.sprite = iconToUse;
            gearIconImage.color = Color.white;
        }

        if (gearNameLabel != null)
        {
            gearNameLabel.text = string.IsNullOrEmpty(item.itemName) ? "Unnamed Gear" : item.itemName;
        }
    }

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
        }
    }

    public ItemData GetCurrentItem()
    {
        return currentItem;
    }
}
