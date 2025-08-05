using UnityEngine;

namespace DialogueSystem
{
    public class NarrativeSceneBinder : MonoBehaviour
    {
        public string dialogueText;
        public string triggerFlag;
        public DialogueRunner runner;

        private void OnTriggerEnter(Collider other)
        {
            if (runner != null && !runner.HasFlag(triggerFlag))
            {
                runner.StartDialogue(dialogueText);
                runner.AddFlag(triggerFlag);
            }
        }
    }
}
