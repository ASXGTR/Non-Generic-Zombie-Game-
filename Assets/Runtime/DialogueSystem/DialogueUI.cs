using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        public Text dialogueTextBox;
        public GameObject dialoguePanel;

        public void Show(string dialogue)
        {
            if (dialogueTextBox != null)
                dialogueTextBox.text = dialogue;

            if (dialoguePanel != null)
                dialoguePanel.SetActive(true);
        }

        public void Hide()
        {
            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);
        }
    }
}
