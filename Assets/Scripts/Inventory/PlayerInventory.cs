using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    /// <summary>
    /// Local inventory component for equipped gear, container logic, and storage delegation.
    /// </summary>
    [AddComponentMenu("Inventory/Player Inventory")]
    public class PlayerInventory : MonoBehaviour
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
                if (item.itemType == ItemTypeEnum.Clothing && !item.isEquipped)
                {
                    EquipItem(item);
                    Debug.Log($"[PlayerInventory] Auto-equipped: {item.ItemName}");
                }
            }

            Debug.Log("[PlayerInventory] Starter gear auto-equipped.");
        }

        public void AuditGearContainers()
        {
            registeredContainers.Clear();

            foreach (var item in ownedItems)
            {
                if (item == null)
                {
                    Debug.LogWarning("[PlayerInventory] 🛑 Audit skipped null gear entry.");
                    continue;
                }

                Debug.Log($"[PlayerInventory] 🔍 Audit: {item.ItemName} | isContainer: {item.isContainer} | Cap: {item.storageCapacity} | Equipped: {item.isEquipped}");

                if (item.isContainer && item.storageCapacity > 0)
                {
                    item.internalStorage ??= new List<InventoryItem>();
                    RegisterContainer(item);
                }
            }

            auditComplete = true;
            Debug.Log($"[PlayerInventory] ✅ Audit complete. Registered: {registeredContainers.Count}");
        }

        private void RegisterContainer(InventoryItem containerGear)
        {
            if (containerGear == null || containerGear.storageCapacity <= 0)
            {
                Debug.LogWarning($"[PlayerInventory] ⚠️ Invalid container: {containerGear?.ItemName}");
                return;
            }

            if (!registeredContainers.Contains(containerGear))
            {
                registeredContainers.Add(containerGear);
                Debug.Log($"[PlayerInventory] 📦 Registered: {containerGear.ItemName}");
            }
        }

        public void EquipItem(InventoryItem item)
        {
            if (item == null || item.itemType != ItemTypeEnum.Clothing)
            {
                Debug.LogWarning($"[PlayerInventory] ❌ Cannot equip: {item?.ItemName}");
                return;
            }

            item.isEquipped = true;

            if (!ownedItems.Contains(item))
                ownedItems.Add(item);

            item.internalStorage ??= new List<InventoryItem>();
            RegisterContainer(item);

            Debug.Log($"[PlayerInventory] 🧥 Equipped: {item.ItemName}");
        }

        public void AddItem(InventoryItem item)
        {
            if (item == null)
            {
                Debug.LogWarning("[PlayerInventory] ⚠️ Null item ignored.");
                return;
            }

            ownedItems.Add(item);

            foreach (var gear in registeredContainers)
            {
                gear.internalStorage ??= new List<InventoryItem>();

                if (gear.internalStorage.Count < gear.storageCapacity)
                {
                    gear.internalStorage.Add(item);
                    Debug.Log($"[PlayerInventory] ✅ Stored '{item.ItemName}' in '{gear.ItemName}'");
                    return;
                }
            }

            Debug.LogWarning($"[PlayerInventory] ❌ No available storage for '{item.ItemName}'");
        }

        public List<InventoryItem> GetEquippedGear() =>
            ownedItems.Where(i => i != null && i.isEquipped).ToList();

        public ItemData GetEquippedItemBySlotType(GearSlotType slotType) =>
            ownedItems.FirstOrDefault(i => i != null && i.isEquipped && i.gearSlotType == slotType)?.data;

        public List<InventoryItem> GetActiveStorageGear() => registeredContainers;

        public void PrintStorageDebug()
        {
            foreach (var gear in registeredContainers)
            {
                int used = gear.internalStorage?.Count ?? 0;
                Debug.Log($"[PlayerInventory] {gear.ItemName}: {used}/{gear.storageCapacity}");
            }
        }

        public List<InventoryItem> GetAllItems()
        {
            List<InventoryItem> all = new(ownedItems);

            foreach (var gear in registeredContainers)
            {
                if (gear?.internalStorage != null)
                    all.AddRange(gear.internalStorage);
            }

            return all;
        }
    }
}
