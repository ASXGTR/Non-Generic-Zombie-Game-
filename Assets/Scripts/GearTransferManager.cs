using Game.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Inventory
{
    /// <summary>
    /// Manages bulk item transfer between gear containers with filtering, animation hooks, and event signaling.
    /// </summary>
    public class GearTransferManager : MonoBehaviour
    {
        [Header("Transfer Events")]
        public UnityEvent<InventoryItem, InventoryItem> onItemTransferred;
        public UnityEvent<InventoryItem, InventoryItem> onTransferStarted;
        public UnityEvent<InventoryItem, InventoryItem> onTransferCompleted;

        [Header("Transfer Settings")]
        [Tooltip("Optional delay (in seconds) between each item transfer for smooth UX.")]
        [SerializeField] private float transferDelay = 0f;

        [Tooltip("Optional category filter: leave empty to transfer all.")]
        [SerializeField] private string requiredCategory;

        private const string logTag = "[GearTransferManager]";

        /// <summary>
        /// Initiates an async transfer of all eligible items.
        /// </summary>
        public void TransferAllAsync(InventoryItem sourceGear, InventoryItem targetGear)
        {
            if (!ValidateGear(sourceGear, targetGear)) return;
            StartCoroutine(TransferCoroutine(sourceGear, targetGear));
        }

        /// <summary>
        /// Coroutine that performs delayed item-by-item transfer with filtering and event hooks.
        /// </summary>
        private IEnumerator TransferCoroutine(InventoryItem sourceGear, InventoryItem targetGear)
        {
            var sourceItems = new List<InventoryItem>(sourceGear.internalStorage);
            int targetCapacity = targetGear.storageCapacity;
            int transferred = 0;

            onTransferStarted?.Invoke(sourceGear, targetGear);

            foreach (var item in sourceItems)
            {
                if (targetGear.internalStorage.Count >= targetCapacity)
                {
                    Debug.LogWarning($"{logTag} {targetGear.ItemName} is full. Stopping transfer.");
                    break;
                }

                if (!IsItemTransferable(item))
                    continue;

                if (!string.IsNullOrEmpty(requiredCategory) &&
                    !string.Equals(item.category, requiredCategory, System.StringComparison.OrdinalIgnoreCase))
                {
                    Debug.Log($"{logTag} Skipped {item.ItemName} — category mismatch.");
                    continue;
                }

                targetGear.internalStorage.Add(item);
                sourceGear.internalStorage.Remove(item);
                transferred++;

                onItemTransferred?.Invoke(item, targetGear);

                if (transferDelay > 0f)
                    yield return new WaitForSeconds(transferDelay);
            }

            onTransferCompleted?.Invoke(sourceGear, targetGear);
            Debug.Log($"{logTag} Transferred {transferred} items from {sourceGear.ItemName} to {targetGear.ItemName}.");
        }

        /// <summary>
        /// Validates that both gear containers are ready.
        /// </summary>
        private bool ValidateGear(InventoryItem source, InventoryItem target)
        {
            if (source == null || target == null)
            {
                Debug.LogWarning($"{logTag} Source or target gear is null.");
                return false;
            }

            if (source.internalStorage == null || target.internalStorage == null)
            {
                Debug.LogWarning($"{logTag} One of the gear containers lacks internalStorage.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if an item is valid and eligible for transfer.
        /// </summary>
        private bool IsItemTransferable(InventoryItem item)
        {
            if (item == null)
            {
                Debug.LogWarning($"{logTag} Skipped null item.");
                return false;
            }

            if (!item.CanBeStored())
            {
                Debug.LogWarning($"{logTag} {item.ItemName} is not transferable (contains nested items).");
                return false;
            }

            return true;
        }
    }
}
