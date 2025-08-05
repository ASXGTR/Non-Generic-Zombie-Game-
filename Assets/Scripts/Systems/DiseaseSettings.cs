using UnityEngine;

namespace Systems.Disease.Config
{
    [CreateAssetMenu(fileName = "DiseaseSettings", menuName = "Disease System/Settings")]
    public class DiseaseSettings : ScriptableObject
    {
        public float symptomDelay = 5f;
        public int maxActiveDiseases = 3;
        public bool enableDebugLogs = false;
    }
}
