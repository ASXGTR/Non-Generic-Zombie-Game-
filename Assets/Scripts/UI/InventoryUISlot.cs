using Core.Shared.Models;
using Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
	/// <summary>
	/// UI slot for drag-drop behavior, item assignment, and lock state.
	/// </summary>
	public class InventoryUISlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[Header("Slot References")]
		[SerializeField] private Transform itemAnchor;
		[SerializeField] private Image highlight;

		[Header("Slot State")]
		[SerializeField] private InventoryItem assignedItem;
		[SerializeField] private bool isLocked;

		public InventoryItem AssignedItem => assignedItem;

		private Color defaultHighlightColor;
		private readonly Color hoverColor = new(0.9f, 0.9f, 1f, 1f);

		private void Awake()
		{
			if (highlight != null)
				defaultHighlightColor = highlight.color;
		}

		public void OnDrop(PointerEventData eventData)
		{
			if (isLocked || eventData.pointerDrag == null)
			{
				Debug.Log($"[InventoryUISlot] Drop rejected: Locked={isLocked} or no item.");
				return;
			}

			var draggable = eventData.pointerDrag.GetComponent<DraggableItem>();
			if (draggable?.Item == null)
			{
				Debug.LogWarning("[InventoryUISlot] Invalid drop: missing DraggableItem or item reference.");
				return;
			}

			if (ValidateItem(draggable.Item))
			{
				AssignItem(draggable.Item);
				draggable.OnSlotAssigned(this);
			}
			else
			{
				Debug.Log($"[InventoryUISlot] Item rejected: {draggable.Item.name}");
			}
		}

		public void AssignItem(InventoryItem item)
		{
			assignedItem = item;
			item.transform.SetParent(itemAnchor, false);
			item.transform.localPosition = Vector3.zero;
			Debug.Log($"[InventoryUISlot] Assigned: {item.name}");
		}

		public bool ValidateItem(InventoryItem item) => item != null;

		public void ClearSlot()
		{
			if (assignedItem != null)
			{
				Destroy(assignedItem.gameObject);
				assignedItem = null;
				Debug.Log("[InventoryUISlot] Slot cleared.");
			}
		}

		public void LockSlot(bool locked)
		{
			isLocked = locked;
			Debug.Log($"[InventoryUISlot] Lock state changed: {locked}");
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (highlight != null)
				highlight.color = hoverColor;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (highlight != null)
				highlight.color = defaultHighlightColor;
		}
	}
}
