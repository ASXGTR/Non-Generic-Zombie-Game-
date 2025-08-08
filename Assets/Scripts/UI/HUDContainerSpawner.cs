using Core.Shared.Models;
using UnityEngine;
using Game.Inventory;

public class HUDContainerSpawner : MonoBehaviour
{
    private EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = Object.FindFirstObjectByType<EquipmentManager>();
        if (equipmentManager == null)
        {
            Debug.LogError("[HUDContainerSpawner] EquipmentManager not found.");
            return;
        }

        InitializeStoragePanels();
    }

    void InitializeStoragePanels()
    {
        foreach (var kvp in equipmentManager.GetAllEquippedItems())
        {
            var gear = kvp.Value;
            if (gear == null || !gear.IsContainer || gear.StorageCapacity <= 0) continue;

            Debug.Log($"[HUDContainerSpawner] Initializing storage for '{gear.ItemName}' with {gear.StorageCapacity} slots.");
            // TODO: Instantiate HUD container prefab via manager or panel spawner
        }
    }

    public int GetTotalStorageCapacity()
    {
        int total = 0;
        foreach (var kvp in equipmentManager.GetAllEquippedItems())
        {
            var gear = kvp.Value;
            if (gear != null)
                total += gear.StorageCapacity;
        }
        return total;
    }
}
