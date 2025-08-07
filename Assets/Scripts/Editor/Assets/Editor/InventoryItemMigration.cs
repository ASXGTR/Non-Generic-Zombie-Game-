// File: Assets/Editor/InventoryItemMigration.cs

using Core.Shared.Models;
using NUnit.Framework.Interfaces;
using System.IO;
using UnityEditor;
using UnityEngine;

public class InventoryItemMigration : EditorWindow
{
    [MenuItem("Tools/Migrate InventoryItem to ItemData")]
    public static void MigrateInventoryItems()
    {
        string[] guids = AssetDatabase.FindAssets("t:InventoryItem");

        int migrated = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            InventoryItem oldItem = AssetDatabase.LoadAssetAtPath<InventoryItem>(path);

            if (oldItem == null) continue;

            // Create new ItemData asset
            ItemData newItem = ScriptableObject.CreateInstance<ItemData>();

            newItem.name = oldItem.name;
            newItem.icon = oldItem.Icon;
            newItem.itemName = oldItem.ItemName;
            newItem.tooltipText = oldItem.Description;
            newItem.slotFootprint = 1;
            newItem.storageCapacity = oldItem.IsContainer ? oldItem.InternalStorage?.Count ?? 0 : 0;
            newItem.containerCapacity = oldItem.IsContainer ? oldItem.InternalStorage?.Count ?? 0 : 0;

            string newPath = path.Replace("InventoryItem", "ItemData").Replace(".asset", "_Migrated.asset");
            AssetDatabase.CreateAsset(newItem, newPath);

            migrated++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"[Migration] ✅ Migrated {migrated} InventoryItem assets to ItemData.");
    }
}
