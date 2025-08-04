using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

        public void CloseInventory()
        {
            inventoryPanel?.SetActive(false);
            storagePanel?.SetActive(false);
            mainScreenCanvas?.SetActive(true);

            ClearStorageSlots();

            Debug.Log("[InventoryUIManager] Inventory UI closed and cleaned.");
        }

        public void RefreshUI()
        {
            Debug.Log("[InventoryUIManager] Inventory UI refreshed.");
        }

        public void ShowTransferError(string message)
        {
            Debug.LogWarning("[InventoryUIManager] Transfer Error: " + message);
        }

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
                RenderStorageSlot(item);
            }

            Debug.Log($"📦 Showing storage slots for '{containerItem.itemName}' | Items: {containerItem.internalStorage.Count}");
        }

        private void RenderStorageSlot(Item item)
        {
            if (item == null)
                return;

            GameObject slotGO = Instantiate(itemSlotPrefab, storageGridParent);
            if (slotGO == null)
            {
                Debug.LogWarning("[InventoryUIManager] Failed to instantiate item slot prefab.");
                return;
            }

            ItemSlotUI slotUI = slotGO.GetComponent<ItemSlotUI>();
            if (slotUI != null)
                slotUI.Initialize(item);
            else
                Debug.LogWarning("[InventoryUIManager] Missing ItemSlotUI component in prefab.");

            spawnedStorageSlots.Add(slotGO);
        }

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
