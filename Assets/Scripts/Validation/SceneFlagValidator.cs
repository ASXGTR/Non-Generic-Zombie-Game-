// File: Assets/Scripts/Validation/SceneFlagValidator.cs
using UnityEngine;

namespace Game.DialogueSystem
{
    public class SceneFlagValidator : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string[] expectedFlags;

        private void Awake()
        {
            foreach (string key in expectedFlags)
            {
                if (!flagSystem.flags.ContainsKey(key))
                    Debug.LogWarning($"[SceneFlagValidator] Missing expected flag: {key}");
            }
        }
    }
}
