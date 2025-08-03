using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> ownedItems = new List<Item>();
        public Item rightHand;
        public Item leftHand;

        public bool auditComplete = false;

        private List<Item> registeredContainers = new List<Item>();

        /// <summary>
        /// Returns true if there are any valid registered gear containers.
        /// </summary>
        public bool hasRegisteredGearContainers
        {
            get
            {
                return registeredContainers.Any(item =>
                    item != null &&
                    item.isContainer &&
                    item.isEquipped &&
                    item.storageCapacity > 0 &&
                    item.internalStorage != null);
            }
        }

        /// <summary>
        /// Automatically equips all unequipped clothing on game start.
        /// </summary>
        public void AutoEquipStarterGear()
        {
            foreach (Item item in ownedItems)
            {
                if (item.itemType == ItemType.Clothing && !item.isEquipped)
                {
                    EquipItem(item);
                    Debug.Log($"[Inventory] Auto-equipped: {item.itemName}");
                }
            }

            Debug.Log("[Inventory] Starter gear auto-equipped.");
        }

        /// <summary>
        /// Finds and registers container gear pieces with storage capacity.
        /// </summary>
        public void AuditOwnedGearContainers()
        {
            registeredContainers.Clear();

            foreach (Item item in ownedItems)
            {
                if (item == null)
                {
                    Debug.LogWarning("🛑 Audit skipped null gear entry.");
                    continue;
                }

                Debug.Log($"🔍 Audit Gear: {item.itemName} | isContainer: {item.isContainer} | Cap: {item.storageCapacity} | Equipped: {item.isEquipped}");

                if (item.isContainer && item.storageCapacity > 0)
                {
                    if (item.internalStorage == null)
                    {
                        item.internalStorage = new List<Item>();
                        Debug.Log($"🛠️ Container force-initialized: {item.itemName}");
                    }

                    RegisterContainer(item);
                }
            }

            Debug.Log($"✅ Audit complete — registered containers: {registeredContainers.Count}");
            auditComplete = true;
        }

        /// <summary>
        /// Registers valid container gear into the active container list.
        /// </summary>
        public void RegisterContainer(Item containerGear)
        {
            if (containerGear == null)
            {
                Debug.LogWarning("⚠️ Tried to register null container gear.");
                return;
            }

            if (containerGear.storageCapacity <= 0)
            {
                Debug.LogWarning($"⚠️ Gear '{containerGear.itemName}' has 0 capacity. Not registered.");
                return;
            }

            if (!registeredContainers.Contains(containerGear))
            {
                registeredContainers.Add(containerGear);
                Debug.Log($"📦 Registered container: {containerGear.itemName}");
            }
        }

        /// <summary>
        /// Returns all equipped gear pieces.
        /// </summary>
        public List<Item> GetEquippedGear()
        {
            List<Item> equipped = new List<Item>();
            foreach (Item item in ownedItems)
            {
                if (item != null && item.isEquipped)
                    equipped.Add(item);
            }
            return equipped;
        }

        /// <summary>
        /// Returns the currently equipped item for a specific gear slot.
        /// Used by Gear UI to populate slot visuals.
        /// </summary>
        public ItemData GetEquippedItemBySlotType(GearSlotType slotType)
        {
            foreach (Item item in ownedItems)
            {
                if (item != null && item.isEquipped && item.gearSlotType == slotType)
                    return item.data;
            }

            return null;
        }

        public List<Item> GetActiveStorageGear()
        {
            return registeredContainers;
        }

        /// <summary>
        /// Tries to place the given item into the first available storage container.
        /// </summary>
        public void AddItemToFirstAvailableSlot(Item item)
        {
            if (item == null)
            {
                Debug.LogWarning("⚠️ Tried to add a null item.");
                return;
            }

            bool placed = false;
            foreach (Item gear in GetActiveStorageGear())
            {
                if (gear == null)
                    continue;

                if (gear.internalStorage == null)
                    gear.internalStorage = new List<Item>();

                if (gear.internalStorage.Count < gear.storageCapacity)
                {
                    gear.internalStorage.Add(item);
                    Debug.Log($"✅ Placed '{item.itemName}' into '{gear.itemName}'");
                    placed = true;
                    break;
                }
            }

            if (!placed)
                Debug.LogWarning($"❌ No available storage found for '{item.itemName}'");
        }

        public void AddItem(Item item)
        {
            if (item == null)
            {
                Debug.LogWarning("⚠️ Null item assignment ignored.");
                return;
            }

            ownedItems.Add(item);

            if (registeredContainers.Count == 0)
            {
                Debug.Log($"📥 No active storage gear. '{item.itemName}' remains loose.");
                return;
            }

            AddItemToFirstAvailableSlot(item);
        }

        public void CreateStorageSlot(SlotType slotType)
        {
            Debug.Log($"📦 Storage slot of type '{slotType}' requested.");
        }

        public void EquipItem(Item item)
        {
            if (item == null || item.itemType != ItemType.Clothing)
            {
                Debug.LogWarning($"❌ Cannot equip: {item?.itemName}");
                return;
            }

            item.isEquipped = true;

            if (!ownedItems.Contains(item))
                ownedItems.Add(item);

            Debug.Log($"🧥 Equipped: {item.itemName}");

            if (item.storageCapacity > 0 && item.internalStorage == null)
            {
                item.internalStorage = new List<Item>();
                Debug.Log($"🧠 Initialized storage on: {item.itemName}");
            }

            RegisterContainer(item);
        }

        public void PrintStorageDebug()
        {
            foreach (Item gear in GetActiveStorageGear())
            {
                int used = gear.internalStorage?.Count ?? 0;
                Debug.Log($"[Inventory] {gear.itemName}: {used}/{gear.storageCapacity}");
            }
        }

        public List<Item> GetAllItems()
        {
            List<Item> all = new List<Item>();
            all.AddRange(ownedItems);

            foreach (Item gear in GetActiveStorageGear())
            {
                if (gear?.internalStorage != null)
                    all.AddRange(gear.internalStorage);
            }

            return all;
        }
    }
}
