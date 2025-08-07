// File: Assets/Scripts/Interaction/YarnTriggerZone.cs

using UnityEngine;
using DialogueSystem; // ✅ Corrected namespace

namespace Game.Interaction
{
    public class YarnTriggerZone : MonoBehaviour
    {
        [SerializeField] private DialogueRunner runner;
        [SerializeField] private string dialogueId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                runner.StartDialogue(dialogueId); // ✅ Uses merged method
            }
        }
    }
}
