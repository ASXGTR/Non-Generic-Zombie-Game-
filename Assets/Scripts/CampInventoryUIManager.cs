using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Inventory;
using Game.UI;

public class CampInventoryUIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryUI;
    public GameObject blackoutOverlay;
    public GameObject itemSlotPrefab;
    public Transform itemGridParent;
    public Inventory playerInventory;
    public GameObject toggleInventoryButton;
    public GameObject playerHUD;
    public GameObject HUDCanvas;
    public GameObject campCanvas;

    [Header("Settings")]
    public KeyCode toggleKey = KeyCode.I;
    public bool allowInventory = true;

    private bool isOpen = false;
    private List<GameObject> spawnedSlots = new List<GameObject>();

    void Start()
    {
        if (inventoryUI == null)
            Debug.LogError("❌ InventoryUI is not assigned.");
        if (blackoutOverlay == null)
            Debug.LogError("❌ BlackoutOverlay is not assigned.");
        if (playerInventory == null)
            Debug.LogError("❌ PlayerInventory reference is missing!");
        if (campCanvas == null)
            Debug.LogWarning("⚠️ Camp canvas reference is missing from CampInventoryUIManager.");
        if (HUDCanvas == null)
            Debug.LogWarning("⚠️ HUDCanvas reference is missing from CampInventoryUIManager.");

        inventoryUI?.SetActive(false);
        blackoutOverlay?.SetActive(false);
        toggleInventoryButton?.SetActive(true);
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
        if (!inventoryUI)
            return;

        isOpen = true;
        inventoryUI.SetActive(true);
        blackoutOverlay?.SetActive(true);
        toggleInventoryButton?.SetActive(false);

        campCanvas?.SetActive(false);

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
        if (!inventoryUI)
        {
            Debug.LogWarning("⚠️ inventoryUI is null!");
            return;
        }

        isOpen = false;
        inventoryUI.SetActive(false);
        blackoutOverlay?.SetActive(false);
        toggleInventoryButton?.SetActive(true);

        campCanvas?.SetActive(true);

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

        List<Item> displayItems = playerInventory.GetAllItems();

        if (displayItems.Count == 0)
        {
            Debug.Log("👜 No items found to display.");
            return;
        }

        foreach (Item item in displayItems)
        {
            GameObject slotGO = Instantiate(itemSlotPrefab, itemGridParent);
            ItemSlotUI slotUI = slotGO.GetComponent<ItemSlotUI>();
            if (slotUI != null)
                slotUI.Initialize(item);
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
