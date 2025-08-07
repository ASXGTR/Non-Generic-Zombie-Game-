using UnityEngine;

namespace Systems
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

    // 🧩 Stubbed Dependencies

    public static class PlayerSpawnManager
    {
        public static void SpawnPlayerAtStart()
        {
            Debug.Log("[PlayerSpawnManager] Player spawned at start position.");
        }
    }

    public static class InventoryInitializer
    {
        public static void LoadDefaultInventory()
        {
            Debug.Log("[InventoryInitializer] Default inventory loaded.");
        }
    }

    public static class StatInitializer
    {
        public static void InitializeStats()
        {
            Debug.Log("[StatInitializer] Player stats initialized.");
        }
    }

    public static class UIBootstrap
    {
        public static void BindPlayerUI()
        {
            Debug.Log("[UIBootstrap] Player UI bound.");
        }
    }

    // Optional stub if SaveManager is needed later
    public static class SaveManager
    {
        public static void LoadLastSave()
        {
            Debug.Log("[SaveManager] Last save loaded.");
        }
    }
}
