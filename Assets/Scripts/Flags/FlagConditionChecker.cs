// File: Assets/Scripts/Flags/FlagConditionChecker.cs

using UnityEngine;
using UnityEngine.Events;

namespace Flags
{
    public class FlagConditionChecker : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string flagName;
        [SerializeField] private UnityEvent ifTrue;
        [SerializeField] private UnityEvent ifFalse;

        public void Evaluate()
        {
            if (flagSystem == null)
            {
                Debug.LogWarning("FlagSystem reference is missing.");
                return;
            }

            if (flagSystem.GetFlag(flagName))
                ifTrue.Invoke();
            else
                ifFalse.Invoke();
        }
    }
}
