using System.Collections.Generic;
using UnityEngine;
using Game.Inventory; // ✅ This is the key import for DiseaseType

public class DiseaseManager : MonoBehaviour
{
    public List<DiseaseProgress> activeDiseases = new();

    public void TryContractDisease(DiseaseType type, float currentSickness)
    {
        float immunity = GetImmunityChance(currentSickness);
        float baseChance = GetBaseChance(type); // You can switch-case or use SO
        float finalChance = baseChance * (1f - immunity);

        if (Random.value < finalChance)
            ApplyDisease(type);
    }

    float GetImmunityChance(float sicknessValue)
    {
        float normalized = Mathf.Clamp01(sicknessValue / 100f);
        return Mathf.Lerp(0.75f, 0f, normalized); // 0 sickness = 75% immune
    }

    void ApplyDisease(DiseaseType type)
    {
        activeDiseases.Add(new DiseaseProgress(type));
        // Trigger effects, symptoms, UI updates...
    }

    float GetBaseChance(DiseaseType type)
    {
        return type switch
        {
            DiseaseType.Salmonella => 0.2f,
            DiseaseType.ZombieVirus => 0.05f,
            DiseaseType.Hypothermia => 0.15f,
            DiseaseType.Dehydration => 0.3f,
            DiseaseType.FatigueSyndrome => 0.25f,
            DiseaseType.Infection => 0.35f,
            DiseaseType.FoodPoisoning => 0.2f,
            DiseaseType.Heatstroke => 0.2f,
            _ => 0.1f
        };
    }
}
