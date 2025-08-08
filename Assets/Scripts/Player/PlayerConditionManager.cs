using UnityEngine;
using Flags;

public class PlayerConditionManager : MonoBehaviour
{
    /// <summary>
    /// Bitmask representing all current player conditions (physical + mental).
    /// </summary>
    [Tooltip("Current bitwise condition state of the player.")]
    public PlayerConditionFlags currentConditions = PlayerConditionFlags.None;

    /// <summary>
    /// Adds a condition to the current state.
    /// </summary>
    public void AddCondition(PlayerConditionFlags condition)
    {
        if (!HasCondition(condition))
        {
            currentConditions |= condition;
            Debug.Log("Condition added: " + condition);
        }
    }

    /// <summary>
    /// Removes a condition from the current state.
    /// </summary>
    public void RemoveCondition(PlayerConditionFlags condition)
    {
        if (HasCondition(condition))
        {
            currentConditions &= ~condition;
            Debug.Log("Condition removed: " + condition);
        }
    }

    /// <summary>
    /// Checks if a specific condition is active.
    /// </summary>
    public bool HasCondition(PlayerConditionFlags condition)
    {
        return (currentConditions & condition) != 0;
    }

    /// <summary>
    /// Clears all conditions.
    /// </summary>
    public void ResetConditions()
    {
        currentConditions = PlayerConditionFlags.None;
        Debug.Log("All conditions reset.");
    }

    /// <summary>
    /// Logs all active conditions for debugging.
    /// </summary>
    public void LogConditions()
    {
        Debug.Log("Current conditions: " + currentConditions);
    }
}
