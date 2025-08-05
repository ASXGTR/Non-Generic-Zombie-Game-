using UnityEngine;

namespace Flags
{
    public class FlagSetter : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string flagName;
        [SerializeField] private bool value;

        public void TriggerFlagChange() => flagSystem.SetFlag(flagName, value);
    }
}
