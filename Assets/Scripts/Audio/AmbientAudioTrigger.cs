// File: Assets/Scripts/Audio/AmbientAudioTrigger.cs
using UnityEngine;

namespace Game.Audio
{
    public class AmbientAudioTrigger : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioSource audioSource;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
