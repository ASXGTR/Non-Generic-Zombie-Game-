// File: Assets/Scripts/Flags/FlagSetter.cs

using UnityEngine;

namespace Flags
{
    public class FlagSetter : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string flagName;
        [SerializeField] private bool value;

        public void TriggerFlagChange()
        {
            if (flagSystem == null)
            {
                Debug.LogWarning("FlagSystem reference is missing.");
                return;
            }

            flagSystem.SetFlag(flagName, value);
        }
    }
}
