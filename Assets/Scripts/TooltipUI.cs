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
