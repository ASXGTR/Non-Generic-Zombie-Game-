// File: Assets/Scripts/Scene/PrefabStoryBinder.cs

using UnityEngine;
using Flags;

namespace Scene
{
    /// <summary>
    /// Enables or disables a target GameObject based on a StoryFlag.
    /// Used for conditional prefab activation in scenes.
    /// </summary>
    public class PrefabStoryBinder : MonoBehaviour
    {
        [SerializeField] private string flagName;
        [SerializeField] private GameObject targetObject;

        private void Awake()
        {
            if (targetObject == null || string.IsNullOrEmpty(flagName))
            {
                Debug.LogWarning("[PrefabStoryBinder] Missing target or flag name.");
                return;
            }

            bool shouldEnable = StoryFlags.IsSet(flagName);
            targetObject.SetActive(shouldEnable);

            Debug.Log($"[PrefabStoryBinder] Flag '{flagName}' is {(shouldEnable ? "set" : "unset")}. Target '{targetObject.name}' {(shouldEnable ? "enabled" : "disabled")}.");
        }
    }
}
