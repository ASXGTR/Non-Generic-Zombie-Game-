using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text dialogueText;
        [SerializeField] private Button continueButton;

        public void DisplayLine(string line)
        {
            dialogueText.text = line;
            continueButton.gameObject.SetActive(true);
        }

        public void HideLine()
        {
            dialogueText.text = "";
            continueButton.gameObject.SetActive(false);
        }
    }
}
