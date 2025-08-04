using UnityEngine;
using UnityEngine.Events;
using Game.Inventory;

namespace Game.UI
{
    /// <summary>
    /// Binds tooltip content to UI trigger events and optional item metadata.
    /// </summary>
    public class TooltipTrigger : MonoBehaviour
    {
        [Header("Tooltip Content")]
        [SerializeField] private string header;
        [TextArea(2, 5)]
        [SerializeField] private string content;

        [Header("Linked Item (Optional)")]
        [SerializeField] private InventoryItem linkedItem;

        [Header("Events")]
        public UnityEvent OnTooltipUpdated;

        private const string logTag = "[TooltipTrigger]";

        /// <summary>
        /// Configures tooltip text manually.
        /// </summary>
        public void Setup(string newHeader, string newContent)
        {
            header = string.IsNullOrEmpty(newHeader) ? "Unnamed" : newHeader;
            content = string.IsNullOrEmpty(newContent) ? "No description available." : newContent;
            linkedItem = null;

            OnTooltipUpdated?.Invoke();

            if (TooltipUI.Instance != null)
            {
                TooltipUI.Instance.SetContent(header, content);
                TooltipUI.Instance.ShowTooltip(null);
            }
        }

        /// <summary>
        /// Configures tooltip from an InventoryItem reference.
        /// </summary>
        public void SetupFromItem(InventoryItem item)
        {
            if (item == null)
            {
                Debug.LogWarning($"{logTag} Attempted to setup tooltip from null item.");
                return;
            }

            linkedItem = item;
            header = item.ItemName ?? "Unnamed";
            content = item.TooltipText ?? "No description available.";

            OnTooltipUpdated?.Invoke();

            if (TooltipUI.Instance != null)
            {
                TooltipUI.Instance.SetContent(header, content);
                TooltipUI.Instance.ShowTooltip(linkedItem);
            }
        }

        /// <summary>
        /// Clears tooltip content and hides UI.
        /// </summary>
        public void Clear()
        {
            header = "";
            content = "";
            linkedItem = null;

            OnTooltipUpdated?.Invoke();

            if (TooltipUI.Instance != null)
                TooltipUI.Instance.HideTooltip();
        }

        /// <summary>
        /// Manually shows tooltip UI with current data.
        /// </summary>
        public void Show()
        {
            if (TooltipUI.Instance != null)
                TooltipUI.Instance.ShowTooltip(linkedItem);
        }

        /// <summary>
        /// Manually hides tooltip UI.
        /// </summary>
        public void Hide()
        {
            if (TooltipUI.Instance != null)
                TooltipUI.Instance.HideTooltip();
        }

        /// <summary>
        /// Gets the current tooltip header text.
        /// </summary>
        public string GetHeader() => header;

        /// <summary>
        /// Gets the current tooltip content text.
        /// </summary>
        public string GetContent() => content;

        /// <summary>
        /// Gets the current linked item (may be null).
        /// </summary>
        public InventoryItem GetLinkedItem() => linkedItem;
    }
}
