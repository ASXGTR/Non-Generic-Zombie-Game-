using UnityEditor;
using UnityEngine;

public static class ItemDataReserializer
{
    [MenuItem("Tools/Cleanup Invalid Serialized Fields")]
    public static void ReSerializeItemAssets()
    {
        string[] guids = AssetDatabase.FindAssets("t:ItemData");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AssetDatabase.ForceReserializeAssets(new[] { path });
        }

        Debug.Log("♻️ All ItemData assets reserialized.");
    }
}
