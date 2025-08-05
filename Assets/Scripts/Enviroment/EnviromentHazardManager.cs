using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHazardManager : MonoBehaviour
{
    public HashSet<EnvironmentHazardFlags> activeHazards = new();

    public void AddHazard(EnvironmentHazardFlags hazard)
    {
        activeHazards.Add(hazard);
        Debug.Log("Hazard detected: " + hazard);
    }

    public void RemoveHazard(EnvironmentHazardFlags hazard)
    {
        if (activeHazards.Contains(hazard))
        {
            activeHazards.Remove(hazard);
            Debug.Log("Hazard resolved: " + hazard);
        }
    }

    public bool HasHazard(EnvironmentHazardFlags hazard) => activeHazards.Contains(hazard);
}
