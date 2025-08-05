using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public interface IFadeOverlay
{
    void SetAlpha(float alpha);
    Color CurrentColor { get; }
}

[RuntimeService]
public class SceneTransitionService : ISceneTransitionResponder
{
    private readonly IFadeOverlay fadeOverlay;
    private readonly float fadeDuration;

    public SceneTransitionService(IFadeOverlay overlay, float duration = 1f)
    {
        fadeOverlay = overlay;
        fadeDuration = duration;
    }

    public async Task FadeInAsync()
    {
        Debug.Log("[SceneTransitionService] Begin fade-in.");
        float timer = fadeDuration;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, 1f - (timer / fadeDuration));
            fadeOverlay.SetAlpha(alpha);
            await Task.Yield();
        }

        fadeOverlay.SetAlpha(0f);
        Debug.Log("[SceneTransitionService] Fade-in complete.");
    }

    public async Task FadeOutAndLoadAsync(string sceneName)
    {
        Debug.Log($"[SceneTransitionService] Begin fade-out to scene: {sceneName}");
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeOverlay.SetAlpha(alpha);
            await Task.Yield();
        }

        fadeOverlay.SetAlpha(1f);
        Debug.Log("[SceneTransitionService] Fade-out complete. Loading scene...");
        SceneManager.LoadScene(sceneName);
    }

    public async void OnSceneLoaded(string sceneName)
    {
        Debug.Log($"[SceneTransitionService] Scene loaded: {sceneName}. Triggering auto fade-in.");
        await FadeInAsync();
    }
}
