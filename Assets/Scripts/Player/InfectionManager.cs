using Core.Shared.Models;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    public HashSet<InfectionFlags> activeInfections = new();

    public void AddInfection(InfectionFlags flag)
    {
        activeInfections.Add(flag);
        Debug.Log("Infection added: " + flag);
    }

    public void RemoveInfection(InfectionFlags flag)
    {
        if (activeInfections.Contains(flag))
        {
            activeInfections.Remove(flag);
            Debug.Log("Infection removed: " + flag);
        }
    }

    public bool HasInfection(InfectionFlags flag) => activeInfections.Contains(flag);
}
