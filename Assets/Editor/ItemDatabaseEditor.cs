using UnityEditor;
using UnityEngine;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
    private string selectedType = "";
    private List<ItemData> filteredItems = new();
    private bool showFilteredResults = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10);

        ItemDatabase db = (ItemDatabase)target;

        GUILayout.Label("📂 Item Tools", EditorStyles.boldLabel);

        DrawRefreshButton(db);
        DrawLabelingButton();
        DrawMissingTagAudit(db);
        DrawTypeFilter(db);
    }

    private void DrawRefreshButton(ItemDatabase db)
    {
        if (GUILayout.Button("🔄 Refresh Item List"))
        {
            RefreshItemList(db);
        }
    }

    private void RefreshItemList(ItemDatabase db)
    {
        var guids = AssetDatabase.FindAssets("t:ItemData");
        var foundItems = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<ItemData>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(item => item != null)
            .ToList();

        db.allItemDataAssets = new List<ItemData>(foundItems);
        db.allItems = new List<ItemData>(foundItems);

        EditorUtility.SetDirty(db);
        Debug.Log($"✅ Refreshed: {foundItems.Count} items loaded.");
    }

    private void DrawLabelingButton()
    {
        GUILayout.Space(5);
        if (GUILayout.Button("🏷 Reassign Labels (Tags + Type)"))
        {
            ItemDataLabelerEditor.LabelSelectedItemDataAssets();
        }
    }

    private void DrawMissingTagAudit(ItemDatabase db)
    {
        GUILayout.Space(5);
        if (GUILayout.Button("🔎 Find Items With Missing Tags"))
        {
            filteredItems = db.allItemDataAssets
                .Where(i => i != null && (i.tags == null || i.tags.Count == 0))
                .ToList();

            selectedType = "[Missing Tags]";
            showFilteredResults = true;
        }
    }

    private void DrawTypeFilter(ItemDatabase db)
    {
        GUILayout.Space(10);
        GUILayout.Label("🧪 Filter by Item Type", EditorStyles.boldLabel);

        var types = db.allItemDataAssets
            .Where(i => i != null)
            .Select(i => i.itemType.ToString())
            .Distinct()
            .OrderBy(t => t)
            .ToList();

        if (types.Count == 0)
        {
            GUILayout.Label("⚠️ No item types detected. Refresh first.", EditorStyles.helpBox);
            return;
        }

        int currentIndex = Mathf.Max(0, types.IndexOf(selectedType));
        int newIndex = EditorGUILayout.Popup("Select Type", currentIndex, types.ToArray());
        selectedType = types[newIndex];

        if (GUILayout.Button($"🔍 Show Items of Type '{selectedType}'"))
        {
            filteredItems = db.allItemDataAssets
                .Where(i => i != null && i.itemType.ToString() == selectedType)
                .OrderBy(i => i.itemName)
                .ToList();

            showFilteredResults = true;
        }

        if (showFilteredResults)
        {
            GUILayout.Space(5);
            GUILayout.Label($"📦 {filteredItems.Count} items in '{selectedType}':", EditorStyles.miniBoldLabel);

            foreach (var item in filteredItems)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label($"🧾 {item.itemName} | Weight: {item.weight} | Storage: {item.storageCapacity}");

                if (item.tags != null && item.tags.Count > 0)
                    GUILayout.Label("🏷 Tags: " + string.Join(", ", item.tags));
                else
                    GUILayout.Label("🚫 No Tags", EditorStyles.miniLabel);

                GUILayout.EndVertical();
            }

            GUILayout.Space(5);
            if (GUILayout.Button("❌ Clear Filter"))
            {
                filteredItems.Clear();
                showFilteredResults = false;
            }
        }
    }
}
