// File: Assets/Scripts/Systems/IntroFlowController.cs
using UnityEngine;
using UnityEngine.Events;

namespace Game.Systems
{
    public class IntroFlowController : MonoBehaviour
    {
        [SerializeField] private UnityEvent onIntroComplete;

        public void BeginIntroSequence()
        {
            // Play splash, load scene, run Yarn dialogue, etc.
            Debug.Log("[IntroFlowController] Intro started.");
            Invoke(nameof(EndIntroSequence), 5f); // placeholder timing
        }

        private void EndIntroSequence()
        {
            onIntroComplete.Invoke();
            Debug.Log("[IntroFlowController] Intro complete.");
        }
    }
}
