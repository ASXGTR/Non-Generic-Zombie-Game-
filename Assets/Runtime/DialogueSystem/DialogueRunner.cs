using UnityEngine;

namespace DialogueSystem
{
    public class DialogueRunner : MonoBehaviour
    {
        public DialogueUI dialogueUI;
        public StoryFlags storyFlags;

        public void StartDialogue(string dialogueText)
        {
            if (dialogueUI != null)
                dialogueUI.Show(dialogueText);
        }

        public bool HasFlag(string flag) => storyFlags.HasFlag(flag);
        public void AddFlag(string flag) => storyFlags.AddFlag(flag);
        public void RemoveFlag(string flag) => storyFlags.RemoveFlag(flag);
    }
}
