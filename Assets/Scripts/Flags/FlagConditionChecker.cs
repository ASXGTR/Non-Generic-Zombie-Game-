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
            if (flagSystem.GetFlag(flagName))
                ifTrue.Invoke();
            else
                ifFalse.Invoke();
        }
    }
}
