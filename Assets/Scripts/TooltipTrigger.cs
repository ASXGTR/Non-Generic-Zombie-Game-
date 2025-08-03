using UnityEngine;
using UnityEngine.Events;
using Game.Inventory;

namespace Game.UI
{
    public class TooltipTrigger : MonoBehaviour
    {
        [Header("Tooltip Content")]
        public string header;
        [TextArea(2, 5)]
        public string content;

        [Header("Linked Item (Optional)")]
        public Item linkedItem;

        [Header("Events")]
        public UnityEvent OnTooltipUpdated;

        /// <summary>
        /// Set tooltip text manually.
        /// </summary>
        public void Setup(string newHeader, string newContent)
        {
            header = newHeader;
            content = newContent;

            OnTooltipUpdated?.Invoke();

            // TODO: Hook into your actual TooltipUI script here.
            // Example: TooltipUI.Instance.Show(this);
        }

        /// <summary>
        /// Set tooltip content based on item data.
        /// </summary>
        public void SetupFromItem(Item item)
        {
            if (item == null) return;

            linkedItem = item;
            header = item.itemName;
            content = item.itemDescription;

            OnTooltipUpdated?.Invoke();

            // TODO: Hook into TooltipUI for live item tooltips.
        }

        /// <summary>
        /// Clears tooltip data.
        /// </summary>
        public void Clear()
        {
            header = "";
            content = "";
            linkedItem = null;

            OnTooltipUpdated?.Invoke();
        }
    }
}
