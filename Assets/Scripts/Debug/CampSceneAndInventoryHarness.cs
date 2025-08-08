using UnityEngine;
using Systems;
using Core.Shared.Models;

public class CampSceneAndInventoryHarness : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject[] storagePanels;

    [Header("Storage Containers")]
    public GameObject[] storageContainers;

    [Header("Inventory")]
    public GameObject inventoryRoot;
    public GameObject inventoryPanel;

    [Header("Crafting")]
    public GameObject recipePanel;
    public CraftingRecipe[] availableRecipes;

    private void Start()
    {
        // Bind storage panels to containers
        StoragePanelBinder.BindPanelsToContainers(storagePanels, storageContainers);

        // Refresh inventory UI
        InventoryUIUpdater.RefreshAll();

        // TODO: Use availableRecipes to populate crafting panel
        Debug.Log("CampSceneAndInventoryHarness initialized.");
    }
}
