using UnityEngine;

public class GlobalGameStateHolder : MonoBehaviour
{
    public static GlobalGameStateHolder Instance;

    public string lastNodeId = "shoreline_wake_up";
    public CampExitResult campExitResult = CampExitResult.None;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum CampExitResult
    {
        None,
        SleptWell,
        InterruptedSleep,
        LeftCamp
    }
}
