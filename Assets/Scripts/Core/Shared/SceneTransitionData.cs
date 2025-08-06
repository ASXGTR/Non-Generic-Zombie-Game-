using UnityEngine;

public interface IFadeOverlay
{
    void SetAlpha(float alpha);
    Color CurrentColor { get; }
}

// Optional: include this if it's part of the shared contract
public interface ISceneTransitionResponder
{
    void OnSceneLoaded(string sceneName);
}
