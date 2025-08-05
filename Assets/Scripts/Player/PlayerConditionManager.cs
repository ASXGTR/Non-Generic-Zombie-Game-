using System.Collections.Generic;
using UnityEngine;

public class PlayerConditionManager : MonoBehaviour
{
    public HashSet<PlayerConditionFlags> activeConditions = new();

    public void AddCondition(PlayerConditionFlags condition)
    {
        activeConditions.Add(condition);
        Debug.Log("Condition added: " + condition);
    }

    public void RemoveCondition(PlayerConditionFlags condition)
    {
        if (activeConditions.Contains(condition))
        {
            activeConditions.Remove(condition);
            Debug.Log("Condition removed: " + condition);
        }
    }

    public bool HasCondition(PlayerConditionFlags condition) => activeConditions.Contains(condition);
}
