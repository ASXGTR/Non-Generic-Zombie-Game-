using System.Collections.Generic;
using UnityEngine;

public class MentalStateManager : MonoBehaviour
{
    public HashSet<MentalStateFlags> activeStates = new();

    public void AddState(MentalStateFlags state)
    {
        activeStates.Add(state);
        Debug.Log("Mental state added: " + state);
    }

    public void RemoveState(MentalStateFlags state)
    {
        if (activeStates.Contains(state))
        {
            activeStates.Remove(state);
            Debug.Log("Mental state removed: " + state);
        }
    }

    public bool HasState(MentalStateFlags state) => activeStates.Contains(state);
}
