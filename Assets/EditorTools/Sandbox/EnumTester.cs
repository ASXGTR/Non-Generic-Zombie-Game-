using Game.Inventory;
using UnityEngine;

public class EnumTest : MonoBehaviour
{
    void Start()
    {
        DebugDisease(DiseaseType.Salmonella);
        DebugDisease(DiseaseType.Cholera);
        DebugDisease(DiseaseType.RadiationPoisoning);
    }

    private void DebugDisease(DiseaseType disease)
    {
        string message = $"🦠 Disease Enum Value: {disease} ({(int)disease})";

        switch (disease)
        {
            case DiseaseType.Salmonella:
                message += " — Caused by contaminated food.";
                break;
            case DiseaseType.Cholera:
                message += " — Linked to unsafe water sources.";
                break;
            case DiseaseType.RadiationPoisoning:
                message += " — Result of prolonged radiation exposure.";
                break;
            default:
                message += " — Unknown disease type.";
                break;
        }

        Debug.Log(message);
    }
}
