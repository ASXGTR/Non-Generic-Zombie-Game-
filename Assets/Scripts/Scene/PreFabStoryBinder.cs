// File: Assets/Scripts/Scene/PrefabStoryBinder.cs
using UnityEngine;
using Flags;

namespace Game.DialogueSystem
{
    /// <summary>
    /// Activates GameObjects based on story flags.
    /// Used to conditionally enable prefabs in scenes.
    /// </summary>
    public class PrefabStoryBinder : MonoBehaviour
    {
        [SerializeField] private string[] flagsToCheck;
        [SerializeField] private GameObject[] activateIfTrue;

        private void Start()
        {
            if (flagsToCheck.Length != activateIfTrue.Length)
            {
                Debug.LogWarning("[PrefabStoryBinder] Mismatched flag and object arrays.");
                return;
            }

            for (int i = 0; i < flagsToCheck.Length; i++)
            {
                string flag = flagsToCheck[i];
                GameObject target = activateIfTrue[i];

                if (target == null || string.IsNullOrEmpty(flag))
                {
                    Debug.LogWarning($"[PrefabStoryBinder] Missing target or flag at index {i}.");
                    continue;
                }

                bool active = StoryFlags.IsSet(flag);
                target.SetActive(active);

                Debug.Log($"[PrefabStoryBinder] Flag '{flag}' is {(active ? "set" : "unset")}. Target '{target.name}' {(active ? "enabled" : "disabled")}.");
            }
        }
    }
}
