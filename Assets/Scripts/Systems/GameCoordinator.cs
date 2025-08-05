using Survival.Audio;
using Survival.Audio.Playback;
using Survival.Core.Scene;
using Survival.Core.State;
using Survival.Core.Time;
using Survival.UI.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Survival.Core.Game
{
    public class GameCoordinator : MonoBehaviour
    {
        [Header("State")]
        [SerializeField] private GameStateTracker stateTracker;
        [SerializeField] private TimeManager timeManager;

        [Header("Scene")]
        [SerializeField] private SceneLoader sceneLoader;

        [Header("Audio")]
        [SerializeField] private AudioCuePlayer audioPlayer;
        [SerializeField] private AudioCue pauseCue;
        [SerializeField] private AudioCue resumeCue;

        [Header("UI")]
        [SerializeField] private PauseController pauseController;

        private bool isPaused;

        private void Awake()
        {
            if (stateTracker == null) stateTracker = FindObjectOfType<GameStateTracker>();
            if (timeManager == null) timeManager = FindObjectOfType<TimeManager>();
            if (sceneLoader == null) sceneLoader = FindObjectOfType<SceneLoader>();
            if (pauseController == null) pauseController = FindObjectOfType<PauseController>();
            if (audioPlayer == null) audioPlayer = FindObjectOfType<AudioCuePlayer>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            isPaused = !isPaused;
            timeManager.SetPaused(isPaused);
            pauseController.SetVisible(isPaused);

            var cue = isPaused ? pauseCue : resumeCue;
            audioPlayer.PlayCue(cue);

            stateTracker.SetState(isPaused ? GameState.Paused : GameState.Playing);
        }

        public void TriggerGameOver()
        {
            stateTracker.SetState(GameState.GameOver);
            timeManager.SetPaused(true);
            pauseController.ShowGameOverUI();
            audioPlayer.PlayCue(AudioCue.GameOver);
        }

        public void LoadScene(string sceneName)
        {
            sceneLoader.LoadSceneAsync(sceneName);
        }
    }
}
