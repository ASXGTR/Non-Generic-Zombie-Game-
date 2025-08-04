// DiseaseDatabase.cs
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
