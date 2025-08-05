using System.Collections.Generic;
using UnityEngine;

namespace Systems.Disease.Runtime
{
    public class DiseaseInstance
    {
        public string DiseaseName { get; private set; }
        public float Duration { get; private set; }
        public float TimeRemaining { get; private set; }

        private List<string> symptoms;
        private int symptomIndex;

        public DiseaseInstance(string name, float duration, List<string> symptomList)
        {
            DiseaseName = name;
            Duration = duration;
            TimeRemaining = duration;
            symptoms = symptomList;
            symptomIndex = 0;
        }

        public void Tick(float deltaTime)
        {
            TimeRemaining -= deltaTime;
            if (TimeRemaining <= 0f)
            {
                OnExpired?.Invoke(this);
            }
        }

        public string TriggerNextSymptom()
        {
            if (symptomIndex >= symptoms.Count) return null;
            var symptom = symptoms[symptomIndex];
            symptomIndex++;
            OnSymptomTriggered?.Invoke(symptom);
            return symptom;
        }

        public bool IsExpired() => TimeRemaining <= 0f;

        public event System.Action<string> OnSymptomTriggered;
        public event System.Action<DiseaseInstance> OnExpired;
    }
}
