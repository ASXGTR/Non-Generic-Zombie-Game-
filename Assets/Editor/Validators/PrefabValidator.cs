using UnityEditor;
using UnityEngine;

namespace DialogueSystem.EditorTools
{
    public static class PrefabValidator
    {
        [MenuItem("Tools/Validate/Prefab Flag References")]
        public static void RunPrefabScan()
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (prefab.GetComponent<DialogueSystem.NarrativeSceneBinder>() == null)
                    Debug.LogWarning($"Missing NarrativeSceneBinder: {path}");
            }
        }
    }
}
