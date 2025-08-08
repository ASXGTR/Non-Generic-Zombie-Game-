// File: Assets/Scripts/Flags/FlagGroupLoader.cs

using UnityEngine;

namespace Flags
{
    [System.Serializable]
    public class FlagGroup
    {
        public string name;
        public string[] flags;
    }

    public class FlagGroupLoader : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private FlagGroup[] groups;

        public void LoadGroup(string groupName)
        {
            if (flagSystem == null)
            {
                Debug.LogWarning("FlagSystem reference is missing.");
                return;
            }

            foreach (var g in groups)
            {
                if (g.name == groupName)
                {
                    foreach (var f in g.flags)
                        flagSystem.SetFlag(f, true);
                    break;
                }
            }
        }
    }
}
