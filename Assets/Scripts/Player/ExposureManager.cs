using System.Collections.Generic;
using UnityEngine;

public class ExposureManager : MonoBehaviour
{
    public HashSet<ExposureFlags> activeExposureStates = new();

    public void AddExposure(ExposureFlags flag)
    {
        activeExposureStates.Add(flag);
        Debug.Log("Exposure added: " + flag);
    }

    public void RemoveExposure(ExposureFlags flag)
    {
        if (activeExposureStates.Contains(flag))
        {
            activeExposureStates.Remove(flag);
            Debug.Log("Exposure removed: " + flag);
        }
    }

    public bool HasExposure(ExposureFlags flag) => activeExposureStates.Contains(flag);
}
