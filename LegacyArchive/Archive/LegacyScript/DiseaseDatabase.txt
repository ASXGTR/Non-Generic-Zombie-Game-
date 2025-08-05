// ARCHIVED SCRIPT — DO NOT MODIFY OR INCLUDE IN BUILD
// Original: DiseaseDatabase.cs
// Role: Central disease registry and lookup (Legacy)
// Status: Migrated to modular system
// Replaced By:
//   - DiseaseRegistry.cs (Systems/Disease)
//   - DiseaseDefinition.cs (Systems/Disease/Definitions)
//   - SymptomPlayer.cs (UI/HUD/Symptoms)
//   - DiseaseEditorUtility.cs (Editor/Tools/Disease)
// Date Archived: 2025-08-05
// Runtime: This script should NOT be executed at runtime.
// Notes: Retained for traceability. All references should be updated. Exclude from build compilation via assembly filters or conditional symbols if needed.

using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace DiseaseSystem
{
    public class DiseaseDatabase : MonoBehaviour
    {
        public List<DiseaseConfig> allConfigs;

        public DiseaseConfig GetConfig(DiseaseType type)
        {
            return allConfigs.Find(c => c.diseaseType == type);
        }
    }
}
