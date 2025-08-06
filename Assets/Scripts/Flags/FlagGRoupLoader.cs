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
        [SerializeField] private FlagGroup[] groups;

        public void LoadGroup(string groupName)
        {
            foreach (var g in groups)
            {
                if (g.name == groupName)
                {
                    foreach (var f in g.flags)
                        StoryFlags.SetFlag(f, true);
                    break;
                }
            }
        }
    }
}
