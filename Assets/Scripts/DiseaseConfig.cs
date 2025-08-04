// DiseaseConfig.cs
// Authoring-friendly disease definition for runtime progression and cure logic

using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace DiseaseSystem
{
    [CreateAssetMenu(fileName = "NewDiseaseConfig", menuName = "Diseases/Disease Config")]
    public class DiseaseConfig : ScriptableObject
    {
        [Header("Identity")]
        public DiseaseType diseaseType;
        public string diseaseName;
        [TextArea] public string description;

        [Header("Progression")]
        [Range(0f, 1f)] public float baseSeverity = 0.1f;
        [Range(0f, 2f)] public float progressionRate = 0.2f;
        public bool autoCuresOverTime = false;
        public float naturalRecoveryRate = 0f;

        [Header("Contagion")]
        public bool isContagious = false;
        [Range(0f, 1f)] public float infectionChance = 0.25f;

        [Header("Recovery & Treatment")]
        public List<string> cureItemIDs = new();

        [Header("Visuals & Feedback")]
        public Sprite icon;
        public AudioClip diseaseSound;

        [Header("Editor Debug")]
        public bool showInDebugMenu = true;
    }
}
