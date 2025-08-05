using UnityEngine;

namespace Debug
{
    /// <summary>
    /// Utility for clearing and inspecting inventory slot grids during runtime or debugging.
    /// </summary>
    public class ItemGridDebugger : MonoBehaviour
    {
        [Header("Item Grid Debug")]
        public Transform slotParent;
        public bool autoClearOnStart = true;

        private const string logTag = "[ItemGridDebugger]";

        private void Start()
        {
            if (autoClearOnStart)
            {
                ClearGrid();
                UnityEngine.Debug.Log($"{logTag} Cleared existing slots under '{(slotParent != null ? slotParent.name : "null")}' on Start.");
            }
        }

        /// <summary>
        /// Clears all child slot GameObjects under the slot parent.
        /// </summary>
        [ContextMenu("Clear Grid")]
        public void ClearGrid()
        {
            if (slotParent == null)
            {
                UnityEngine.Debug.LogWarning($"{logTag} Slot parent not assigned.");
                return;
            }

            int removed = 0;
            foreach (Transform child in slotParent)
            {
                DestroyImmediate(child.gameObject);
                removed++;
            }

            UnityEngine.Debug.Log($"{logTag} Removed {removed} slots from '{slotParent.name}'.");
        }

        /// <summary>
        /// Logs all child GameObjects under the slot parent.
        /// </summary>
        [ContextMenu("Log Slot Children")]
        public void LogSlotChildren()
        {
            if (slotParent == null)
            {
                UnityEngine.Debug.LogWarning($"{logTag} Slot parent not assigned.");
                return;
            }

            UnityEngine.Debug.Log($"{logTag} SlotParent '{slotParent.name}' has {slotParent.childCount} children:");
            for (int i = 0; i < slotParent.childCount; i++)
            {
                UnityEngine.Debug.Log($"  - [{i}] {slotParent.GetChild(i).name}");
            }
        }
    }
}
