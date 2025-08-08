// File: Assets/Scripts/Flags/FlagListener.cs

using UnityEngine;
using UnityEngine.Events;

namespace Flags
{
    public class FlagListener : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string flagName;
        [SerializeField] private UnityEvent onTrue;

        private void Start()
        {
            if (flagSystem == null)
            {
                Debug.LogWarning("FlagSystem reference is missing.");
                return;
            }

            if (flagSystem.GetFlag(flagName))
                onTrue.Invoke();
        }
    }
}
