using Game.Inventory;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataInspector : Editor
{
    private ItemData item;
    private SerializedProperty iconProp;
    private SerializedProperty equippedSpriteProp;
    private SerializedProperty tagsProp;

    private void OnEnable()
    {
        item = (ItemData)target;

        // 🧯 Safely fetch fields even if Unity ghosted them
        iconProp = serializedObject.FindProperty("icon");
        equippedSpriteProp = serializedObject.FindProperty("equippedSprite");
        tagsProp = serializedObject.FindProperty("tags");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("🗂 Item Metadata", EditorStyles.boldLabel);

        DrawTagsAndEquippedSprite();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("🔍 Sprite Overview", EditorStyles.boldLabel);

        DrawSpriteField("Icon", iconProp, item.Icon, item.UsesFallbackIcon, "UI icon shown in inventory slots");
        DrawSpriteField("Equipped Sprite", equippedSpriteProp, item.EquippedSprite, item.UsesFallbackEquippedSprite, "Sprite used when gear is equipped on player");

        EditorGUILayout.Space();
        if (GUILayout.Button("🛠 Auto-Fill From Name"))
        {
            TryAutoAssignSprites(item);
        }

        if (GUILayout.Button("↩️ Reset to Defaults"))
        {
            iconProp.objectReferenceValue = null;
            equippedSpriteProp.objectReferenceValue = null;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawTagsAndEquippedSprite()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("📎 Tags & Visuals", EditorStyles.boldLabel);

        // 🏷️ Manual Tags
        EditorGUILayout.PropertyField(tagsProp, true);

        // 🎽 Equipped Sprite
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(new GUIContent("Equipped Sprite", "Sprite shown when gear is equipped"));
        EditorGUILayout.PropertyField(equippedSpriteProp);

        Sprite preview = item.EquippedSprite;
        if (preview != null)
        {
            Texture2D thumbnail = AssetPreview.GetAssetPreview(preview);
            if (thumbnail != null)
                GUILayout.Label(thumbnail, GUILayout.Width(64), GUILayout.Height(64));
        }
        else
        {
            EditorGUILayout.HelpBox("No equipped sprite assigned. Using fallback.", MessageType.Info);
        }

        if (item.UsesFallbackEquippedSprite)
        {
            EditorGUILayout.HelpBox("⚠️ Fallback equipped sprite in use. Consider assigning manually.", MessageType.Warning);
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawSpriteField(string label, SerializedProperty prop, Sprite displaySprite, bool isFallback, string tooltip)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField(new GUIContent(label, tooltip), EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(prop);

        if (displaySprite != null)
        {
            Texture2D preview = AssetPreview.GetAssetPreview(displaySprite);
            if (preview != null)
                GUILayout.Label(preview, GUILayout.Width(64), GUILayout.Height(64));
        }
        else
        {
            EditorGUILayout.HelpBox("No sprite assigned. Using fallback.", MessageType.Warning);
        }

        if (isFallback)
        {
            EditorGUILayout.HelpBox("⚠️ Fallback sprite in use. Consider assigning manually.", MessageType.Info);
        }

        EditorGUILayout.EndVertical();
    }

    private void TryAutoAssignSprites(ItemData data)
    {
        string itemName = data.name;
        Sprite foundIcon = Resources.Load<Sprite>($"Icons/{itemName}");
        Sprite foundEquipped = Resources.Load<Sprite>($"Equipped/{itemName}");

        iconProp.objectReferenceValue = foundIcon;
        equippedSpriteProp.objectReferenceValue = foundEquipped;
    }
}
