// ARCHIVED SCRIPT — DO NOT MODIFY OR INCLUDE IN BUILD
// Original: TooltipPreview.cs
// Role: Legacy tooltip visualizer for editor preview
// Status: Replaced by modular systems:
//   - TooltipPreviewEditor.cs   (Editor/UI)
//   - TooltipData.cs            (UI/Tooltip)
// Date Archived: 2025-08-05
// Runtime: This script should NOT be executed at runtime.
// Notes:
//   - Preview logic moved to CustomEditor drawer
//   - TooltipDisplay.cs not used in editor context
//   - Retained for audit and migration traceability only
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
