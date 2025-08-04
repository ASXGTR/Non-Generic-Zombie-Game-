// DiseaseManager.cs
using DiseaseSystem;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiseaseManager : MonoBehaviour
{
    public List<DiseaseInstance> activeDiseases = new();
    public DiseaseDatabase diseaseDatabase;

    public static event System.Action<DiseaseType> OnDiseaseContracted;
    public static event System.Action<DiseaseType> OnDiseaseCured;
    public static event System.Action<DiseaseType, float> OnDiseaseProgressed;

    public void TryContractDisease(DiseaseType type, float sicknessResistance)
    {
        if (HasDisease(type)) return;

        DiseaseConfig config = diseaseDatabase.GetConfig(type);
        if (config == null)
        {
            Debug.LogWarning($"🚫 No config found for disease type: {type}");
            return;
        }

        float immunity = GetImmunityChance(sicknessResistance);
        float finalChance = config.infectionChance * (1f - immunity);

        if (Random.value < finalChance)
            ApplyDisease(type);
    }

    bool HasDisease(DiseaseType type) =>
        activeDiseases.Any(d => d.diseaseType == type && !d.isCured);

    float GetImmunityChance(float resistanceValue)
    {
        float normalized = Mathf.Clamp01(resistanceValue / 100f);
        return Mathf.Lerp(0.75f, 0f, normalized);
    }

    void ApplyDisease(DiseaseType type)
    {
        var newInstance = new DiseaseInstance(type);
        activeDiseases.Add(newInstance);
        OnDiseaseContracted?.Invoke(type);
        Debug.Log($"🦠 Contracted disease: {type}");
    }

    public void UpdateDiseases(float elapsedTime, float progressionRate = 0.01f)
    {
        foreach (var disease in activeDiseases.Where(d => !d.isCured))
        {
            float prevSeverity = disease.severity;
            disease.UpdateDisease(elapsedTime, progressionRate);

            if (disease.severity != prevSeverity)
                OnDiseaseProgressed?.Invoke(disease.diseaseType, disease.severity);
        }
    }

    public bool TryCureDisease(DiseaseType type, string itemId)
    {
        var instance = activeDiseases.FirstOrDefault(d => d.diseaseType == type);
        if (instance == null || instance.isCured) return false;

        DiseaseConfig config = diseaseDatabase.GetConfig(type);
        if (config == null) return false;

        if (instance.TryCure(itemId, config))
        {
            OnDiseaseCured?.Invoke(type);
            Debug.Log($"✅ Cured disease: {type} using item: {itemId}");
            return true;
        }

        return false;
    }

    public List<string> GetDiseaseStatusSummaries() =>
        activeDiseases.Select(d => d.GetStatus()).ToList();

    public List<DiseaseType> GetActiveDiseaseTypes() =>
        activeDiseases.Where(d => !d.isCured).Select(d => d.diseaseType).ToList();
}
