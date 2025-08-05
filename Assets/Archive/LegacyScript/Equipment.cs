// 🔒 Deprecated Equipment.cs
// Logic merged into EquipmentManager.cs on 2025-08-05
// Retained for reference only. Do not use in runtime.


using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// Handles character equipment logic for clothing and gear.
/// </summary>
public class Equipment : MonoBehaviour
{
    [Tooltip("Current equipped items keyed by clothing slot.")]
    private readonly Dictionary<ClothingSlot, Item> equippedItems = new();

    /// <summary>
    /// Attempts to equip the provided clothing item to its designated slot.
    /// </summary>
    public bool EquipItem(Item item)
    {
        if (item == null || item.data == null)
        {
            Debug.LogWarning("[Equipment] Cannot equip null or invalid item.");
            return false;
        }

        if (item.itemType != ItemType.Clothing)
        {
            Debug.LogWarning($"[Equipment] Item '{item.ItemName}' is not clothing.");
            return false;
        }

        ClothingSlot slot = item.clothingSlot;

        if (equippedItems.ContainsKey(slot))
        {
            Debug.Log($"[Equipment] Replacing '{equippedItems[slot].ItemName}' in {slot} with '{item.ItemName}'.");
            equippedItems[slot] = item;
        }
        else
        {
            Debug.Log($"[Equipment] Equipped '{item.ItemName}' in slot {slot}.");
            equippedItems.Add(slot, item);
        }

        // TODO: Trigger UI update, save state, or stat recalculation
        return true;
    }

    /// <summary>
    /// Unequips the item from the specified slot, if any.
    /// </summary>
    public bool UnequipItem(ClothingSlot slot)
    {
        if (equippedItems.ContainsKey(slot))
        {
            Debug.Log($"[Equipment] Unequipped '{equippedItems[slot].ItemName}' from slot {slot}.");
            equippedItems.Remove(slot);

            // TODO: Trigger stat removal, drop to inventory, etc.
            return true;
        }

        Debug.LogWarning($"[Equipment] No item in slot {slot} to unequip.");
        return false;
    }

    /// <summary>
    /// Returns the item equipped in the given slot, if any.
    /// </summary>
    public Item GetEquippedItem(ClothingSlot slot)
    {
        equippedItems.TryGetValue(slot, out Item item);
        return item;
    }

    /// <summary>
    /// Returns all currently equipped items (read-only).
    /// </summary>
    public IReadOnlyDictionary<ClothingSlot, Item> GetAllEquippedItems()
    {
        return equippedItems;
    }

    /// <summary>
    /// Clears all equipped items (for reset, respawn, etc.)
    /// </summary>
    public void ClearAllEquipment()
    {
        equippedItems.Clear();
        // TODO: Notify UI, refresh visuals, update stats
    }

    /// <summary>
    /// Checks if a slot is currently occupied.
    /// </summary>
    public bool IsSlotOccupied(ClothingSlot slot) => equippedItems.ContainsKey(slot);
}
