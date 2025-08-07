// File: Assets/Scripts/DialogueSystem/DialogueRunner.cs

using UnityEngine;

namespace DialogueSystem
{
    public class DialogueRunner : MonoBehaviour
    {
        [Header("Dialogue Setup")]
        [SerializeField] private DialogueUI ui;
        [SerializeField] private TextAsset dialogueScript;

        private string[] lines;
        private int currentLine;
        private bool dialogueStarted;

        private void Start()
        {
            dialogueStarted = false;
        }

        /// <summary>
        /// Starts the dialogue sequence from a given ID.
        /// </summary>
        public void StartDialogue(string id)
        {
            if (dialogueScript == null)
            {
                Debug.LogWarning("[DialogueRunner] No dialogue script assigned.");
                return;
            }

            lines = dialogueScript.text.Split('\n');
            currentLine = 0;
            dialogueStarted = true;
            RunNextLine();
        }

        /// <summary>
        /// Advances to the next line in the dialogue.
        /// </summary>
        public void RunNextLine()
        {
            if (!dialogueStarted || lines == null || lines.Length == 0)
            {
                Debug.LogWarning("[DialogueRunner] Dialogue not started or script is empty.");
                return;
            }

            if (currentLine < lines.Length)
            {
                ui.DisplayLine(lines[currentLine]);
                currentLine++;
            }
            else
            {
                ui.HideLine();
                dialogueStarted = false;
            }
        }

        /// <summary>
        /// Called by UI to continue dialogue.
        /// </summary>
        public void OnContinue()
        {
            RunNextLine();
        }
    }
}
