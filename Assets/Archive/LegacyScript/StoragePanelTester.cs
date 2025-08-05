// 🔒 Deprecated StoragePanelTester.cs
// Logic modularized into StoragePanelSpawner.cs, InventoryDebugTools.cs, SlotAuditTool.cs, and UIPanelManager.cs on 2025-08-05
// Retained for reference only. Do not use in runtime.
using Game.Inventory;
using Game.UI; // ✅ Required for InventoryUIManager reference
using UnityEngine;

/// <summary>
/// Lite test for showing an item's internal storage in the inventory UI panel.
/// </summary>
public class StoragePanelTester : MonoBehaviour
{
    [Header("Assign the Green Apple ItemData")]
    [SerializeField] private ItemData greenAppleData;

    [Header("Assign your Inventory UI Manager")]
    [SerializeField] private InventoryUIManager uiManager;

    private const string logTag = "[StoragePanelTester]";

    private void Start()
    {
        if (greenAppleData == null || uiManager == null)
        {
            Debug.LogWarning($"{logTag} ⚠️ Missing references! Assign Green Apple and UI Manager in inspector.");
            return;
        }

        // Create a Green Apple item and add one copy to its internal storage (simulating container test)
        Item containerItem = new Item(greenAppleData);

        if (containerItem.internalStorage != null)
        {
            containerItem.internalStorage.Add(new Item(greenAppleData));
            uiManager.ShowStorage(containerItem);
            Debug.Log($"{logTag} 📦 Showing 1 Green Apple inside container UI.");
        }
        else
        {
            Debug.LogWarning($"{logTag} ❌ Item has no internal storage. Not a container.");
        }
    }
}
