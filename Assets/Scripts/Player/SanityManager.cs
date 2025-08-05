using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    public HashSet<SanityFlags> activeSanityEvents = new();

    public void AddEvent(SanityFlags flag)
    {
        activeSanityEvents.Add(flag);
        Debug.Log("Sanity event triggered: " + flag);
    }

    public void RemoveEvent(SanityFlags flag)
    {
        if (activeSanityEvents.Contains(flag))
        {
            activeSanityEvents.Remove(flag);
            Debug.Log("Sanity event resolved: " + flag);
        }
    }

    public bool HasEvent(SanityFlags flag) => activeSanityEvents.Contains(flag);
}
