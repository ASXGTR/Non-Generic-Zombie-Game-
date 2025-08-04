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
