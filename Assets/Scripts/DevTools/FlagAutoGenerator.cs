// File: Assets/Scripts/DevTools/FlagAutoGenerator.cs

using UnityEngine;
using UnityEditor;
using System.IO;
using Flags; // ✅ Required to resolve StoryFlags

namespace DevTools
{
#if UNITY_EDITOR
    /// <summary>
    /// Editor tool to auto-generate a StoryFlags asset from dialogue files.
    /// Scans for lines containing '$flagName' and creates entries.
    /// </summary>
    public class FlagAutoGenerator : EditorWindow
    {
        private string flagAssetName = "StoryFlags";
        private string[] rawFlags;

        [MenuItem("Tools/Flag Auto Generator")]
        public static void ShowWindow()
        {
            GetWindow<FlagAutoGenerator>("Flag Auto Generator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Generate StoryFlags from Raw List", EditorStyles.boldLabel);
            flagAssetName = EditorGUILayout.TextField("Asset Name", flagAssetName);

            if (GUILayout.Button("Scan Dialogue Folder"))
            {
                string[] guids = AssetDatabase.FindAssets("t:TextAsset", new[] { "Assets/Dialogues" });
                var allFlags = new System.Collections.Generic.HashSet<string>();

                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    string content = File.ReadAllText(path);

                    foreach (var line in content.Split('\n'))
                    {
                        if (line.Contains("$")) // crude flag reference check
                        {
                            string[] parts = line.Trim().Split('$');
                            if (parts.Length > 1)
                            {
                                string key = parts[1].Split(' ')[0].Trim();
                                if (!string.IsNullOrEmpty(key))
                                    allFlags.Add(key);
                            }
                        }
                    }
                }

                var flagAsset = CreateInstance<StoryFlags>();
                foreach (var flag in allFlags)
                    flagAsset.SetFlag(flag, false);

                string assetPath = $"Assets/Flags/{flagAssetName}.asset";
                Directory.CreateDirectory("Assets/Flags");
                AssetDatabase.CreateAsset(flagAsset, assetPath);
                AssetDatabase.SaveAssets();

                Debug.Log($"[FlagAutoGenerator] Created StoryFlags asset at '{assetPath}' with {allFlags.Count} flags.");
            }
        }
    }
#endif
}
