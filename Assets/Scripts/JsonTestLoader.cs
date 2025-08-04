using UnityEngine;

/// <summary>
/// Loads and optionally previews a JSON file from the Resources folder for testing or debugging.
/// </summary>
public class JsonTestLoader : MonoBehaviour
{
    [Header("🔍 JSON Path inside Resources")]
    [Tooltip("Do not include .json extension")]
    public string jsonPath = "Nodes/shoreline_wake_up";

    private void Start()
    {
        LoadJson();
    }

    /// <summary>
    /// Loads the JSON file from Resources and logs its contents.
    /// </summary>
    [ContextMenu("🔄 Load JSON Now")]
    public void LoadJson()
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

            // Trim preview (only first 300 characters for huge files)
            string preview = jsonFile.text.Length > 300
                ? jsonFile.text.Substring(0, 300) + "...\n(Preview truncated)"
                : jsonFile.text;

            Debug.Log($"🧾 JSON Preview:\n{preview}");

            // Optional: deserialize logic scaffold
            // var yourData = JsonUtility.FromJson<YourDataType>(jsonFile.text);
        }
        else
        {
            Debug.LogError($"❌ Failed to load JSON at: Resources/{jsonPath}.json");
        }
    }
}
