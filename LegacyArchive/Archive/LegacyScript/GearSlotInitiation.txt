// 🔒 Deprecated GearSlotInitiation.cs
// Logic modularized into GearSlotRegistry.cs and supporting systems on 2025-08-05
// Retained for reference only. Do not use in runtime.
using Game.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// Waits for player/inventory setup, then caches gear slot UI and populates them.
/// </summary>
public class GearSlotInitiation : MonoBehaviour
{
    [Header("UI Slot Settings")]
    public Transform slotParent;

    private readonly List<GearSlotUI> gearSlotUIs = new();

    void Start()
    {
        StartCoroutine(StartupSequence());
    }

    private IEnumerator StartupSequence()
    {
        const int timeoutFrames = 600;
        int waited = 0;

        // Wait for PlayerStarterSetup to complete
        while (!PlayerStarterSetup.setupComplete && waited < timeoutFrames)
        {
            waited++;
            yield return null;
        }

        Inventory inventory = null;

        // Wait for Inventory to exist and gear containers to register
        while ((inventory == null || !inventory.hasRegisteredGearContainers) && waited < timeoutFrames)
        {
            inventory = Object.FindFirstObjectByType<Inventory>();
            waited++;
            yield return null;
        }

        if (inventory == null || !inventory.hasRegisteredGearContainers)
        {
            Debug.LogError("🚫 Slot generation aborted — no gear containers after max wait.");
            yield break;
        }

        Debug.Log($"⏳ GearSlotInitiation waited {waited} frames for setup.");
        CacheUISlots();
        PopulateUISlots(inventory);
    }

    private void CacheUISlots()
    {
        gearSlotUIs.Clear();

        foreach (Transform child in slotParent)
        {
            GearSlotUI slotUI = child.GetComponent<GearSlotUI>();
            if (slotUI != null)
            {
                gearSlotUIs.Add(slotUI);
                Debug.Log($"📦 Cached GearSlotUI: {child.name}");
            }
        }

        Debug.Log($"🔍 Total GearSlotUI slots cached: {gearSlotUIs.Count}");
    }

    private void PopulateUISlots(Inventory inventory)
    {
        List<Item> gearContainers = inventory.GetEquippedGear();

        if (gearContainers == null || gearContainers.Count == 0)
        {
            Debug.LogWarning("⚠️ No equipped gear found to display.");
            return;
        }

        foreach (GearSlotUI slotUI in gearSlotUIs)
        {
            Item matchingGear = gearContainers.Find(g => g.gearSlotType == slotUI.slotType);

            if (matchingGear != null && matchingGear.data != null)
            {
                slotUI.Initialize(matchingGear);
                Debug.Log($"✅ Initialized slot: {slotUI.slotType} → {matchingGear.itemName}");
            }
            else
            {
                slotUI.ClearSlot();
                Debug.Log($"❌ No gear found for slot: {slotUI.slotType}");
            }
        }
    }
}
