using UnityEngine;
using UnityEngine.UI;
using Game.Inventory;
using Game.UI;

namespace Game.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [Header("UI References")]
        public Image iconImage;
        public TooltipTrigger tooltipTrigger;

        [Header("Fallback Settings")]
        [Tooltip("Optional icon if item is missing or has no sprite.")]
        public Sprite defaultIcon;

        private Item currentItem;

        /// <summary>
        /// Initializes the slot with an item. Can be null for empty slots.
        /// </summary>
        public void Initialize(Item item)
        {
            SetItem(item);
        }

        /// <summary>
        /// Assigns an item to the slot and updates visuals.
        /// </summary>
        public void SetItem(Item item)
        {
            currentItem = item;

            if (item == null)
            {
                ClearSlot();
                return;
            }

            // 🔍 Ensure item icon is set, fallback if needed
            Sprite iconToUse = item.icon != null ? item.icon : defaultIcon;

            if (iconImage != null)
            {
                iconImage.enabled = iconToUse != null;
                iconImage.sprite = iconToUse;
            }

            // 🧠 Setup tooltip
            if (tooltipTrigger != null)
            {
                string title = string.IsNullOrEmpty(item.itemName) ? "Unknown Item" : item.itemName;
                string description = string.IsNullOrEmpty(item.itemDescription) ? "No description available." : item.itemDescription;
                tooltipTrigger.Setup(title, description);
            }
        }

        /// <summary>
        /// Clears slot visuals and tooltip.
        /// </summary>
        public void ClearSlot()
        {
            currentItem = null;

            if (iconImage != null)
            {
                iconImage.sprite = null;
                iconImage.enabled = false;
            }

            if (tooltipTrigger != null)
                tooltipTrigger.Setup("", "");
        }
    }
}
