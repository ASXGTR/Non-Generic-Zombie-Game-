using Core.Shared.Models;
using UnityEngine;
using UnityEngine.UI;
using Systems.Disease.Runtime;

namespace UI.HUD.Disease
{
    public class DiseaseHUDController : MonoBehaviour
    {
        [SerializeField] private Text diseaseText;
        [SerializeField] private SymptomPlayer symptomPlayer;

        public void OnDiseaseApplied(DiseaseInstance instance)
        {
            diseaseText.text = $"{instance.DiseaseName} ({instance.TimeRemaining:F0}s)";
        }

        public void OnSymptomTriggered(string symptom, AudioClip clip)
        {
            symptomPlayer.PlaySymptom(symptom, clip);
        }

        public void OnDiseaseExpired(DiseaseInstance instance)
        {
            diseaseText.text = "";
            symptomPlayer.Clear();
        }
    }
}
