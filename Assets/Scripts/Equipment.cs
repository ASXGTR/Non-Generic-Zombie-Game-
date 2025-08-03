using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;

public class Equipment : MonoBehaviour
{
    private Dictionary<ClothingSlot, Item> equippedItems = new Dictionary<ClothingSlot, Item>();

    public bool EquipItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("Cannot equip null item.");
            return false;
        }

        if (item.itemType != ItemType.Clothing)
        {
            Debug.LogWarning($"Cannot equip item '{item.itemName}', it is not clothing.");
            return false;
        }

        ClothingSlot slot = item.clothingSlot;

        if (equippedItems.ContainsKey(slot))
        {
            Debug.Log($"Replacing equipped item '{equippedItems[slot].itemName}' in slot {slot} with '{item.itemName}'.");
            equippedItems[slot] = item;
        }
        else
        {
            Debug.Log($"Equipped '{item.itemName}' in slot {slot}.");
            equippedItems.Add(slot, item);
        }

        return true;
    }

    public bool UnequipItem(ClothingSlot slot)
    {
        if (equippedItems.ContainsKey(slot))
        {
            Debug.Log($"Unequipped '{equippedItems[slot].itemName}' from slot {slot}.");
            equippedItems.Remove(slot);
            return true;
        }
        else
        {
            Debug.LogWarning($"No item equipped in slot {slot} to unequip.");
            return false;
        }
    }

    public Item GetEquippedItem(ClothingSlot slot)
    {
        equippedItems.TryGetValue(slot, out Item item);
        return item;
    }

    public IReadOnlyDictionary<ClothingSlot, Item> GetAllEquippedItems()
    {
        return equippedItems;
    }
}
