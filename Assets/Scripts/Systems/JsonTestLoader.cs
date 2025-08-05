using UnityEngine;
using Systems.Serialization;
using Tools.Paths;
using Tools.Diagnostics;
using DataModels;

public class JsonTestLoader : MonoBehaviour
{
    void Start()
    {
        string path = PathResolver.StreamingAssets("test.json");
        string json = JsonLoader.Load(path);

        if (string.IsNullOrEmpty(json))
        {
            DebugLogger.Warn("Test JSON is empty or missing.");
            return;
        }

        TestData data = JsonDeserializer.FromJson<TestData>(json);
        DebugLogger.Info($"Loaded TestData: {data.name}, {data.description}, {data.value}");
    }
}
