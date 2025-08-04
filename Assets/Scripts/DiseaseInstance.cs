// DiseaseInstance.cs
using DiseaseSystem;
using Game.Inventory;
using UnityEngine;

namespace DiseaseSystem
{
    public class DiseaseInstance
    {
        public DiseaseType diseaseType;
        public float severity;
        public bool isCured;
        public float duration;

        public DiseaseInstance(DiseaseType type)
        {
            diseaseType = type;
            severity = 0.1f;
            isCured = false;
            duration = 0f;
        }

        public void UpdateDisease(float elapsedTime, float progressionRate)
        {
            if (isCured) return;
            severity = Mathf.Clamp01(severity + progressionRate * elapsedTime);
            duration += elapsedTime;
        }

        public bool TryCure(string itemId, DiseaseConfig config)
        {
            if (isCured || config == null) return false;
            if (config.cureItemIDs.Contains(itemId))
            {
                isCured = true;
                severity = 0f;
                return true;
            }
            return false;
        }

        public string GetStatus()
        {
            if (isCured) return $"{diseaseType} 🟢 Cured";
            if (severity >= 1f) return $"{diseaseType} 💀 Fatal";
            if (severity >= 0.75f) return $"{diseaseType} 🔴 Critical";
            if (severity >= 0.4f) return $"{diseaseType} 🟠 Worsening";
            return $"{diseaseType} 🟡 Mild";
        }
    }
}
