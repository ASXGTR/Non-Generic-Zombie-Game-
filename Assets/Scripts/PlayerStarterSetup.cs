using UnityEngine;
using Game.Inventory;
using Game.UI;
using System.Collections.Generic;

public class PlayerStarterSetup : MonoBehaviour
{
    [Header("Starter Clothing")]
    public ItemData starterJeans;
    public ItemData starterTShirt;

    [Header("Starter Items")]
    public ItemData glowstickItem;

    [Header("UI")]
    public GameObject itemSlotPrefab;
    public Transform slotGridParent;

    private Inventory playerInventory;

    public static bool setupComplete = false;

    void Start()
    {
        playerInventory = Object.FindFirstObjectByType<Game.Inventory.Inventory>();

        if (playerInventory == null)
        {
            Debug.LogError("❌ No Inventory found for PlayerStarterSetup.");
            setupComplete = true;
            return;
        }

        EquipStarterClothing();
        AddInitialItemToStorage();
        GenerateInitialStorageSlots();

        Debug.Log("🎮 Player starter setup complete.");
        setupComplete = true;
    }

    void EquipStarterClothing()
    {
        EquipSingleClothing(starterJeans, "👖");
        EquipSingleClothing(starterTShirt, "👕");

        foreach (var gear in playerInventory.GetEquippedGear())
        {
            Debug.Log($"[StartupCheck] Gear: {gear.itemName} | Equipped: {gear.isEquipped} | Capacity: {gear.storageCapacity} | HasStorage: {(gear.internalStorage != null ? "✅" : "❌")}");
        }
    }

    void EquipSingleClothing(ItemData clothingData, string emoji)
    {
        if (clothingData == null) return;

        Item clothingItem = new Item(clothingData);

        if (!playerInventory.ownedItems.Contains(clothingItem))
            playerInventory.ownedItems.Add(clothingItem);

        playerInventory.EquipItem(clothingItem);

        if (clothingItem.storageCapacity > 0 && clothingItem.internalStorage == null)
        {
            clothingItem.internalStorage = new List<Item>();
            Debug.Log($"📦 Initialized storage for {clothingItem.itemName} | Capacity: {clothingItem.storageCapacity}");
        }

        Debug.Log($"{emoji} Equipped: {clothingData.itemName}");
    }

    void AddInitialItemToStorage()
    {
        if (glowstickItem == null) return;

        Item starterItem = new Item(glowstickItem);
        playerInventory.AddItem(starterItem);
        Debug.Log($"📦 Added starter item: {glowstickItem.itemName}");
    }

    void GenerateInitialStorageSlots()
    {
        if (slotGridParent == null)
        {
            Debug.LogError("🚫 SlotGridParent not assigned.");
            return;
        }

        int totalSlots = 0;

        List<Item> allEquipped = playerInventory.GetEquippedGear();
        foreach (Item gear in allEquipped)
        {
            if (gear?.internalStorage != null)
                totalSlots += gear.internalStorage.Count;
        }

        if (totalSlots < 16) totalSlots = 16;

        for (int i = 0; i < totalSlots; i++)
        {
            GameObject slotGO = Instantiate(itemSlotPrefab, slotGridParent);
            ItemSlotUI slotUI = slotGO.GetComponent<ItemSlotUI>();

            if (slotUI != null)
                slotUI.Initialize(null);
            else
                Debug.LogWarning("⚠️ ItemSlotUI missing on prefab.");
        }

        Debug.Log($"🔧 Generated {totalSlots} starter inventory slots.");
    }
}
