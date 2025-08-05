using UnityEngine;

namespace DialogueSystem
{
    public class DialogueVariableProxy : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string flagName;

        public bool GetValue() => flagSystem.GetFlag(flagName);
    }
}
