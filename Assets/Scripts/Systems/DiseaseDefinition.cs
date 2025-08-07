// File: Assets/Scripts/Gameplay/Disease/DiseaseDefinition.cs
using UnityEngine;
using DiseaseSystem; // Only needed if you want to use DiseaseType

namespace Gameplay.Disease
{
    [CreateAssetMenu(fileName = "NewDisease", menuName = "Disease System/Disease Definition")]
    public class DiseaseDefinition : ScriptableObject
    {
        public string DiseaseName;
        public string Description;
        public Sprite Icon;
        public float Duration;
        public string[] Symptoms;
        public AudioClip[] SymptomSounds;

        // Optional: Link to enum
        public DiseaseType Type;

        // Optional: Cure items
        public string[] CureItemIds;
    }
}
