using Game.Inventory;
using Game.Inventory.EditorTools;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10);

        ItemData item = (ItemData)target;
        GUILayout.Label("🧾 Item Preview", EditorStyles.boldLabel);

        GUILayout.Label($"📛 Name: {item.itemName}");
        GUILayout.Label($"⚖️ Weight: {item.weight}kg");
        GUILayout.Label($"📦 Storage Capacity: {item.storageCapacity}");

        GUILayout.Label($"🗃 Type: {item.itemType}");
        GUILayout.Label($"📚 Category: {item.category}");
        GUILayout.Label($"🏷 Flags: {item.flags}");

        if (item.tags != null && item.tags.Count > 0)
        {
            GUILayout.Label("🏷 Tags:");
            foreach (var tag in item.tags)
                GUILayout.Label($"• {tag}", EditorStyles.miniLabel);
        }
        else
        {
            GUILayout.Label("🚫 No Tags Assigned", EditorStyles.helpBox);
        }

        GUILayout.Space(10);
        if (GUILayout.Button("🎯 Validate Item"))
        {
            string issues = ItemDataValidator.Validate(item);
            if (string.IsNullOrEmpty(issues))
                Debug.Log($"✅ '{item.itemName}' passed validation.");
            else
                Debug.LogWarning($"⚠️ Issues in '{item.itemName}':\n{issues}");
        }
    }
}
