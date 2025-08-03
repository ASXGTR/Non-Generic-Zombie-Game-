using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;

    public Image fadeOverlay;
    public float fadeDuration = 1f;

    private bool globalFadingEnabled = false;

    void Awake()
    {
        // ✅ Debug log to check if this is a root GameObject
        if (transform.parent != null)
        {
            Debug.LogError("❌ FadeController: SceneTransitionManager is still PARENTED to " + transform.parent.name);
        }
        else
        {
            Debug.Log("✅ FadeController: SceneTransitionManager is a ROOT GameObject.");
        }

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("📌 FadeController initialized in scene: " + currentScene);

        // ✅ Always enable global fading once
        EnableGlobalFading();

        if (fadeOverlay == null)
        {
            Debug.LogError("❌ FadeController: fadeOverlay is not assigned in the Inspector!");
        }

        StartCoroutine(FadeIn());
    }

    public void StartGameWithFade(string sceneName)
    {
        Debug.Log("🚨 BUTTON CLICKED — Attempting scene transition.");

        if (fadeOverlay == null)
        {
            Debug.LogError("❌ FadeController: fadeOverlay is null. Did you assign it in the Inspector?");
            return;
        }

        Debug.Log("✅ StartGameWithFade triggered. Loading scene: " + sceneName);
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeIn()
    {
        Debug.Log("🌓 FadeIn() coroutine started.");

        float t = fadeDuration;
        Color c = fadeOverlay.color;
        while (t > 0)
        {
            t -= Time.deltaTime;
            c.a = Mathf.Clamp01(t / fadeDuration);
            fadeOverlay.color = c;
            yield return null;
        }

        Debug.Log("✅ FadeIn complete.");
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        float t = 0f;
        Color c = fadeOverlay.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Clamp01(t / fadeDuration);
            fadeOverlay.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (fadeOverlay != null && globalFadingEnabled)
        {
            Debug.Log("🎬 Scene loaded: " + scene.name + " — triggering FadeIn()");
            StartCoroutine(FadeIn());
        }
    }

    private void EnableGlobalFading()
    {
        if (!globalFadingEnabled)
        {
            globalFadingEnabled = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("🌍 Global fading enabled for future scenes.");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
