using UnityEngine;

namespace Systems
{
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

    // 🧩 Stubbed Dependencies

    public static class PathResolver
    {
        public static string StreamingAssets(string filename) =>
            System.IO.Path.Combine(Application.streamingAssetsPath, filename);
    }

    public static class JsonLoader
    {
        public static string Load(string path)
        {
            if (!System.IO.File.Exists(path)) return null;
            return System.IO.File.ReadAllText(path);
        }
    }

    public static class JsonDeserializer
    {
        public static T FromJson<T>(string json) => JsonUtility.FromJson<T>(json);
    }

    public static class DebugLogger
    {
        public static void Info(string message) => Debug.Log($"[INFO] {message}");
        public static void Warn(string message) => Debug.LogWarning($"[WARN] {message}");
    }

    [System.Serializable]
    public class TestData
    {
        public string name;
        public string description;
        public int value;
    }
}
