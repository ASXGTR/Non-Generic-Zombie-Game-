using UnityEngine;

public class JsonTestLoader : MonoBehaviour
{
    [Header("🔍 JSON Path inside Resources")]
    [Tooltip("Do not include .json extension")]
    public string jsonPath = "Nodes/shoreline_wake_up";

    void Start()
    {
        LoadJson();
    }

    void LoadJson()
    {
        if (string.IsNullOrEmpty(jsonPath))
        {
            Debug.LogError("❌ JSON path is empty or null!");
            return;
        }

        TextAsset jsonFile = Resources.Load<TextAsset>(jsonPath);

        if (jsonFile != null)
        {
            Debug.Log($"✅ Loaded JSON from: Resources/{jsonPath}.json");
            Debug.Log($"🧾 JSON Content:\n{jsonFile.text}");

            // Optional: Deserialize here later
            // var yourData = JsonUtility.FromJson<YourDataType>(jsonFile.text);
        }
        else
        {
            Debug.LogError($"❌ Failed to load JSON at: Resources/{jsonPath}.json");
        }
    }
}
