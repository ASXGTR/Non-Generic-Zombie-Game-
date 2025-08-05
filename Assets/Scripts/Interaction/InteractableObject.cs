using UnityEngine;
using UnityEngine.Events;

namespace Game.Interaction
{
    public class InteractableObject : MonoBehaviour
    {
        [Header("Interaction Metadata")]
        [SerializeField] private string interactionPrompt = "Examine";
        [SerializeField] private InteractionTags[] interactionTags;

        [Header("Interaction Events")]
        [SerializeField] private UnityEvent onInteract;

        public string GetPrompt() => interactionPrompt;

        public bool HasTag(InteractionTags tag)
        {
            foreach (var t in interactionTags)
                if (t == tag) return true;

            return false;
        }

        public void Interact()
        {
            Debug.Log($"[InteractableObject] Player interacted: {interactionPrompt}");
            onInteract.Invoke();
        }
    }
}
