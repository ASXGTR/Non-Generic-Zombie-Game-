// 🔒 Deprecated DragableItems.cs
// Logic modularized into UIDragHandler.cs, SlotDetector.cs, ItemSnapper.cs, and DropValidator.cs on 2025-08-05
// Retained for reference only. Do not use in runtime.
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Inventory;

/// <summary>
/// Represents a UI item that can be dragged between slots or containers.
/// </summary>
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("Type of item, e.g. 'Helmet', 'Weapon'")]
    public string itemType;

    [Header("UI References")]
    public Image icon;

    [Header("Drag Settings")]
    public float dragScale = 1.1f;

    // Runtime references
    public InventoryItem item;
    public InventorySlot originSlot; // ✅ Useful for drag validation

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        if (canvasGroup == null)
            Debug.LogWarning("[DraggableItem] Missing CanvasGroup component!");

        if (rectTransform == null)
            Debug.LogWarning("[DraggableItem] Missing RectTransform component!");

        if (icon == null)
            Debug.LogWarning("[DraggableItem] Icon Image reference is missing.");
    }

    /// <summary>
    /// Initializes the draggable item's visuals and data.
    /// </summary>
    public void Initialize(InventoryItem sourceItem, InventorySlot slotContext = null)
    {
        item = sourceItem;
        originSlot = slotContext;

        if (item != null && item.data != null)
        {
            itemType = item.data.clothingSlot.ToString(); // Optional legacy usage
            icon.sprite = item.data.Icon;
            icon.enabled = true;
        }
        else
        {
            itemType = "Unknown";
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); // Move to top of canvas
        transform.localScale = Vector3.one * dragScale;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;

        // TODO: Add tooltip hiding, drop zone highlight, etc.
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform != null)
            rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        // TODO: Handle drop target validation & audio feedback
    }

    /// <summary>
    /// Clears visuals and resets references (for pooling).
    /// </summary>
    public void ResetVisual()
    {
        item = null;
        originSlot = null;
        itemType = "Unknown";

        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }

        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }
}
