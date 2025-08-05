using UnityEngine;
using Environment.Data;
using Environment.Spawn;
using Systems.Storage;
using Inventory.UI;

public class CampSceneAndInventoryHarness : MonoBehaviour
{
    public CampData campData;
    public GameObject[] campPrefabs;
    public GameObject[] campContainers;
    public GameObject[] storagePanels;

    void Start()
    {
        // Camp scene setup
        CampDataLoader.Load(campData);
        CampObjectSpawner.Spawn(campPrefabs, campData);
        Debug.Log("Camp scene initialized for testing.");

        // Inventory UI binding
        StoragePanelBinder.BindPanelsToContainers(storagePanels, campContainers);
        InventoryUIUpdater.RefreshAll(storagePanels);
        Debug.Log("Camp inventory UI initialized and linked to containers.");
    }
}
