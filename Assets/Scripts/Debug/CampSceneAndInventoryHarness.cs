// Filename: CampSceneAndInventoryHarness.cs
// Location: Assets/Scripts/Harness
// Namespace: Harness

using Data;        // For RecipeData
using Environment; // For CampData and CampObjectSpawner
using Inventory;   // For StoragePanelBinder and InventoryUIUpdater
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Harness
{
    public class CampSceneAndInventoryHarness : MonoBehaviour
    {
        [Header("Camp Setup")]
        public CampData campData;
        public CampObjectSpawner objectSpawner;

        [Header("Inventory")]
        public StoragePanelBinder storagePanelBinder;
        public InventoryUIUpdater uiUpdater;

        [Header("Recipes")]
        public RecipeData[] recipeBook;

        private void Awake()
        {
            InitializeCamp();
            BindInventory();
        }

        private void InitializeCamp()
        {
            if (campData != null && objectSpawner != null)
            {
                objectSpawner.SpawnCampObjects(campData);
            }
            else
            {
                Debug.LogWarning("CampData or ObjectSpawner not assigned.");
            }
        }

        private void BindInventory()
        {
            if (storagePanelBinder != null && uiUpdater != null)
            {
                storagePanelBinder.BindPanels();
                uiUpdater.RefreshInventoryDisplay();
            }
            else
            {
                Debug.LogWarning("Inventory binder or UI updater not assigned.");
            }
        }
    }
}
