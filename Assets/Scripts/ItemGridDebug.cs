using UnityEngine;

public class ItemGridDebugger : MonoBehaviour
{
    [Header("Item Grid Debug")]
    public Transform slotParent;
    public bool autoClearOnStart = true;

    void Start()
    {
        if (autoClearOnStart)
        {
            ClearGrid();
            Debug.Log($"[ItemGridDebugger] Cleared existing slots under '{slotParent.name}' on Start.");
        }
    }

    public void ClearGrid()
    {
        if (slotParent == null)
        {
            Debug.LogWarning("[ItemGridDebugger] Slot parent not assigned.");
            return;
        }

        int removed = 0;
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
            removed++;
        }

        Debug.Log($"[ItemGridDebugger] Removed {removed} slots from '{slotParent.name}'.");
    }

    public void LogSlotChildren()
    {
        if (slotParent == null)
        {
            Debug.LogWarning("[ItemGridDebugger] Slot parent not assigned.");
            return;
        }

        Debug.Log($"[ItemGridDebugger] SlotParent '{slotParent.name}' has {slotParent.childCount} children:");
        for (int i = 0; i < slotParent.childCount; i++)
        {
            Debug.Log($"  - [{i}] {slotParent.GetChild(i).name}");
        }
    }
}
