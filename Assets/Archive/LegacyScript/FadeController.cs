// 🗂️ ARCHIVED: Legacy/FadeController.cs
// 🔄 Migrated to: Systems/Scenes/Transition/SceneTransitionService.cs
// 🧠 Purpose: Scene-wide fade transitions using UI overlay opacity.
// 📆 Archived: 2025-08-05
// ⚠️ Notes: Transition logic now async and modular. Overlay injected. Scene events delegated.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Controls screen fade transitions between scenes.
/// </summary>
public class FadeController : MonoBehaviour
{
    public static FadeController instance;

    [Header("Fade Settings")]
    public Image fadeOverlay;
    public float fadeDuration = 1f;

    private bool globalFadingEnabled = false;
    private Coroutine currentFade;

    void Awake()
    {
        if (transform.parent != null)
            Debug.LogError("[FadeController] Should be root-level, but parented to: " + transform.parent.name);
        else
            Debug.Log("[FadeController] Correctly positioned at scene root.");

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
        if (fadeOverlay == null)
        {
            Debug.LogError("[FadeController] Missing fadeOverlay reference.");
            return;
        }

        EnableGlobalFading();
        StartCoroutine(FadeIn());
    }

    public void StartGameWithFade(string sceneName)
    {
        if (fadeOverlay == null)
        {
            Debug.LogError("[FadeController] Cannot fade — overlay is null.");
            return;
        }

        Debug.Log($"[FadeController] Starting fade to scene: {sceneName}");
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeIn()
    {
        Debug.Log("[FadeController] Begin fade-in.");
        Color c = fadeOverlay.color;
        float timer = fadeDuration;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, 1f - (timer / fadeDuration));
            fadeOverlay.color = c;
            yield return null;
        }

        c.a = 0f;
        fadeOverlay.color = c;
        Debug.Log("[FadeController] Fade-in complete.");
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        Debug.Log("[FadeController] Begin fade-out.");
        Color c = fadeOverlay.color;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeOverlay.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeOverlay.color = c;
        Debug.Log("[FadeController] Fade-out complete. Loading scene...");
        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (fadeOverlay != null && globalFadingEnabled)
        {
            Debug.Log($"[FadeController] Scene loaded: {scene.name}. Triggering auto fade-in.");
            StartCoroutine(FadeIn());
        }
    }

    void EnableGlobalFading()
    {
        if (!globalFadingEnabled)
        {
            globalFadingEnabled = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("[FadeController] Global scene fade listening enabled.");
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Debug.Log("[FadeController] Cleaned up fade listener.");
        }
    }
}
