using UnityEngine;

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
    }
}
