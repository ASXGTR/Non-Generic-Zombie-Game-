using UnityEngine;

namespace Survival.Narrative.Triggers
{
    [System.Serializable]
    public class TriggerSet
    {
        public bool requiresItem;
        public string requiredItemId;
        public bool requiresStat;
        public string statName;
        public float minValue;
    }
}
