using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Game.Inventory;

public class Editor_ItemDataLabelerAuto : AssetPostprocessor
{
    // This method is called automatically by Unity after assets are imported/created/modified
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        bool changed = false;

        foreach (string assetPath in importedAssets)
        {
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(assetPath);
            if (itemData != null)
            {
                var labelsList = new System.Collections.Generic.List<string>();

                // Add itemType label
                labelsList.Add(itemData.itemType.ToString());

                // Add tags as labels, avoid duplicates and empty strings
                foreach (var tag in itemData.tags)
                {
                    if (!string.IsNullOrWhiteSpace(tag) && !labelsList.Contains(tag.Trim()))
                    {
                        labelsList.Add(tag.Trim());
                    }
                }

                AssetDatabase.SetLabels(itemData, labelsList.ToArray());
                Debug.Log($"[AutoLabeler] Set labels [{string.Join(", ", labelsList)}] for '{itemData.itemName}'");

                changed = true;
            }
        }

        if (changed)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
