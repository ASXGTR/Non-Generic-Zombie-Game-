using UnityEngine;
using Core.Shared;

namespace Scene
{
    /// <summary>
    /// Enables or disables a target GameObject based on a StoryFlag.
    /// Used for conditional prefab activation in scenes.
    /// </summary>
    public class PreFabStoryBinder : MonoBehaviour
    {
        [SerializeField] private string flagName;
        [SerializeField] private GameObject targetObject;

        private void Awake()
        {
            if (targetObject == null || string.IsNullOrEmpty(flagName))
            {
                Debug.LogWarning("[PreFabStoryBinder] Missing target or flag name.");
                return;
            }

            bool shouldEnable = StoryFlags.IsSet(flagName);
            targetObject.SetActive(shouldEnable);

            Debug.Log($"[PreFabStoryBinder] Flag '{flagName}' is {(shouldEnable ? "set" : "unset")}. Target '{targetObject.name}' {(shouldEnable ? "enabled" : "disabled")}.");
        }
    }
}
