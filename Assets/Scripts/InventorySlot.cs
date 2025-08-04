using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory
{
    /// <summary>
    /// A UI slot capable of holding and validating inventory items. Supports drag/drop, highlighting, and debug feedback.
    /// </summary>
    public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Slot References")]
        [SerializeField] private Image slotHighlight;
        [SerializeField] private Transform itemAnchor;

        [Header("Slot State")]
        [SerializeField] private InventoryItem assignedItem;
        [SerializeField] private bool isLocked;

        public InventoryItem AssignedItem => assignedItem;

        private Color defaultHighlightColor;
        private readonly Color hoverColor = new Color(0.9f, 0.9f, 1f, 1f);

        private void Awake()
        {
            if (slotHighlight != null)
                defaultHighlightColor = slotHighlight.color;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (isLocked || eventData.pointerDrag == null)
            {
                Debug.Log($"[InventorySlot] Drop rejected: Locked={isLocked} or no item");
                return;
            }

            DraggableItem draggable = eventData.pointerDrag.GetComponent<DraggableItem>();
            if (draggable == null || draggable.Item == null)
            {
                Debug.LogWarning("[InventorySlot] Invalid drop: missing DraggableItem component or item reference.");
                return;
            }

            if (ValidateItem(draggable.Item))
            {
                AssignItem(draggable.Item);
                draggable.OnSlotAssigned(this);
            }
            else
            {
                Debug.Log($"[InventorySlot] Item '{draggable.Item.name}' rejected by slot '{gameObject.name}'");
            }
        }

        public void AssignItem(InventoryItem item)
        {
            assignedItem = item;
            item.transform.SetParent(itemAnchor, false);
            item.transform.localPosition = Vector3.zero;
            Debug.Log($"[InventorySlot] Item '{item.name}' assigned to slot '{gameObject.name}'");
        }

        public bool ValidateItem(InventoryItem item)
        {
            // Hook for item validation logic (type check, compatibility, etc.)
            return item != null;
        }

        public void ClearSlot()
        {
            if (assignedItem != null)
            {
                Destroy(assignedItem.gameObject);
                assignedItem = null;
                Debug.Log($"[InventorySlot] Slot '{gameObject.name}' cleared");
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (slotHighlight != null)
                slotHighlight.color = hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (slotHighlight != null)
                slotHighlight.color = defaultHighlightColor;
        }

        public void LockSlot(bool locked)
        {
            isLocked = locked;
            Debug.Log($"[InventorySlot] Slot '{gameObject.name}' lock state changed: {isLocked}");
        }
    }
}
