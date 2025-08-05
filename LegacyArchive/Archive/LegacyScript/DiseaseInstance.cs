// ARCHIVED SCRIPT — DO NOT MODIFY OR INCLUDE IN BUILD
// Original: DiseaseInstance.cs
// Role: Runtime container for active disease effects
// Status: Migrated to modular system
// Replaced By:
//   - DiseaseInstance.cs (Systems/Disease/Runtime)
//   - SymptomPlayer.cs (UI/HUD/Symptoms)
//   - DiseaseManager.cs (Systems/Disease)
// Date Archived: 2025-08-05
// Runtime: This script should NOT be executed at runtime.
// Notes: Retained for traceability. All references should be updated. Exclude from build via assembly filters or conditional symbols.
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
