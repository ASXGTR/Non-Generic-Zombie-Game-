using UnityEngine;
using UnityEditor;

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
            GUILayout.Label("Validate ItemData Assets", EditorStyles.boldLabel);
            fallbackSprite = (Sprite)EditorGUILayout.ObjectField("Fallback Sprite", fallbackSprite, typeof(Sprite), false);

            if (GUILayout.Button("Scan & Fix Missing Icons"))
            {
                ValidateAllItemData();
            }
        }

        private void ValidateAllItemData()
        {
            string[] assetGUIDs = AssetDatabase.FindAssets("t:ItemData");
            int globalFixCount = 0;

            foreach (string guid in assetGUIDs)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(path);

                if (item == null) continue;

                bool needsFix = false;
                SerializedObject serializedItem = new SerializedObject(item);

                // Icon check
                var iconProp = serializedItem.FindProperty("icon");
                if (iconProp != null && iconProp.objectReferenceValue == null)
                {
                    Debug.LogWarning($"❌ [Missing Icon] {item.name} — 'icon' field not assigned. Asset path: {path}", item);
                    if (fallbackSprite != null)
                    {
                        iconProp.objectReferenceValue = fallbackSprite;
                        needsFix = true;
                    }
                }

                // EquippedSprite check
                var equippedProp = serializedItem.FindProperty("equippedSprite");
                if (equippedProp != null && equippedProp.objectReferenceValue == null)
                {
                    Debug.LogWarning($"⚠️ [Missing EquippedSprite] {item.name} — 'equippedSprite' field not assigned. Asset path: {path}", item);
                    if (fallbackSprite != null)
                    {
                        equippedProp.objectReferenceValue = fallbackSprite;
                        needsFix = true;
                    }
                }

                if (needsFix)
                {
                    serializedItem.ApplyModifiedProperties();
                    EditorUtility.SetDirty(item);
                    globalFixCount++;
                }
            }

            AssetDatabase.SaveAssets();
            Debug.Log($"✅ Scan complete. {globalFixCount} item(s) auto-fixed.");
        }
    }
}
