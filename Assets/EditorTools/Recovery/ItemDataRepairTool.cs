using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public class ItemDataRepairTool : EditorWindow
{
    [MenuItem("Tools/ItemData Repair/Reattach Missing Sprites")]
    public static void ReattachSprites()
    {
        string[] guids = AssetDatabase.FindAssets("t:ItemData", new[] { "Assets/Items_Index" });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Game.Inventory.ItemData>(path);

            bool changed = false;

            if (asset != null)
            {
                // If field is null but fallback isn't active, force default
                if (asset.Icon == null)
                {
                    asset.Icon = Game.Inventory.ItemData.DefaultIcon;
                    changed = true;
                }

                if (asset.EquippedSprite == null)
                {
                    asset.EquippedSprite = Game.Inventory.ItemData.DefaultEquippedSprite;
                    changed = true;
                }

                if (changed)
                {
                    EditorUtility.SetDirty(asset);
                    Debug.Log($"🔧 Patched missing sprite in: {path}");
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("✅ ItemData reattachment complete.");
    }
}
