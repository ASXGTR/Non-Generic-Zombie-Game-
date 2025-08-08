// File: Assets/Scripts/Validation/SceneFlagValidator.cs

using UnityEngine;
using Flags;

namespace Game.Validation
{
    /// <summary>
    /// Validates that expected story flags are set when a scene loads.
    /// Useful for debugging narrative progression.
    /// </summary>
    public class SceneFlagValidator : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string[] expectedFlags;

        private void Awake()
        {
            if (flagSystem == null)
            {
                Debug.LogWarning("[SceneFlagValidator] FlagSystem reference is missing.");
                return;
            }

            foreach (string key in expectedFlags)
            {
                if (!flagSystem.IsSet(key))
                    Debug.LogWarning($"[SceneFlagValidator] Missing expected flag: {key}");
            }
        }
    }
}
