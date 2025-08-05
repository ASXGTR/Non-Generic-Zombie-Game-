using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Inventory.EditorTools
{
    public class ItemDataValidator : EditorWindow
    {
        private Sprite fallbackSprite;

        [MenuItem("Tools/ItemData Validator")]
        public static void ShowWindow()
        {
            GetWindow<ItemDataValidator>("ItemData Validator");
        }

        private void OnGUI()
        {
            GUILayout.Label("🧼 Validate & Repair ItemData Assets", EditorStyles.boldLabel);
            fallbackSprite = (Sprite)EditorGUILayout.ObjectField("Fallback Sprite", fallbackSprite, typeof(Sprite), false);

            if (GUILayout.Button("🧹 Scan & Fix ItemData Assets"))
            {
                ValidateAllItemData();
            }
        }

        private void ValidateAllItemData()
        {
            string[] assetGUIDs = AssetDatabase.FindAssets("t:ItemData");
            int globalFixCount = 0;
            int iconFixedCount = 0;
            int equippedFixedCount = 0;
            int fallbackUsed = 0;

            foreach (string guid in assetGUIDs)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(path);

                if (item == null) continue;

                bool needsFix = false;
                SerializedObject serializedItem = new SerializedObject(item);

                var iconProp = serializedItem.FindProperty("icon");
                var equippedProp = serializedItem.FindProperty("equippedSprite");
                var nameProp = serializedItem.FindProperty("itemName");
                var idProp = serializedItem.FindProperty("id");

                // 🧠 Validate icon
                if (iconProp != null && iconProp.objectReferenceValue == null)
                {
                    if (equippedProp != null && equippedProp.objectReferenceValue != null)
                    {
                        iconProp.objectReferenceValue = equippedProp.objectReferenceValue;
                        Debug.Log($"🛠️ [Icon Restored] {item.name} used equippedSprite as icon.");
                        iconFixedCount++;
                    }
                    else if (fallbackSprite != null)
                    {
                        iconProp.objectReferenceValue = fallbackSprite;
                        Debug.Log($"🛠️ [Fallback Icon] {item.name} assigned fallback sprite.");
                        iconFixedCount++;
                        fallbackUsed++;
                    }
                    else
                    {
                        Debug.LogWarning($"❌ [Missing Icon] {item.name} has no sprite reference!");
                    }
                    needsFix = true;
                }

                // 🧠 Validate equippedSprite
                if (equippedProp != null && equippedProp.objectReferenceValue == null)
                {
                    if (fallbackSprite != null)
                    {
                        equippedProp.objectReferenceValue = fallbackSprite;
                        Debug.Log($"🛠️ [Fallback EquippedSprite] {item.name} assigned fallback.");
                        equippedFixedCount++;
                        fallbackUsed++;
                        needsFix = true;
                    }
                    else
                    {
                        Debug.LogWarning($"⚠️ [Missing EquippedSprite] {item.name} has no equipped visual.");
                    }
                }

                // 🔍 Validate core string fields
                if (string.IsNullOrWhiteSpace(item.ItemName))
                {
                    Debug.LogWarning($"⚠️ [Missing ItemName] {item.name} — Name field empty.");
                }
                if (string.IsNullOrWhiteSpace(item.Id))
                {
                    Debug.LogWarning($"⚠️ [Missing ID] {item.name} — ID field empty.");
                }

                // 🏷️ Validate tags
                if (item.Tags == null || item.Tags.Count == 0)
                {
                    Debug.LogWarning($"📛 [No Tags] {item.name} has no tags assigned.");
                }
                else
                {
                    var duplicates = item.Tags.GroupBy(t => t).Where(g => g.Count() > 1).Select(g => g.Key);
                    foreach (string dup in duplicates)
                    {
                        Debug.LogWarning($"🔁 [Duplicate Tag] {item.name} has duplicate tag: '{dup}'");
                    }
                }

                // 💾 Apply fixes
                if (needsFix)
                {
                    serializedItem.ApplyModifiedProperties();
                    EditorUtility.SetDirty(item);
                    globalFixCount++;
                }
            }

            AssetDatabase.SaveAssets();

            Debug.Log($"✅ Validation complete!\n" +
                      $"🔍 Scanned: {assetGUIDs.Length} items\n" +
                      $"🛠️ Fixed: {globalFixCount} assets\n" +
                      $"🖼️ Icons repaired: {iconFixedCount}\n" +
                      $"👕 EquippedSprites repaired: {equippedFixedCount}\n" +
                      $"📦 Fallbacks used: {fallbackUsed}");
        }
    }
}
