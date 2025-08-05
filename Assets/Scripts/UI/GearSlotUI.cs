using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Inventory.DataModels;
using Systems.Equipment;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GearSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [Header("Slot Settings")]
    public GearType type;
    public Image icon;
    public Button button;

    private GearData currentGear;
    private IGearManager gearManager;
    private bool isHovered;

    void Awake()
    {
        gearManager = Object.FindFirstObjectByType<GearManager>();
        if (gearManager == null)
            Debug.LogError("GearManager not found. GearSlotUI will not function correctly.");

        if (button != null)
            button.onClick.AddListener(OnClick);
        else
            Debug.LogWarning($"GearSlotUI on {gameObject.name} has no Button assigned.");

        ClearSlot();
    }

    public void SetGear(GearData gear)
    {
        currentGear = gear;

        if (icon != null)
        {
            icon.sprite = gear.icon;
            icon.enabled = true;
        }
    }

    public void ClearSlot()
    {
        currentGear = null;

        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    private void OnClick()
    {
        if (gearManager == null) return;

        if (currentGear != null)
        {
            gearManager.Unequip(type);
        }
        else
        {
            gearManager.OpenEquipMenu(type);
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
        if (dragged != null && dragged.Gear != null && gearManager != null)
        {
            gearManager.Equip(type, dragged.Gear);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!isHovered) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(100, 100, 0));
        Handles.Label(transform.position + Vector3.up * 60, $"GearSlot: {type}", EditorStyles.boldLabel);
    }
#endif
}
