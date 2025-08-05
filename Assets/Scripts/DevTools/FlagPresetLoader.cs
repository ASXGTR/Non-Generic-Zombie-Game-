// File: Assets/Scripts/DevTools/FlagPresetLoader.cs
using UnityEngine;

namespace Game.DialogueSystem
{
    [System.Serializable]
    public struct FlagPreset
    {
        public string name;
        public bool value;
    }

    public class FlagPresetLoader : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private FlagPreset[] presets;

        public void ApplyPresets()
        {
            foreach (var p in presets)
                flagSystem.SetFlag(p.name, p.value);
        }
    }
}
