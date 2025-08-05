using DiseaseSystem;
using Game.Inventory;
using UnityEngine;

namespace Game.Status.Models
{
    [System.Serializable]
    public class DiseaseData
    {
        public DiseaseType Type;
        public string Name;
        public float Severity;
        public float RemainingTime;
        public bool IsCured;

        public DiseaseData(DiseaseType type, string name, float severity, float duration)
        {
            Type = type;
            Name = name;
            Severity = severity;
            RemainingTime = duration;
            IsCured = false;
        }

        public void Tick(float deltaTime, float rate)
        {
            if (IsCured) return;
            RemainingTime -= deltaTime;
            Severity = Mathf.Clamp01(Severity + rate * deltaTime);
        }

        public bool TryCure(string itemId, DiseaseConfig config)
        {
            if (IsCured) return false;
            if (config.cureItemIds.Contains(itemId))
            {
                IsCured = true;
                Severity = 0f;
                return true;
            }
            return false;
        }
    }
}
