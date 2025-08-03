using UnityEditor;
using UnityEngine;
using Game.Inventory;

public static class Editor_ItemDataLabeler
{
    // Run this manually from menu or call from AssetPostprocessor for auto on save (see below)
    [MenuItem("Assets/Label ItemData Assets by Tags and Type")]
    public static void LabelSelectedItemDataAssets()
    {
        var selectedObjects = Selection.objects;

        foreach (var obj in selectedObjects)
        {
            if (obj is ItemData itemData)
            {
                // Collect labels from tags and itemType
                var labelsList = new System.Collections.Generic.List<string>();

                // Add itemType label
                labelsList.Add(itemData.itemType.ToString());

                // Add tags as labels (avoid duplicates)
                foreach (var tag in itemData.tags)
                {
                    if (!string.IsNullOrWhiteSpace(tag) && !labelsList.Contains(tag))
                    {
                        labelsList.Add(tag.Trim());
                    }
                }

                // Assign all labels to the asset
                AssetDatabase.SetLabels(itemData, labelsList.ToArray());

                Debug.Log($"Set labels [{string.Join(", ", labelsList)}] for '{itemData.itemName}'");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
