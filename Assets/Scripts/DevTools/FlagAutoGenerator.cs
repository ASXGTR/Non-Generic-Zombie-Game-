// File: Assets/Scripts/DevTools/FlagAutoGenerator.cs
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Game.DialogueSystem
{
#if UNITY_EDITOR
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
                            string key = line.Trim().Split('$')[1].Split(' ')[0];
                            allFlags.Add(key);
                        }
                    }
                }

                var flagAsset = CreateInstance<StoryFlags>();
                foreach (var flag in allFlags)
                    flagAsset.SetFlag(flag, false);

                AssetDatabase.CreateAsset(flagAsset, $"Assets/Flags/{flagAssetName}.asset");
                AssetDatabase.SaveAssets();
                Debug.Log($"[FlagAutoGenerator] Created StoryFlags asset with {allFlags.Count} flags.");
            }
        }
    }
#endif
}
