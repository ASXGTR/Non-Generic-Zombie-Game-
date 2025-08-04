using UnityEngine;
using UnityEngine.UI;
using Game.Inventory;

namespace Game.UI
{
    /// <summary>
    /// Represents a single preview slot in a tooltip panel, showing item icon and triggering hover tooltip.
    /// </summary>
    public class TooltipPreviewSlot : MonoBehaviour
    {
        [Header("Preview")]
        [SerializeField] private Image icon;
        [SerializeField] private TooltipTrigger tooltipTrigger;

        private InventoryItem item;

        /// <summary>
        /// Sets the item to preview in this slot.
        /// </summary>
        public void SetItem(InventoryItem newItem)
        {
            item = newItem;

            if (item == null)
            {
                if (icon != null) icon.sprite = null;
                if (icon != null) icon.enabled = false;
                if (tooltipTrigger != null) tooltipTrigger.Clear();
                return;
            }

            if (icon != null)
            {
                icon.sprite = item.icon;
                icon.enabled = item.icon != null;
            }

            if (tooltipTrigger != null)
                tooltipTrigger.SetupFromItem(item);
        }

        /// <summary>
        /// Returns the current item previewed in this slot.
        /// </summary>
        public InventoryItem GetItem() => item;

        /// <summary>
        /// (Optional) Test slot manually via inspector.
        /// </summary>
        [ContextMenu("⏺️ Clear Preview")]
        private void ClearSlot()
        {
            SetItem(null);
            Debug.Log("[TooltipPreviewSlot] Slot cleared manually.");
        }
    }
}
