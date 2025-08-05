using UnityEngine;
using System.Collections.Generic;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "StoryFlags", menuName = "Dialogue/StoryFlags")]
    public class StoryFlags : ScriptableObject
    {
        [SerializeField] private List<string> activeFlags = new List<string>();

        public bool HasFlag(string flag) => activeFlags.Contains(flag);

        public void AddFlag(string flag)
        {
            if (!activeFlags.Contains(flag))
                activeFlags.Add(flag);
        }

        public void RemoveFlag(string flag)
        {
            if (activeFlags.Contains(flag))
                activeFlags.Remove(flag);
        }

        public List<string> GetAllFlags() => new List<string>(activeFlags);
    }
}
