using UnityEngine;
using UnityEngine.UI;

namespace Debug
{
    public class FlagDisplayDebugger : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private Text debugText;

        private void Update()
        {
            string output = "Story Flags:\n";
            foreach (var kvp in flagSystem.flags)
                output += $"{kvp.Key}: {kvp.Value}\n";

            debugText.text = output;
        }
    }
}
