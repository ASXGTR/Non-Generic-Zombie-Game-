using UnityEngine;
using Game.Inventory;

public class HUDContainerSpawner : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = Object.FindFirstObjectByType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("[HUDContainerSpawner] Inventory not found.");
            return;
        }

        InitializeStoragePanels();
    }

    void InitializeStoragePanels()
    {
        foreach (var gear in inventory.GetEquippedGear())
        {
            if (gear == null || !gear.isContainer || gear.storageCapacity <= 0) continue;

            Debug.Log($"[HUDContainerSpawner] Initializing storage for '{gear.itemName}' with {gear.storageCapacity} slots.");
            // TODO: Instantiate HUD container prefab via manager or panel spawner
        }
    }

    public int GetTotalStorageCapacity()
    {
        int total = 0;
        foreach (var gear in inventory.GetEquippedGear())
        {
            if (gear != null)
                total += gear.storageCapacity;
        }
        return total;
    }
}
