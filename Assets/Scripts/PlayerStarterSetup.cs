using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// Sets up the player's starting inventory on game start or manually via inspector.
/// </summary>
public class PlayerStarterSetup : MonoBehaviour
{
    [Header("Starter Items")]
    [SerializeField] private List<ItemData> starterItems;

    [Header("Player Inventory")]
    [SerializeField] private Inventory playerInventory;

    private const string logTag = "[PlayerStarterSetup]";

    private void Start()
    {
        ApplyStarterItems();
    }

    /// <summary>
    /// Adds tagged "starter" items to the player's inventory.
    /// </summary>
    [ContextMenu("Add Starter Items")]
    public void ApplyStarterItems()
    {
        if (playerInventory == null)
        {
            Debug.LogWarning($"{logTag} No inventory assigned.");
            return;
        }

        if (starterItems == null || starterItems.Count == 0)
        {
            Debug.LogWarning($"{logTag} No starter items defined.");
            return;
        }

        int added = 0;
        int skipped = 0;

        foreach (ItemData itemData in starterItems)
        {
            if (itemData == null)
            {
                Debug.LogWarning($"{logTag} Null ItemData reference in starter list.");
                continue;
            }

            string name = itemData.ItemName ?? "(Unnamed)";
            List<string> tags = itemData.Tags;

            if (tags != null && tags.Contains("starter"))
            {
                var newItem = new Item(itemData); // Assumes constructor signature
                playerInventory.AddItem(newItem);
                Debug.Log($"{logTag} ➕ Added starter item: {name}");
                added++;
            }
            else
            {
                Debug.Log($"{logTag} ⚠️ Skipped non-starter item: {name}");
                skipped++;
            }
        }

        Debug.Log($"{logTag} Summary — Added: {added}, Skipped: {skipped}");
    }
}
