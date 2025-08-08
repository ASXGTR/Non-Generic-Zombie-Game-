using UnityEngine;
using System.Collections.Generic;

namespace Audio
{
    public class AudioCuePlayer : MonoBehaviour
    {
        public static AudioCuePlayer Instance { get; private set; }

        [Header("🔊 Audio Source")]
        [SerializeField] private AudioSource audioSource;

        [Header("🎵 Cue Library")]
        [Tooltip("Map each AudioCue to its corresponding AudioClip")]
        [SerializeField] private List<AudioCueClip> cueLibrary = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void PlayCue(AudioCue cue)
        {
            var clip = GetClip(cue);
            if (clip != null && audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"[AudioCuePlayer] Cue not found or audioSource missing: {cue}");
            }
        }

        private AudioClip GetClip(AudioCue cue)
        {
            foreach (var entry in cueLibrary)
            {
                if (entry.cue == cue)
                    return entry.clip;
            }
            return null;
        }
    }

    [System.Serializable]
    public class AudioCueClip
    {
        public AudioCue cue;
        public AudioClip clip;
    }
}
