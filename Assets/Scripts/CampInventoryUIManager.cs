// Game.UI.CampInventoryUIManager.cs
using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;
using Game.UI;

namespace Game.UI
{
    public class CampInventoryUIManager : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject inventoryUI;
        public GameObject blackoutOverlay;
        public GameObject itemSlotPrefab;
        public Transform itemGridParent;
        public Game.Inventory.Inventory playerInventory; // 🔧 Fully qualified type to disambiguate
        public GameObject toggleInventoryButton;
        public GameObject playerHUD;
        public GameObject HUDCanvas;
        public GameObject campCanvas;

        [Header("Settings")]
        public KeyCode toggleKey = KeyCode.I;
        public bool allowInventory = true;

        private bool isOpen = false;
        private readonly List<GameObject> spawnedSlots = new();

        void Start()
        {
            if (inventoryUI == null)
                Debug.LogError("❌ InventoryUI is not assigned.");
            if (blackoutOverlay == null)
                Debug.LogError("❌ BlackoutOverlay is not assigned.");
            if (playerInventory == null)
                Debug.LogError("❌ PlayerInventory reference is missing!");
            if (campCanvas == null)
                Debug.LogWarning("⚠️ Camp canvas reference is missing.");
            if (HUDCanvas == null)
                Debug.LogWarning("⚠️ HUDCanvas reference is missing.");

            if (inventoryUI != null)
                inventoryUI.SetActive(false);
            if (blackoutOverlay != null)
                blackoutOverlay.SetActive(false);
            if (toggleInventoryButton != null)
                toggleInventoryButton.SetActive(true);
        }

        void Update()
        {
            if (!allowInventory || inventoryUI == null)
                return;

            if (Input.GetKeyDown(toggleKey))
            {
                if (!isOpen)
                    OpenInventory();
                else
                    CloseInventory();
            }
        }

        public void OpenInventory()
        {
            if (inventoryUI == null)
                return;

            isOpen = true;
            inventoryUI.SetActive(true);

            if (blackoutOverlay != null)
                blackoutOverlay.SetActive(true);
            if (toggleInventoryButton != null)
                toggleInventoryButton.SetActive(false);
            if (campCanvas != null)
                campCanvas.SetActive(false);
            if (HUDCanvas != null)
            {
                HUDCanvas.SetActive(true);
                Debug.Log("📺 HUDCanvas enabled during inventory open.");
            }
            if (playerHUD != null)
            {
                playerHUD.SetActive(true);
                Debug.Log("📺 playerHUD enabled during inventory open.");
            }

            RefreshInventoryUI();
            Debug.Log("📦 Camp inventory opened.");
        }

        public void CloseInventory()
        {
            if (inventoryUI == null)
            {
                Debug.LogWarning("⚠️ inventoryUI is null!");
                return;
            }

            isOpen = false;
            inventoryUI.SetActive(false);

            if (blackoutOverlay != null)
                blackoutOverlay.SetActive(false);
            if (toggleInventoryButton != null)
                toggleInventoryButton.SetActive(true);
            if (campCanvas != null)
                campCanvas.SetActive(true);

            Debug.Log("📦 Camp inventory closed.");
            ClearInventoryUI();
        }

        void RefreshInventoryUI()
        {
            ClearInventoryUI();

            if (playerInventory == null)
            {
                Debug.LogWarning("⚠️ No playerInventory assigned.");
                return;
            }

            List<InventoryItem> displayItems = playerInventory.GetAllItems();

            if (displayItems == null || displayItems.Count == 0)
            {
                Debug.Log("👜 No items found to display.");
                return;
            }

            foreach (InventoryItem item in displayItems)
            {
                if (item == null) continue;

                GameObject slotGO = Instantiate(itemSlotPrefab, itemGridParent);
                if (slotGO == null)
                {
                    Debug.LogWarning("❌ Failed to instantiate item slot prefab.");
                    continue;
                }

                ItemSlotUI slotUI = slotGO.GetComponent<ItemSlotUI>();
                if (slotUI != null)
                    slotUI.SetSlot(item);
                else
                    Debug.LogWarning("❌ Missing ItemSlotUI component on instantiated slot prefab.");

                spawnedSlots.Add(slotGO);
            }
        }

        void ClearInventoryUI()
        {
            foreach (var go in spawnedSlots)
            {
                if (go != null)
                    Destroy(go);
            }

            spawnedSlots.Clear();
        }
    }
}
