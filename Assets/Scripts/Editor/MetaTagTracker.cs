// File: Assets/Scripts/Editor/MetaTagTracker.cs
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

namespace Game.DialogueSystem
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    public class MetaTagTracker
    {
        static MetaTagTracker()
        {
            string[] types = { "NPC", "Interactable", "LoreBit" };
            List<string> matches = new();

            foreach (var guid in AssetDatabase.FindAssets("t:TextAsset"))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var content = AssetDatabase.LoadAssetAtPath<TextAsset>(path)?.text;

                foreach (var type in types)
                {
                    if (content != null && content.Contains($"<{type}>"))
                        matches.Add($"{type} found in {path}");
                }
            }

            foreach (var line in matches)
                Debug.Log($"[MetaTagTracker] {line}");
        }
    }
#endif
}
