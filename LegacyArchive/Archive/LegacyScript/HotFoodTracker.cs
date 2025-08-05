// ARCHIVED SCRIPT — DO NOT MODIFY OR INCLUDE IN BUILD
// Original: HotFoodTracker.cs
// Role: Legacy handler for temperature-based food tracking (heat state, decay, buffs)
// Status: Replaced by modular systems:
//   - FoodStateTracker.cs     (Systems/Inventory/Food/Components)
//   - TemperatureDecay.cs     (Systems/Environment/Temperature)
//   - HeatBonusApplier.cs     (Systems/Stats/Modifiers)
//   - FoodTemperatureManager.cs (Systems/Inventory/Food/Controllers)
// Date Archived: 2025-08-05
// Runtime: This script should NOT be executed at runtime.
// Notes:
//   - All prefab references have migrated to modular components
//   - Formerly linked temperature to ItemType and applied buffs directly
//   - Obsolete due to clean separation of tracking, decay, and stat logic
//   - Retained for audit and migration traceability only

using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// Tracks hot food items and cools them over time.
/// </summary>
public class HotFoodTracker : MonoBehaviour
{
    public List<Item> hotFoodItems = new();

    void Update()
    {
        TickHeat(); // Could be replaced with turn event trigger later
    }

    /// <summary>
    /// Handles per-frame or per-turn cooldown on hot items.
    /// </summary>
    public void TickHeat()
    {
        for (int i = hotFoodItems.Count - 1; i >= 0; i--)
        {
            Item item = hotFoodItems[i];
            if (item == null || item.data == null)
            {
                hotFoodItems.RemoveAt(i);
                continue;
            }

            if (!item.isHot)
            {
                hotFoodItems.RemoveAt(i);
                continue;
            }

            item.hotDuration--;

            if (item.hotDuration <= 0)
            {
                item.isHot = false;
                hotFoodItems.RemoveAt(i);
                Debug.Log($"🌡️ {item.itemName} has cooled down.");
            }
        }
    }

    /// <summary>
    /// Marks an item as hot and tracks its remaining duration.
    /// </summary>
    public void AddHotFood(Item item, int duration)
    {
        if (item == null || hotFoodItems.Contains(item)) return;

        item.isHot = true;
        item.hotDuration = duration;
        hotFoodItems.Add(item);

        Debug.Log($"🔥 {item.itemName} is now hot for {duration} turns.");
    }
}
