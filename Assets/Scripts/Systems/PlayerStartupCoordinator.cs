using UnityEngine;
using Survival.Inventory;
using Survival.Stats;
using Survival.UI;
using Survival.Persistence;

namespace Survival.Player
{
    public class PlayerStartupCoordinator : MonoBehaviour
    {
        private void Start()
        {
            PlayerSpawnManager.SpawnPlayerAtStart();
            InventoryInitializer.LoadDefaultInventory();
            StatInitializer.InitializeStats();
            UIBootstrap.BindPlayerUI();
            // Optional: SaveManager.LoadLastSave(); // only if tightly integrated
        }
    }
}
