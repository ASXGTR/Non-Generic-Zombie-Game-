using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;

public class HotFoodTracker : MonoBehaviour
{
    public List<Item> hotFoodItems = new List<Item>();

    void Update()
    {
        for (int i = hotFoodItems.Count - 1; i >= 0; i--)
        {
            Item item = hotFoodItems[i];

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
                Debug.Log($"{item.itemName} has cooled down.");
            }
        }
    }

    public void AddHotFood(Item item, int duration)
    {
        if (!hotFoodItems.Contains(item))
        {
            item.isHot = true;
            item.hotDuration = duration;
            hotFoodItems.Add(item);
            Debug.Log($"{item.itemName} is now hot for {duration} turns.");
        }
    }
}
