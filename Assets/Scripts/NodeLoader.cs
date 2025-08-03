using UnityEngine;

public static class NodeLoader
{
    /// <summary>
    /// Loads a story node JSON from the Resources/Nodes/ folder by ID.
    /// </summary>
    /// <param name="nodeId">The name of the JSON file (without .json extension).</param>
    /// <returns>Deserialized StoryNode object or null on failure.</returns>
    public static StoryNode LoadNode(string nodeId)
    {
        if (string.IsNullOrEmpty(nodeId))
        {
            Debug.LogError("❌ NodeLoader: nodeId is null or empty.");
            return null;
        }

        string resourcePath = $"Nodes/{nodeId}";
        TextAsset nodeFile = Resources.Load<TextAsset>(resourcePath);

        if (nodeFile == null)
        {
            Debug.LogError($"❌ NodeLoader: Node file not found at Resources/{resourcePath}.json");
            return null;
        }

        try
        {
            StoryNode node = JsonUtility.FromJson<StoryNode>(nodeFile.text);

            if (node == null)
            {
                Debug.LogError($"❌ NodeLoader: Failed to deserialize JSON for nodeId: {nodeId}");
                return null;
            }

            Debug.Log($"📘 NodeLoader: Successfully loaded node '{nodeId}'.");
            return node;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"❌ NodeLoader: Exception while parsing node '{nodeId}': {ex.Message}");
            return null;
        }
    }
}
