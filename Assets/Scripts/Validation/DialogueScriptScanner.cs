// File: Assets/Scripts/Validation/DialogueScriptScanner.cs
using UnityEngine;

namespace Game.DialogueSystem
{
    public class DialogueScriptScanner : MonoBehaviour
    {
        [SerializeField] private TextAsset dialogueScript;
        [SerializeField] private string[] requiredKeys;

        private void Start()
        {
            foreach (string key in requiredKeys)
            {
                if (!dialogueScript.text.Contains(key))
                    Debug.LogWarning($"[DialogueScriptScanner] Key '{key}' missing in {dialogueScript.name}");
            }
        }
    }
}
