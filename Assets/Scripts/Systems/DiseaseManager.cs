using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Systems
{
    [AddComponentMenu("Status/Disease Manager")]
    public class DiseaseManager : MonoBehaviour
    {
        [Header("Disease Settings")]
        [Tooltip("Database holding all disease configurations")]
        [SerializeField] private DiseaseDatabase diseaseDatabase;

        [Tooltip("Current active disease instances")]
        [SerializeField] private List<DiseaseData> activeDiseases = new();

        public static event System.Action<DiseaseType> OnDiseaseContracted;
        public static event System.Action<DiseaseType> OnDiseaseCured;
        public static event System.Action<DiseaseType, float> OnDiseaseProgressed;

        public void TryContractDisease(DiseaseType type, float resistance)
        {
            if (HasDisease(type)) return;

            var config = diseaseDatabase?.GetConfig(type);
            if (config == null)
            {
                Debug.LogWarning($"[DiseaseManager] No config found for disease: {type}");
                return;
            }

            float finalChance = config.infectionChance * (1f - GetImmunityChance(resistance));
            if (Random.value < finalChance)
                ApplyDisease(type);
        }

        private bool HasDisease(DiseaseType type) =>
            activeDiseases.Any(d => d.Type == type && !d.IsCured);

        private float GetImmunityChance(float resistance)
        {
            float normalized = Mathf.Clamp01(resistance / 100f);
            return Mathf.Lerp(0.75f, 0f, normalized);
        }

        private void ApplyDisease(DiseaseType type)
        {
            var name = diseaseDatabase?.GetName(type);
            var disease = new DiseaseData(type, name, 0.25f, 60f);
            activeDiseases.Add(disease);

            DiseaseHUD.ShowSymptoms(type);
            DiseaseAudioController.PlaySymptoms(type);

            OnDiseaseContracted?.Invoke(type);
            Debug.Log($"[DiseaseManager] Contracted disease: {type}");
        }

        public void UpdateDiseases(float deltaTime, float progressionRate = 0.01f)
        {
            foreach (var disease in activeDiseases.Where(d => !d.IsCured))
            {
                disease.Tick(deltaTime, progressionRate);
                OnDiseaseProgressed?.Invoke(disease.Type, disease.Severity);

                if (disease.Severity >= 1f)
                {
                    DiseaseHUD.ShowCriticalSymptoms(disease.Type);
                    DiseaseAudioController.PlayCriticalSymptoms(disease.Type);
                    DeathHandler.TriggerDeath("Disease: " + disease.Name);
                }
            }
        }

        public bool TryCureDisease(DiseaseType type, string itemId)
        {
            var instance = activeDiseases.FirstOrDefault(d => d.Type == type && !d.IsCured);
            if (instance == null) return false;

            var config = diseaseDatabase?.GetConfig(type);
            if (config == null) return false;

            if (instance.TryCure(itemId, config))
            {
                DiseaseHUD.HideSymptoms(type);
                DiseaseAudioController.StopSymptoms(type);

                OnDiseaseCured?.Invoke(type);
                Debug.Log($"[DiseaseManager] Cured disease: {type} using item: {itemId}");
                return true;
            }

            return false;
        }

        public List<string> GetDiseaseStatusSummaries() =>
            activeDiseases.Select(d => $"{d.Type} — Severity: {d.Severity:F2}, Duration: {d.RemainingTime:F0}s").ToList();

        public List<DiseaseType> GetActiveDiseaseTypes() =>
            activeDiseases.Where(d => !d.IsCured).Select(d => d.Type).ToList();
    }

    // 🧩 Stubbed Dependencies

    public enum DiseaseType
    {
        Flu,
        Cold,
        Infection
    }

    public class DiseaseDatabase : MonoBehaviour
    {
        public DiseaseConfig GetConfig(DiseaseType type) => new DiseaseConfig();
        public string GetName(DiseaseType type) => type.ToString();
    }

    public class DiseaseConfig
    {
        public float infectionChance = 0.5f;
    }

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
            RemainingTime -= deltaTime;
            Severity = Mathf.Clamp01(Severity + rate * deltaTime);
        }

        public bool TryCure(string itemId, DiseaseConfig config)
        {
            IsCured = true;
            return true;
        }
    }

    public static class DiseaseHUD
    {
        public static void ShowSymptoms(DiseaseType type) { }
        public static void ShowCriticalSymptoms(DiseaseType type) { }
        public static void HideSymptoms(DiseaseType type) { }
    }

    public static class DiseaseAudioController
    {
        public static void PlaySymptoms(DiseaseType type) { }
        public static void PlayCriticalSymptoms(DiseaseType type) { }
        public static void StopSymptoms(DiseaseType type) { }
    }

    public static class DeathHandler
    {
        public static void TriggerDeath(string reason) { }
    }
}
