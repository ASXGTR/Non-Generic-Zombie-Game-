using Survival.Audio;
using Survival.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Survival.UI.TitleScreen
{
    public class TitleScreenUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject splashContainer;
        [SerializeField] private Button startButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button quitButton;

        [Header("Audio")]
        [SerializeField] private AudioSource buttonAudio;
        [SerializeField] private AudioCue startCue;
        [SerializeField] private AudioCue quitCue;

        [Header("Transitions")]
        [SerializeField] private CanvasGroup fader;
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private string gameSceneName = "MainScene";

        private void Awake()
        {
            startButton.onClick.AddListener(HandleStart);
            creditsButton.onClick.AddListener(HandleCredits);
            quitButton.onClick.AddListener(HandleQuit);
        }

        private void HandleStart()
        {
            PlayAudio(startCue);
            StartCoroutine(FadeAndLoadScene(gameSceneName));
        }

        private void HandleCredits()
        {
            // TODO: Implement credits screen logic
        }

        private void HandleQuit()
        {
            PlayAudio(quitCue);
            Application.Quit();
        }

        private void PlayAudio(AudioCue cue)
        {
            if (buttonAudio != null && cue != AudioCue.None)
            {
                buttonAudio.PlayOneShot(AudioManager.GetClip(cue));
            }
        }

        private System.Collections.IEnumerator FadeAndLoadScene(string sceneName)
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                fader.alpha = timer / fadeDuration;
                timer += Time.deltaTime;
                yield return null;
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
