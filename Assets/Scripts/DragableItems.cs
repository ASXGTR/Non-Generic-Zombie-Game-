using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Inventory;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("Type of item, e.g. 'Helmet', 'Weapon'")]
    public string itemType;

    public Image icon;

    // 🆕 Runtime reference to the actual item this UI represents
    public Item item;

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
    }

    // 🆕 Call this when spawning the draggable item
    public void Initialize(Item sourceItem)
    {
        item = sourceItem;

        if (item != null && item.data != null)
        {
            itemType = item.data.clothingSlot.ToString(); // Optional: still helpful for legacy logic
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
        transform.SetParent(transform.root); // Bring to front

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform != null)
            rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);

        if (rectTransform != null)
            rectTransform.localPosition = Vector3.zero;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;
    }
}
