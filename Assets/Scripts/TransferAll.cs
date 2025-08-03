using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GearTransferManager : MonoBehaviour
{
    [Header("Transfer Events")]
    public UnityEvent<Item, Item> onItemTransferred;

    /// <summary>
    /// Transfers all items from one gear container to another.
    /// </summary>
    public void TransferAll(Item sourceGear, Item targetGear)
    {
        if (sourceGear == null || targetGear == null)
        {
            Debug.LogWarning("[TransferAll] Source or target gear is null.");
            return;
        }

        if (sourceGear.internalStorage == null || targetGear.internalStorage == null)
        {
            Debug.LogWarning("[TransferAll] One of the gear containers is missing internal storage.");
            return;
        }

        if (targetGear.internalStorage.Count >= targetGear.storageCapacity)
        {
            Debug.LogWarning($"[TransferAll] {targetGear.itemName} is already full.");
            return;
        }

        int transferred = 0;

        // Copy list to avoid mutation issues
        var itemsToMove = new List<Item>(sourceGear.internalStorage);

        foreach (Item item in itemsToMove)
        {
            if (!item.CanBeStored())
            {
                Debug.LogWarning($"[TransferAll] Cannot transfer {item.itemName} — it contains items.");
                continue;
            }

            if (targetGear.internalStorage.Count >= targetGear.storageCapacity)
            {
                Debug.LogWarning($"[TransferAll] {targetGear.itemName} is full. Stopping transfer.");
                break;
            }

            targetGear.internalStorage.Add(item);
            sourceGear.internalStorage.Remove(item);
            transferred++;

            // Fire event hook
            onItemTransferred?.Invoke(item, targetGear);
        }

        Debug.Log($"[TransferAll] Moved {transferred} items from {sourceGear.itemName} to {targetGear.itemName}.");
    }
}
