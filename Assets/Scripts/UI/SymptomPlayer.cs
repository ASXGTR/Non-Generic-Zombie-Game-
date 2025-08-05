using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class SymptomPlayer : MonoBehaviour
    {
        [SerializeField] private Text symptomText;
        [SerializeField] private AudioSource audioSource;

        public void PlaySymptom(string symptom, AudioClip clip)
        {
            symptomText.text = symptom;
            if (clip != null)
                audioSource.PlayOneShot(clip);
        }

        public void Clear()
        {
            symptomText.text = "";
        }
    }
}
