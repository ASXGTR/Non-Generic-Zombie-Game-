using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;

public class GearSlotInitiation : MonoBehaviour
{
    [Header("UI Slot Settings")]
    public Transform slotParent;
    private List<GearSlotUI> gearSlotUIs = new();

    void Start()
    {
        StartCoroutine(StartupSequence());
    }

    private IEnumerator StartupSequence()
    {
        int timeoutFrames = 600;
        int waited = 0;

        while (!PlayerStarterSetup.setupComplete && waited < timeoutFrames)
        {
            waited++;
            yield return null;
        }

        Inventory inventory = Object.FindFirstObjectByType<Inventory>();

        while ((inventory == null || !inventory.hasRegisteredGearContainers) && waited < timeoutFrames)
        {
            waited++;
            yield return null;
            inventory = Object.FindFirstObjectByType<Inventory>();
        }

        if (inventory == null || !inventory.hasRegisteredGearContainers)
        {
            Debug.LogError("🚫 Slot generation aborted — still no valid gear containers after max wait.");
            yield break;
        }

        Debug.Log($"⏳ GearSlotInitiation waited {waited} frames for starter setup.");
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
                Debug.Log($"📦 GearSlotUI found: {child.name}");
            }
        }

        Debug.Log($"🔍 Total gear slot UIs cached: {gearSlotUIs.Count}");
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
                slotUI.Initialize(matchingGear.data);
                Debug.Log($"✅ UI initialized for slot: {slotUI.name} → {matchingGear.itemName}");
            }
            else
            {
                slotUI.ClearSlot();
                Debug.Log($"❌ No gear found for slot: {slotUI.name}");
            }
        }
    }
}
