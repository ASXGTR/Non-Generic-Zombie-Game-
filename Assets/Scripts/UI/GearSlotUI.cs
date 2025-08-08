using Core.Shared.Models;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Inventory;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GearSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [Header("Slot Settings")]
    public string slotName; // e.g. "Helmet", "Boots"
    public Image icon;
    public Button button;

    private ItemInstance currentItem;
    private EquipmentManager equipmentManager;
    private bool isHovered;

    void Awake()
    {
        equipmentManager = Object.FindFirstObjectByType<EquipmentManager>();
        if (equipmentManager == null)
            Debug.LogError("EquipmentManager not found. GearSlotUI will not function correctly.");

        if (button != null)
            button.onClick.AddListener(OnClick);
        else
            Debug.LogWarning($"GearSlotUI on {gameObject.name} has no Button assigned.");

        ClearSlot();
    }

    public void SetGear(ItemInstance item)
    {
        currentItem = item;

        if (icon != null && item?.Data?.icon != null)
        {
            icon.sprite = item.Data.icon;
            icon.enabled = true;
        }
        else if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public void ClearSlot()
    {
        currentItem = null;

        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    private void OnClick()
    {
        if (equipmentManager == null || string.IsNullOrEmpty(slotName)) return;

        if (currentItem != null)
        {
            equipmentManager.UnequipItem(slotName);
            ClearSlot();
        }
        else
        {
            Debug.Log($"Open equip menu for slot: {slotName}");
            // You can implement a UI popup or selection logic here
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        // Optional: Show tooltip or highlight
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        // Optional: Hide tooltip or highlight
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableGearIcon>();
        if (dragged?.ItemInstance != null && equipmentManager != null && !string.IsNullOrEmpty(slotName))
        {
            equipmentManager.EquipItem(slotName, dragged.ItemInstance);
            SetGear(dragged.ItemInstance);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!isHovered) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(100, 100, 0));
        Handles.Label(transform.position + Vector3.up * 60, $"GearSlot: {slotName}", EditorStyles.boldLabel);
    }
#endif
}
