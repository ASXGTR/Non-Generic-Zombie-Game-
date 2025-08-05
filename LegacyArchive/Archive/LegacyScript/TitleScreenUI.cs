// ARCHIVED SCRIPT — DO NOT MODIFY OR INCLUDE IN BUILD
// Original: TitleScreenUI.cs
// Role: Initial title screen controller — buttons, audio, transitions
// Status: Replaced by modular UI components:
//   - TitleScreenUI.cs       (UI/TitleScreen/TitleScreenUI.cs)
//   - CreditsScreenUI.cs     (UI/TitleScreen/CreditsScreenUI.cs)
// Date Archived: 2025-08-05
// Notes:
//   - All button and transition logic split for clarity
//   - AudioCue enum now scoped under Survival.Audio
//   - Credits handling modularized for future extensibility
//   - Recommend prefab remap and scene link verification
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Handles title screen buttons and transitions.
/// </summary>
public class TitleScreenUI : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "MainScene";

    [Header("Title Screen UI")]
    [SerializeField] private GameObject gameTitle;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject creditsButton;

    [Header("Credits UI")]
    [SerializeField] private GameObject creditsText;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject returnButton;
    [SerializeField] private GameObject creditsTextOverlay; // ✅ persistent overlay

    [Header("Audio")]
    [SerializeField] private AudioSource backgroundMusic;

    private TMP_Text creditsButtonText;
    private const string logTag = "[TitleScreenUI]";

    private void Start()
    {
        if (creditsButton != null)
            creditsButtonText = creditsButton.GetComponentInChildren<TMP_Text>(true);

        InitializeUIState();
    }

    /// <summary>
    /// Starts the game and transitions scene.
    /// </summary>
    [ContextMenu("▶ Start Game")]
    private void StartGame()
    {
        Debug.Log($"{logTag} 🎮 Starting game...");

        if (backgroundMusic != null)
            StartCoroutine(FadeOutMusic(3.5f));

        if (!string.IsNullOrEmpty(gameSceneName))
            SceneManager.LoadScene(gameSceneName);
        else
            Debug.LogError($"{logTag} ❌ Scene name is empty.");
    }

    /// <summary>
    /// Quits the game from title screen.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log($"{logTag} 🛑 Quit (Editor)");
#else
        Application.Quit();
        Debug.Log($"{logTag} 🛑 Quit (Build)");
#endif
    }

    /// <summary>
    /// Shows credits panel.
    /// </summary>
    public void ShowCredits()
    {
        if (gameTitle != null) gameTitle.SetActive(false);
        if (newGameButton != null) newGameButton.SetActive(false);
        if (creditsButton != null) creditsButton.SetActive(false);

        if (creditsText != null) creditsText.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(true);
        if (returnButton != null) returnButton.SetActive(true);
        if (creditsTextOverlay != null) creditsTextOverlay.SetActive(true);

        Debug.Log($"{logTag} 📜 Credits shown.");
    }

    /// <summary>
    /// Returns to the main title UI.
    /// </summary>
    public void ReturnToTitle()
    {
        if (creditsText != null) creditsText.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (returnButton != null) returnButton.SetActive(false);

        ShowTitleUI();

        Debug.Log($"{logTag} 🏠 Returned to title.");
    }

    /// <summary>
    /// Displays title buttons and logo.
    /// </summary>
    private void ShowTitleUI()
    {
        if (gameTitle != null) gameTitle.SetActive(true);
        if (newGameButton != null) newGameButton.SetActive(true);

        if (creditsButton != null)
        {
            creditsButton.SetActive(true);
            if (creditsButtonText != null)
            {
                creditsButtonText.text = "Credits";
                creditsButtonText.enabled = true;
                creditsButtonText.gameObject.SetActive(true);
            }
        }

        if (creditsTextOverlay != null)
            creditsTextOverlay.SetActive(true);
    }

    /// <summary>
    /// Initializes title screen state on game launch.
    /// </summary>
    private void InitializeUIState()
    {
        ShowTitleUI();

        if (creditsText != null) creditsText.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (returnButton != null) returnButton.SetActive(false);
        if (creditsTextOverlay != null)
            creditsTextOverlay.SetActive(true);
    }

    /// <summary>
    /// Fades out background music.
    /// </summary>
    private System.Collections.IEnumerator FadeOutMusic(float duration)
    {
        if (backgroundMusic == null || !backgroundMusic.isPlaying)
            yield break;

        float startVolume = backgroundMusic.volume;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            backgroundMusic.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        backgroundMusic.Stop();
        backgroundMusic.volume = startVolume;
    }
}
