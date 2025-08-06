using UnityEngine;

namespace Flags
{
    public class FlagSetter : MonoBehaviour
    {
        [SerializeField] private string flagName;
        [SerializeField] private bool value;

        public void TriggerFlagChange() => StoryFlags.SetFlag(flagName, value);
    }
}
