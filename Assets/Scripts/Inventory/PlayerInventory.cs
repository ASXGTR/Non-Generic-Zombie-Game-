// File: Assets/Scripts/Inventory/PlayerInventory.cs

using Core.Shared;
using Core.Shared.Enums;
using Core.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    [AddComponentMenu("Inventory/Player Inventory")]
    public class PlayerInventory : MonoBehaviour
    {
        public List<ItemInstance> ownedItems = new();
        public ItemInstance rightHand;
        public ItemInstance leftHand;
        public bool auditComplete = false;

        private List<ItemInstance> registeredContainers = new();

        public bool hasRegisteredGearContainers => registeredContainers.Any(item =>
            item != null &&
            item.IsContainer &&
            item.IsEquipped &&
            item.StorageCapacity > 0 &&
            item.InternalStorage != null);

        public void AutoEquipStarterGear()
        {
            foreach (var item in ownedItems)
            {
                if (item.Type == ItemTypeEnum.Armor && !item.IsEquipped)
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

                Debug.Log($"[PlayerInventory] 🔍 Audit: {item.ItemName} | IsContainer: {item.IsContainer} | Cap: {item.StorageCapacity} | Equipped: {item.IsEquipped}");

                if (item.IsContainer && item.StorageCapacity > 0)
                {
                    item.EnsureStorageInitialized(); // semantic noop
                    RegisterContainer(item);
                }
            }

            auditComplete = true;
            Debug.Log($"[PlayerInventory] ✅ Audit complete. Registered: {registeredContainers.Count}");
        }

        private void RegisterContainer(ItemInstance containerGear)
        {
            if (containerGear == null || containerGear.StorageCapacity <= 0)
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

        public void EquipItem(ItemInstance item)
        {
            if (item == null || item.Type != ItemTypeEnum.Armor)
            {
                Debug.LogWarning($"[PlayerInventory] ❌ Cannot equip: {item?.ItemName}");
                return;
            }

            item.IsEquipped = true;

            if (!ownedItems.Contains(item))
                ownedItems.Add(item);

            item.EnsureStorageInitialized(); // semantic noop
            RegisterContainer(item);

            Debug.Log($"[PlayerInventory] 🧥 Equipped: {item.ItemName}");
        }

        public void AddItem(ItemInstance item)
        {
            if (item == null)
            {
                Debug.LogWarning("[PlayerInventory] ⚠️ Null item ignored.");
                return;
            }

            ownedItems.Add(item);

            foreach (var gear in registeredContainers)
            {
                if (gear.TryAddToStorage(item))
                {
                    Debug.Log($"[PlayerInventory] ✅ Stored '{item.ItemName}' in '{gear.ItemName}'");
                    return;
                }
            }

            Debug.LogWarning($"[PlayerInventory] ❌ No available storage for '{item.ItemName}'");
        }

        public List<ItemInstance> GetEquippedGear() =>
            ownedItems.Where(i => i != null && i.IsEquipped).ToList();

        public List<ItemInstance> GetActiveStorageGear() => registeredContainers;

        public void PrintStorageDebug()
        {
            foreach (var gear in registeredContainers)
            {
                int used = gear.InternalStorage?.Count ?? 0;
                Debug.Log($"[PlayerInventory] {gear.ItemName}: {used}/{gear.StorageCapacity}");
            }
        }

        public List<ItemInstance> GetAllItems()
        {
            List<ItemInstance> all = new(ownedItems);

            foreach (var gear in registeredContainers)
            {
                if (gear?.InternalStorage != null)
                    all.AddRange(gear.InternalStorage);
            }

            return all;
        }
    }
}
