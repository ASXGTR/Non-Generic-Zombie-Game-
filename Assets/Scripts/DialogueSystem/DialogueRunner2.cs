using UnityEngine;

namespace DialogueSystem
{
    public class DialogueRunner : MonoBehaviour
    {
        [SerializeField] private DialogueUI ui;
        [SerializeField] private TextAsset dialogueScript;

        private string[] lines;
        private int currentLine;

        private void Start()
        {
            lines = dialogueScript.text.Split('\n');
            currentLine = 0;
            RunNextLine();
        }

        public void RunNextLine()
        {
            if (currentLine < lines.Length)
            {
                ui.DisplayLine(lines[currentLine]);
                currentLine++;
            }
            else
            {
                ui.HideLine();
            }
        }

        public void OnContinue()
        {
            RunNextLine();
        }
    }
}
