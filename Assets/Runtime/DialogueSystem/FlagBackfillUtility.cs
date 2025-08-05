using UnityEngine;

namespace DialogueSystem
{
    public class FlagBackfillUtility : MonoBehaviour
    {
        public StoryFlags storyFlags;
        public string[] requiredFlags;

        void Start()
        {
            if (storyFlags == null || requiredFlags == null) return;

            foreach (var flag in requiredFlags)
            {
                if (!storyFlags.HasFlag(flag))
                    storyFlags.AddFlag(flag);
            }
        }
    }
}
