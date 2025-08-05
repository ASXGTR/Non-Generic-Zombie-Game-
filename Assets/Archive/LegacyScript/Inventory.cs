// Game.Inventory.Inventory.cs

// 🔒 Deprecated legacy script.
// Logic merged into InventoryManager.cs on 2025-08-05.
// Retained for reference only. Do not use in runtime.

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public List<InventoryItem> ownedItems = new();
        public InventoryItem rightHand;
        public InventoryItem leftHand;
        public bool auditComplete = false;

        private List<InventoryItem> registeredContainers = new();

        public bool hasRegisteredGearContainers => registeredContainers.Any(item =>
            item != null &&
            item.isContainer &&
            item.isEquipped &&
            item.storageCapacity > 0 &&
            item.internalStorage != null);

        public void AutoEquipStarterGear()
        {
            foreach (var item in ownedItems)
            {
                if (item.itemType == ItemType.Clothing && !item.isEquipped)
                {
                    EquipItem(item);
                    Debug.Log($"[Inventory] Auto-equipped: {item.ItemName}");
                }
            }

            Debug.Log("[Inventory] Starter gear auto-equipped.");
        }

        public void AuditOwnedGearContainers()
        {
            registeredContainers.Clear();

            foreach (var item in ownedItems)
            {
                if (item == null)
                {
                    Debug.LogWarning("🛑 Audit skipped null gear entry.");
                    continue;
                }

                Debug.Log($"🔍 Audit Gear: {item.ItemName} | isContainer: {item.isContainer} | Cap: {item.storageCapacity} | Equipped: {item.isEquipped}");

                if (item.isContainer && item.storageCapacity > 0)
                {
                    if (item.internalStorage == null)
                    {
                        item.internalStorage = new List<InventoryItem>();
                        Debug.Log($"🛠️ Container force-initialized: {item.ItemName}");
                    }

                    RegisterContainer(item);
                }
            }

            Debug.Log($"✅ Audit complete — registered containers: {registeredContainers.Count}");
            auditComplete = true;
        }

        public void RegisterContainer(InventoryItem containerGear)
        {
            if (containerGear == null || containerGear.storageCapacity <= 0)
            {
                Debug.LogWarning($"⚠️ Invalid container: {containerGear?.ItemName}");
                return;
            }

            if (!registeredContainers.Contains(containerGear))
            {
                registeredContainers.Add(containerGear);
                Debug.Log($"📦 Registered container: {containerGear.ItemName}");
            }
        }

        public List<InventoryItem> GetEquippedGear() =>
            ownedItems.Where(i => i != null && i.isEquipped).ToList();

        public ItemData GetEquippedItemBySlotType(GearSlotType slotType) =>
            ownedItems.FirstOrDefault(i => i != null && i.isEquipped && i.gearSlotType == slotType)?.data;

        public List<InventoryItem> GetActiveStorageGear() => registeredContainers;

        public void AddItemToFirstAvailableSlot(InventoryItem item)
        {
            if (item == null)
            {
                Debug.LogWarning("⚠️ Tried to add a null item.");
                return;
            }

            foreach (var gear in GetActiveStorageGear())
            {
                if (gear == null) continue;

                gear.internalStorage ??= new List<InventoryItem>();

                if (gear.internalStorage.Count < gear.storageCapacity)
                {
                    gear.internalStorage.Add(item);
                    Debug.Log($"✅ Placed '{item.ItemName}' into '{gear.ItemName}'");
                    return;
                }
            }

            Debug.LogWarning($"❌ No available storage found for '{item.ItemName}'");
        }

        public void AddItem(InventoryItem item)
        {
            if (item == null)
            {
                Debug.LogWarning("⚠️ Null item assignment ignored.");
                return;
            }

            ownedItems.Add(item);

            if (registeredContainers.Count == 0)
            {
                Debug.Log($"📥 No active storage gear. '{item.ItemName}' remains loose.");
                return;
            }

            AddItemToFirstAvailableSlot(item);
        }

        public void EquipItem(InventoryItem item)
        {
            if (item == null || item.itemType != ItemType.Clothing)
            {
                Debug.LogWarning($"❌ Cannot equip: {item?.ItemName}");
                return;
            }

            item.isEquipped = true;

            if (!ownedItems.Contains(item))
                ownedItems.Add(item);

            Debug.Log($"🧥 Equipped: {item.ItemName}");

            if (item.storageCapacity > 0 && item.internalStorage == null)
            {
                item.internalStorage = new List<InventoryItem>();
                Debug.Log($"🧠 Initialized storage on: {item.ItemName}");
            }

            RegisterContainer(item);
        }

        public void PrintStorageDebug()
        {
            foreach (var gear in GetActiveStorageGear())
            {
                int used = gear.internalStorage?.Count ?? 0;
                Debug.Log($"[Inventory] {gear.ItemName}: {used}/{gear.storageCapacity}");
            }
        }

        public List<InventoryItem> GetAllItems()
        {
            List<InventoryItem> all = new(ownedItems);

            foreach (var gear in GetActiveStorageGear())
            {
                if (gear?.internalStorage != null)
                    all.AddRange(gear.internalStorage);
            }

            return all;
        }
    }
}
