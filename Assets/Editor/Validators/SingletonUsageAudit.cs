using UnityEngine;
using Tools.Logging;
using Systems.Core;

public class SingletonUsageAudit : MonoBehaviour
{
    void Start()
    {
        var singletons = SingletonRegistry.ScanActiveScene();
        foreach (var s in singletons)
        {
            DebugLogger.Log($"Singleton detected: {s.GetType().Name} on {s.gameObject.name}");
        }
    }
}
