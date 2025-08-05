// ARCHIVED SCRIPT — DO NOT MODIFY OR INCLUDE IN BUILD
// Original: NodeLoader.cs
// Role: Legacy narrative node loader and branching logic handler
// Status: Replaced by modular systems:
//   - NarrativeCoordinator.cs       (Systems/Narrative/Core)
//   - NarrativeNodeParser.cs        (Systems/Narrative/Parsing)
//   - ChoiceResolver.cs             (Systems/Narrative/Logic)
//   - TriggerEvaluator.cs           (Systems/Narrative/Triggers)
//   - DialogueUIController.cs       (UI/Narrative)
// Date Archived: 2025-08-05
// Runtime: This script should NOT be executed at runtime.
// Notes:
//   - All branching, condition checks, and UI calls modularized
//   - Editor debug logic moved to NodeDebugUtility.cs
//   - Retained for audit and migration traceability only
using UnityEngine;

/// <summary>
/// Loads a story node JSON from the Resources/Nodes/ folder by ID.
/// </summary>
public static class NodeLoader
{
    /// <summary>
    /// Loads and deserializes a StoryNode from Resources/Nodes/{nodeId}.json.
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

        StoryNode node;
        try
        {
            node = JsonUtility.FromJson<StoryNode>(nodeFile.text);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"❌ NodeLoader: Exception while parsing node '{nodeId}': {ex.Message}");
            return null;
        }

        if (node == null)
        {
            Debug.LogError($"❌ NodeLoader: Failed to deserialize JSON for nodeId: {nodeId}");
            return null;
        }

        Debug.Log($"📘 NodeLoader: Successfully loaded node '{nodeId}'.");
        return node;
    }
}
