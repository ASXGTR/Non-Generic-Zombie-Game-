// ARCHIVED SCRIPT — DO NOT MODIFY OR INCLUDE IN BUILD
// Original: TooltipUI.cs
// Role: Legacy tooltip renderer and controller
// Status: Replaced by modular systems:
//   - TooltipDisplay.cs        (UI/Tooltip)
//   - TooltipData.cs           (UI/Tooltip)
// Date Archived: 2025-08-05
// Runtime: This script should NOT be executed at runtime.
// Notes:
//   - Visual logic migrated to TooltipDisplay.cs
//   - Singleton access retained via Instance pattern
//   - Retained for audit and migration traceability only
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Inventory;

namespace Game.UI
{
    /// <summary>
    /// Handles display logic for in-game tooltips and linked item icons.
    /// </summary>
    public class TooltipUI : MonoBehaviour
    {
        public static TooltipUI Instance;

        [Header("UI References")]
        [SerializeField] private GameObject root;
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private Image previewIcon;

        private const string logTag = "[TooltipUI]";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            HideTooltip();
        }

        /// <summary>
        /// Directly set tooltip content from strings.
        /// </summary>
        public void SetContent(string header, string content)
        {
            if (headerText != null) headerText.text = header ?? "";
            if (contentText != null) contentText.text = content ?? "";
        }

        /// <summary>
        /// Displays tooltip UI from InventoryItem reference.
        /// </summary>
        public void ShowTooltip(InventoryItem item)
        {
            if (item == null)
            {
                HideTooltip();
                Debug.LogWarning($"{logTag} Tried to show tooltip for null item.");
                return;
            }

            SetContent(item.ItemName ?? "Unnamed", item.TooltipText ?? "No description available.");

            if (previewIcon != null)
            {
                bool hasIcon = item.icon != null;
                previewIcon.enabled = hasIcon;
                previewIcon.sprite = hasIcon ? item.icon : null;
            }

            root?.SetActive(true);
        }

        /// <summary>
        /// Hides tooltip and clears preview icon.
        /// </summary>
        public void HideTooltip()
        {
            if (root != null) root.SetActive(false);

            if (previewIcon != null)
            {
                previewIcon.sprite = null;
                previewIcon.enabled = false;
            }
        }
    }
}
