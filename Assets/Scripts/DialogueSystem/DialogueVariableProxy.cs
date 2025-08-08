// File: Assets/Scripts/DialogueSystem/DialogueVariableProxy.cs

using UnityEngine;
using Flags;

namespace DialogueSystem
{
    /// <summary>
    /// Proxy for querying a story flag from the dialogue system.
    /// </summary>
    public class DialogueVariableProxy : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string flagName;

        public bool GetValue()
        {
            if (flagSystem == null)
            {
                Debug.LogWarning("[DialogueVariableProxy] FlagSystem reference is missing.");
                return false;
            }

            if (string.IsNullOrEmpty(flagName))
            {
                Debug.LogWarning("[DialogueVariableProxy] No flag name provided.");
                return false;
            }

            bool value = flagSystem.IsSet(flagName);
            Debug.Log($"[DialogueVariableProxy] Flag '{flagName}' is {(value ? "set" : "unset")}.");
            return value;
        }
    }
}
