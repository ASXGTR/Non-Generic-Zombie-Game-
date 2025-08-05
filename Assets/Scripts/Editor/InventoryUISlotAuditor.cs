using Game.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlotAuditor : EditorWindow
{
    [MenuItem("Tools/Audit/Inventory UI Slot Integrity")]
    public static void ShowWindow()
    {
        GetWindow<InventoryUISlotAuditor>("Inventory UISlot Audit").RunAudit();
    }

    private void RunAudit()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        int missingSlotCount = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab.TryGetComponent(out InventoryUISlot slot))
            {
                bool missing = false;
                if (slot.ItemIcon == null) { Debug.LogWarning($"❌ Missing ItemIcon in {path}"); missing = true; }
                if (slot.UseButton == null) { Debug.LogWarning($"❌ Missing UseButton in {path}"); missing = true; }
                if (slot.DropButton == null) { Debug.LogWarning($"❌ Missing DropButton in {path}"); missing = true; }

                if (!missing)
                {
                    Debug.Log($"✅ Slot OK: {path}");
                }
                else
                {
                    missingSlotCount++;
                }
            }
        }

        if (missingSlotCount == 0)
        {
            Debug.Log("🎉 All InventoryUISlot prefabs passed audit.");
        }
        else
        {
            Debug.LogWarning($"⚠️ {missingSlotCount} InventoryUISlot prefabs have missing references.");
        }
    }
}
