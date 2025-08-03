using UnityEditor;
using UnityEngine;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
    private string selectedType = "";
    private List<ItemData> filteredItems = new List<ItemData>();
    private bool showFilteredResults = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10);

        ItemDatabase db = (ItemDatabase)target;

        GUILayout.Label("Item Tools", EditorStyles.boldLabel);

        // 🔄 Refresh Button
        if (GUILayout.Button("🔄 Refresh Item List"))
        {
            string[] guids = AssetDatabase.FindAssets("t:ItemData");
            List<ItemData> foundItems = new List<ItemData>();

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(path);
                if (item != null)
                    foundItems.Add(item);
            }

            // ⚙️ Clear old entries first
            db.allItemDataAssets.Clear();
            db.allItems.Clear();

            // ✅ Assign new items
            db.allItemDataAssets = foundItems;
            db.allItems = foundItems;

            EditorUtility.SetDirty(db);
            Debug.Log($"✅ ItemDatabase updated: {foundItems.Count} items found.");
        }

        GUILayout.Space(10);
        GUILayout.Label("Filter by Item Type", EditorStyles.boldLabel);

        // 🧩 Gather distinct types safely
        var types = db.allItemDataAssets
            .Where(i => i != null)
            .Select(i => i.itemType.ToString())
            .Distinct()
            .ToList();

        if (types.Count > 0)
        {
            int currentIndex = Mathf.Max(0, types.IndexOf(selectedType));
            int newIndex = EditorGUILayout.Popup("Select Type", currentIndex, types.ToArray());
            selectedType = types[newIndex];

            if (GUILayout.Button($"🔍 Show Items of Type '{selectedType}'"))
            {
                filteredItems = db.allItemDataAssets
                    .Where(i => i != null && i.itemType.ToString() == selectedType)
                    .ToList();

                showFilteredResults = true;
            }

            if (showFilteredResults)
            {
                GUILayout.Space(5);
                GUILayout.Label($"📦 {filteredItems.Count} items in '{selectedType}':", EditorStyles.miniBoldLabel);

                foreach (var item in filteredItems)
                {
                    GUILayout.Label($"🧾 {item.itemName} | Weight: {item.weight} | Storage: {item.storageCapacity}");
                }

                GUILayout.Space(5);
                if (GUILayout.Button("❌ Clear Filter"))
                {
                    filteredItems.Clear();
                    showFilteredResults = false;
                }
            }
        }
        else
        {
            GUILayout.Label("⚠️ No valid Item Types found. Try refreshing the list.", EditorStyles.helpBox);
        }
    }
}
