using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems
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

    // 🧩 Stubbed Types (replace with real ones when available)
    public class GameStateTracker : MonoBehaviour
    {
        public void SetState(GameState state) { }
    }

    public enum GameState { Playing, Paused, GameOver }

    public class TimeManager : MonoBehaviour
    {
        public void SetPaused(bool paused) { }
    }

    public class SceneLoader : MonoBehaviour
    {
        public void LoadSceneAsync(string sceneName) { SceneManager.LoadScene(sceneName); }
    }

    public class AudioCuePlayer : MonoBehaviour
    {
        public void PlayCue(AudioCue cue) { }
    }

    public class AudioCue
    {
        public static AudioCue GameOver => new AudioCue();
    }

    public class PauseController : MonoBehaviour
    {
        public void SetVisible(bool visible) { }
        public void ShowGameOverUI() { }
    }
}
