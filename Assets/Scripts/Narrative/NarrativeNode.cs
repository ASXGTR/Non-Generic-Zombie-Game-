using System.Collections.Generic;
using UnityEngine;
using Survival.Narrative.Triggers;
using Survival.Narrative.Logic;

namespace Survival.Narrative.Core
{
    [System.Serializable]
    public class NarrativeNode
    {
        [Header("Node Identity")]
        public string id;
        [TextArea(3, 10)]
        public string bodyText;

        [Header("Choices")]
        public List<ChoiceData> choices;

        [Header("Triggers")]
        public TriggerSet triggers;
    }
}
