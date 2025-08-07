// File: Assets/Scripts/Validation/SceneFlagValidator.cs

using UnityEngine;
using Flags; // ✅ Added to resolve StoryFlags

namespace Game.Validation // ✅ Corrected namespace
{
    public class SceneFlagValidator : MonoBehaviour
    {
        [SerializeField] private string[] expectedFlags;

        private void Awake()
        {
            foreach (string key in expectedFlags)
            {
                if (!StoryFlags.IsSet(key)) // ✅ Uses public method
                    Debug.LogWarning($"[SceneFlagValidator] Missing expected flag: {key}");
            }
        }
    }
}
