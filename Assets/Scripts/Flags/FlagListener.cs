using UnityEngine;
using UnityEngine.Events;

namespace Flags
{
    public class FlagListener : MonoBehaviour
    {
        [SerializeField] private string flagName;
        [SerializeField] private UnityEvent onTrue;

        private void Start()
        {
            if (StoryFlags.GetFlag(flagName))
                onTrue.Invoke();
        }
    }
}
