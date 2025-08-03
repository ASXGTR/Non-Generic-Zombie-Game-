using UnityEngine;
using Game.Inventory;

public class DiseaseProgress
{
    public DiseaseType diseaseType;
    public float severity; // Value between 0.0 (none) and 1.0 (critical)
    public bool isCured;

    public DiseaseProgress(DiseaseType type)
    {
        diseaseType = type;
        severity = 0.1f;  // Starting severity
        isCured = false;
    }

    public void ProgressOverTime(float rate)
    {
        if (isCured) return;
        severity = Mathf.Clamp01(severity + rate);
    }
}
