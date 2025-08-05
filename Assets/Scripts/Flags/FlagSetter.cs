// File: Assets/Scripts/Flags/FlagSetter.cs
using UnityEngine;

namespace Game.DialogueSystem
{
    public class FlagSetter : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string flagName;
        [SerializeField] private bool value;

        public void TriggerFlagChange() => flagSystem.SetFlag(flagName, value);
    }
}
