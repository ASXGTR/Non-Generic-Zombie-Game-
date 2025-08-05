using DiseaseSystem;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UI.HUD;
using Audio.Effects;
using Game.Status.Models;

namespace Game.Status
{
    [AddComponentMenu("Status/Disease Manager")]
    public class DiseaseManager : MonoBehaviour
    {
        [Header("Disease Settings")]
        [Tooltip("Database holding all disease configurations")]
        [SerializeField] private DiseaseDatabase diseaseDatabase;

        [Tooltip("Current active disease instances")]
        [SerializeField] private List<DiseaseData> activeDiseases = new();

        // 📣 Events
        public static event System.Action<DiseaseType> OnDiseaseContracted;
        public static event System.Action<DiseaseType> OnDiseaseCured;
        public static event System.Action<DiseaseType, float> OnDiseaseProgressed;

        // 🧪 Attempt infection
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

        // 🩺 Infection status check
        private bool HasDisease(DiseaseType type) =>
            activeDiseases.Any(d => d.Type == type && !d.IsCured);

        // 🛡️ Immunity calculation
        private float GetImmunityChance(float resistance)
        {
            float normalized = Mathf.Clamp01(resistance / 100f);
            return Mathf.Lerp(0.75f, 0f, normalized);
        }

        // 💉 Disease application
        private void ApplyDisease(DiseaseType type)
        {
            var name = diseaseDatabase?.GetName(type);
            var disease = new DiseaseData(type, name, 0.25f, 60f);
            activeDiseases.Add(disease);

            DiseaseHUD.ShowSymptoms(type);
            DiseaseAudioController.PlaySymptoms(type);
            VitalsManager.ApplyDiseaseEffect(type, disease.Severity);

            OnDiseaseContracted?.Invoke(type);
            Debug.Log($"[DiseaseManager] Contracted disease: {type}");
        }

        // ⏳ Progress update
        public void UpdateDiseases(float deltaTime, float progressionRate = 0.01f)
        {
            foreach (var disease in activeDiseases.Where(d => !d.IsCured))
            {
                disease.Tick(deltaTime, progressionRate);

                VitalsManager.ApplyDiseaseEffect(disease.Type, disease.Severity);
                OnDiseaseProgressed?.Invoke(disease.Type, disease.Severity);

                if (disease.Severity >= 1f)
                {
                    DiseaseHUD.ShowCriticalSymptoms(disease.Type);
                    DiseaseAudioController.PlayCriticalSymptoms(disease.Type);
                    DeathHandler.TriggerDeath("Disease: " + disease.Name);
                }
            }
        }

        // 🧪 Attempt cure
        public bool TryCureDisease(DiseaseType type, string itemId)
        {
            var instance = activeDiseases.FirstOrDefault(d => d.Type == type && !d.IsCured);
            if (instance == null) return false;

            var config = diseaseDatabase?.GetConfig(type);
            if (config == null) return false;

            if (instance.TryCure(itemId, config))
            {
                VitalsManager.RemoveDiseaseEffect(type);
                DiseaseHUD.HideSymptoms(type);
                DiseaseAudioController.StopSymptoms(type);

                OnDiseaseCured?.Invoke(type);
                Debug.Log($"[DiseaseManager] Cured disease: {type} using item: {itemId}");
                return true;
            }

            return false;
        }

        // 📋 Get status summary
        public List<string> GetDiseaseStatusSummaries() =>
            activeDiseases.Select(d => $"{d.Type} — Severity: {d.Severity:F2}, Duration: {d.RemainingTime:F0}s").ToList();

        // 🦠 Get all active types
        public List<DiseaseType> GetActiveDiseaseTypes() =>
            activeDiseases.Where(d => !d.IsCured).Select(d => d.Type).ToList();
    }
}
