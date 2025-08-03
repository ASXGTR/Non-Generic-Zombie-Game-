using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;

namespace Game.UI
{
    public class InventoryUIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject storagePanel;
        [SerializeField] private GameObject mainScreenCanvas;

        [Header("Slot Prefab & Grid Parent")]
        [SerializeField] private GameObject itemSlotPrefab;
        [SerializeField] private Transform storageGridParent;

        private List<GameObject> spawnedStorageSlots = new List<GameObject>();

        /// <summary>
        /// Opens inventory and storage panels, hides main UI.
        /// </summary>
        public void OpenInventory()
        {
            if (inventoryPanel == null)
            {
                Debug.LogWarning("[InventoryUIManager] inventoryPanel not assigned.");
                return;
            }

            mainScreenCanvas?.SetActive(false);
            inventoryPanel.SetActive(true);
            storagePanel?.SetActive(true);

            Debug.Log("[InventoryUIManager] Inventory and storage panels activated.");
        }

        /// <summary>
        /// Closes all panels and restores main UI.
        /// </summary>
        public void CloseInventory()
        {
            inventoryPanel?.SetActive(false);
            storagePanel?.SetActive(false);
            mainScreenCanvas?.SetActive(true);

            ClearStorageSlots();

            Debug.Log("[InventoryUIManager] Inventory UI closed and cleaned.");
        }

        /// <summary>
        /// Refresh entire UI – placeholder hook.
        /// </summary>
        public void RefreshUI()
        {
            Debug.Log("[InventoryUIManager] Inventory UI refreshed.");
            // Extend with UI rebuild logic if needed
        }

        /// <summary>
        /// Display a transfer warning message.
        /// </summary>
        public void ShowTransferError(string message)
        {
            Debug.LogWarning("[InventoryUIManager] Transfer Error: " + message);
            // You could plug in a UI popup later
        }

        /// <summary>
        /// Show internal storage contents from a gear item.
        /// </summary>
        public void ShowStorage(Item containerItem)
        {
            if (containerItem == null)
            {
                Debug.LogWarning("[InventoryUIManager] containerItem is null.");
                return;
            }

            if (containerItem.internalStorage == null || containerItem.internalStorage.Count == 0)
            {
                Debug.Log($"[InventoryUIManager] '{containerItem.itemName}' has no stored items.");
                ClearStorageSlots();
                return;
            }

            if (storagePanel == null || itemSlotPrefab == null || storageGridParent == null)
            {
                Debug.LogWarning("[InventoryUIManager] Missing UI references for storage rendering.");
                return;
            }

            storagePanel.SetActive(true);
            ClearStorageSlots();

            foreach (Item item in containerItem.internalStorage)
            {
                GameObject slotGO = Instantiate(itemSlotPrefab, storageGridParent);
                ItemSlotUI slotUI = slotGO.GetComponent<ItemSlotUI>();
                if (slotUI != null)
                    slotUI.Initialize(item);
                else
                    Debug.LogWarning("❌ Missing ItemSlotUI component in prefab.");

                spawnedStorageSlots.Add(slotGO);
            }

            Debug.Log($"📦 Showing storage slots for '{containerItem.itemName}' | Items: {containerItem.internalStorage.Count}");
        }

        /// <summary>
        /// Clear previously spawned UI slots from storage grid.
        /// </summary>
        private void ClearStorageSlots()
        {
            foreach (GameObject slot in spawnedStorageSlots)
            {
                if (slot != null)
                    Destroy(slot);
            }
            spawnedStorageSlots.Clear();
        }
    }
}
