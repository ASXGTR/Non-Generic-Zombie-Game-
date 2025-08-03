using UnityEngine;
using Game.Inventory;
using Game.UI; // ✅ Added for InventoryUIManager

public class StorageTestLite : MonoBehaviour
{
    [Header("Assign the Green Apple ItemData")]
    public ItemData greenAppleData;

    [Header("Assign your Inventory UI Manager")]
    public InventoryUIManager uiManager;

    void Start()
    {
        if (greenAppleData == null || uiManager == null)
        {
            Debug.LogWarning("[StorageTestLite] Assign Green Apple and UI Manager in Inspector.");
            return;
        }

        Item greenAppleItem = new Item(greenAppleData);

        if (greenAppleItem.internalStorage != null)
        {
            greenAppleItem.internalStorage.Add(new Item(greenAppleData));
            uiManager.ShowStorage(greenAppleItem);
            Debug.Log($"[StorageTestLite] Showing 1 Green Apple in panel.");
        }
        else
        {
            Debug.LogWarning("[StorageTestLite] Item has no internal storage. Not a container.");
        }
    }
}
