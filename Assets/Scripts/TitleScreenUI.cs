using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreenUI : MonoBehaviour
{
    [Header("Scene Settings")]
    public string gameSceneName = "MainScene";

    [Header("Title Screen UI")]
    public GameObject gameTitle;
    public GameObject newGameButton;
    public GameObject creditsButton;

    [Header("Credits UI")]
    public GameObject creditsText;
    public GameObject creditsPanel;
    public GameObject returnButton;
    public GameObject creditsTextOverlay; // ✅ Separate overlay that stays visible

    [Header("Audio")]
    public AudioSource backgroundMusic;

    private TMP_Text creditsButtonText;

    void Start()
    {
        // Cache TMP_Text reference
        if (creditsButton != null)
            creditsButtonText = creditsButton.GetComponentInChildren<TMP_Text>(true);

        InitializeUIState();
    }

    public void StartGame()
    {
        Debug.Log("🎮 Starting game...");

        if (backgroundMusic != null)
            StartCoroutine(FadeOutMusic(3.5f)); // Matches UI fade time

        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("🛑 Quit (Editor)");
#else
        Application.Quit();
        Debug.Log("🛑 Quit (Build)");
#endif
    }

    public void ShowCredits()
    {
        gameTitle?.SetActive(false);
        newGameButton?.SetActive(false);
        creditsButton?.SetActive(false);

        creditsText?.SetActive(true);
        creditsPanel?.SetActive(true);
        returnButton?.SetActive(true);

        // ✅ Make sure overlay remains untouched here
        Debug.Log("📜 Credits shown");
    }

    public void ReturnToTitle()
    {
        creditsText?.SetActive(false);
        creditsPanel?.SetActive(false);
        returnButton?.SetActive(false);

        ShowTitleUI();

        Debug.Log("🏠 Returned to title");
    }

    private void ShowTitleUI()
    {
        gameTitle?.SetActive(true);
        newGameButton?.SetActive(true);

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

        // ✅ Ensure creditsTextOverlay always stays active
        if (creditsTextOverlay != null)
            creditsTextOverlay.SetActive(true);
    }

    private void InitializeUIState()
    {
        ShowTitleUI();

        creditsText?.SetActive(false);
        creditsPanel?.SetActive(false);
        returnButton?.SetActive(false);

        // ✅ Always ensure overlay is visible on load
        if (creditsTextOverlay != null)
            creditsTextOverlay.SetActive(true);
    }

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
        backgroundMusic.volume = startVolume; // Reset volume for future use
    }
}
