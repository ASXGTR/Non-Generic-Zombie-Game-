using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

namespace DialogueSystem.EditorTools
{
    public static class SceneFlagScanner
    {
        [MenuItem("Tools/Validate/Scene Flags")]
        public static void ScanAllScenes()
        {
            List<string> scenePaths = new List<string>
            {
                "Assets/Scenes/DemoFlags/CampfireStranger.unity",
                "Assets/Scenes/DemoFlags/BrokenForkQuest.unity",
                "Assets/Scenes/DemoFlags/RecipeLore.unity"
            };

            int missingCount = 0;

            foreach (var path in scenePaths)
            {
                var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                var runner = Object.FindFirstObjectByType<DialogueSystem.DialogueRunner>();

                if (runner == null)
                {
                    Debug.LogWarning($"⚠️ [SceneFlagScanner] Missing DialogueRunner in: {path}");
                    missingCount++;
                }
                else
                {
                    Debug.Log($"✅ [SceneFlagScanner] Found DialogueRunner in: {path}");
                }
            }

            Debug.Log($"[SceneFlagScanner] Scan complete. Scenes checked: {scenePaths.Count}. Missing DialogueRunner in {missingCount} scene(s).");
        }
    }
}
