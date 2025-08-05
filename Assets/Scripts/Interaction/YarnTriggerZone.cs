// File: Assets/Scripts/Interaction/YarnTriggerZone.cs
using UnityEngine;

namespace Game.DialogueSystem
{
    public class YarnTriggerZone : MonoBehaviour
    {
        [SerializeField] private DialogueRunner runner;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                runner.RunNextLine();
        }
    }
}
