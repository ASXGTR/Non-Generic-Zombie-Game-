// File: Assets/Scripts/Scene/NarrativeSceneBinder.cs
using UnityEngine;
using Flags;

namespace Game.DialogueSystem
{
    /// <summary>
    /// Marks a scene as entered by setting a story flag.
    /// Used to track narrative progression.
    /// </summary>
    public class NarrativeSceneBinder : MonoBehaviour
    {
        [SerializeField] private string sceneTriggerFlag;

        public void MarkSceneEntered()
        {
            if (string.IsNullOrEmpty(sceneTriggerFlag))
            {
                Debug.LogWarning("[NarrativeSceneBinder] No flag name provided.");
                return;
            }

            StoryFlags.Set(sceneTriggerFlag, true);
            Debug.Log($"[NarrativeSceneBinder] Scene entered. Flag '{sceneTriggerFlag}' set.");
        }
    }
}
