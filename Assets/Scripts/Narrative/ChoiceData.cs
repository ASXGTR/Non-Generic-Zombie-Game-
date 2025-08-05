using UnityEngine;

namespace Survival.Narrative.Logic
{
    [System.Serializable]
    public class ChoiceData
    {
        public string label;
        public string nextNodeId;
        public bool requiresConfirmation;
        public string effectTag;
    }
}
