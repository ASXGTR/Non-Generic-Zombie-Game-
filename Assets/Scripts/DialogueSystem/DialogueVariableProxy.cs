// File: Assets/Scripts/DialogueSystem/DialogueVariableProxy.cs
using UnityEngine;

namespace Game.DialogueSystem
{
    public class DialogueVariableProxy : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string flagName;

        public bool GetValue() => flagSystem.GetFlag(flagName);
    }
}
