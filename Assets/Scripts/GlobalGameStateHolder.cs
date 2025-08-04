using Game.Stats;
using UnityEngine;

/// <summary>
/// Persisted game state holder across scenes — camp exit tracking, stat snapshots, etc.
/// </summary>
public class GlobalGameStateHolder : MonoBehaviour
{
    public static GlobalGameStateHolder Instance;

    [Header("Narrative State")]
    public string lastNodeId = "shoreline_wake_up";
    public CampExitResult campExitResult = CampExitResult.None;

    [Header("Player Stats on Camp Exit")]
    public CampExitStats lastKnownStats;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Possible outcomes when exiting camp.
    /// </summary>
    public enum CampExitResult
    {
        None,
        SleptWell,
        InterruptedSleep,
        LeftCamp
    }

    /// <summary>
    /// Stores a snapshot of player stats from the last camp scene exit.
    /// </summary>
    [System.Serializable]
    public class CampExitStats
    {
        public float health;
        public float stamina;
        public float hunger;
        public float hydration;
        public float temperature;
        public float sickness;

        public CampExitStats() { }

        public CampExitStats(PlayerStats stats)
        {
            health = stats.health;
            stamina = stats.stamina;
            hunger = stats.hunger;
            hydration = stats.hydration;
            temperature = stats.temperature;
            sickness = stats.sickness;
        }
    }

    /// <summary>
    /// Call this before leaving camp to snapshot player stats.
    /// </summary>
    public void SaveStatsFrom(PlayerStats stats)
    {
        if (stats == null) return;
        lastKnownStats = new CampExitStats(stats);
        Debug.Log("[GlobalGameStateHolder] Saved camp exit stats.");
    }
}
