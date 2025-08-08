using UnityEngine;
using System.Collections.Generic;
using Systems;


namespace Systems
{
    public enum GearType
    {
        Helmet,
        Chest,
        Legs,
        Boots,
        Gloves
    }

    [System.Serializable]
    public class GearData
    {
        public GearType type;
        // Add other gear fields as needed
    }

    public class GearSlotUI : MonoBehaviour
    {
        public GearType type;

        public void SetGear(GearData gear) { }
        public void ClearSlot() { }
        public bool HasGear() => false;
        public GearData GetGear() => new GearData { type = type };
    }

    public class GearSlotRegistry : MonoBehaviour
    {
        private Dictionary<GearType, GearSlotUI> slotMap = new();

        public static GearSlotRegistry Instance { get; private set; }

        void Awake()
        {
            Instance = this;

            foreach (var slot in FindObjectsByType<GearSlotUI>(FindObjectsSortMode.None))
            {
                if (!slotMap.ContainsKey(slot.type))
                    slotMap.Add(slot.type, slot);
            }
        }

        public GearSlotUI GetSlot(GearType type) =>
            slotMap.TryGetValue(type, out var slot) ? slot : null;

        public GearSlotUI GetAllySlot(GearType type)
        {
            // Stubbed logic for ally slot
            return GetSlot(type);
        }

        public GearSlotUI GetStorageSlot(GearType type)
        {
            // Stubbed logic for storage slot
            return GetSlot(type);
        }

        public void AssignGear(GearData gear)
        {
            if (slotMap.TryGetValue(gear.type, out var slot))
                slot.SetGear(gear);
        }

        public void ClearAll()
        {
            foreach (var slot in slotMap.Values)
                slot.ClearSlot();
        }

        public Dictionary<GearType, GearData> GetEquippedGear()
        {
            var result = new Dictionary<GearType, GearData>();
            foreach (var kvp in slotMap)
            {
                if (kvp.Value.HasGear())
                    result.Add(kvp.Key, kvp.Value.GetGear());
            }
            return result;
        }
    }

    public class GearTransferService : MonoBehaviour
    {
        public enum TransferType { Equip, Drop, Give, Store }

        public float transferCooldown = 0.5f;
        private float lastTransferTime;

        public GearEvents gearEvents;

        public bool CanTransfer(GearData gear)
        {
            return gear != null && Time.time > lastTransferTime + transferCooldown;
        }

        public bool TransferGear(GearData gear, TransferType type)
        {
            if (!CanTransfer(gear)) return false;

            GearSlotUI targetSlot = GetTransferTarget(gear, type);
            if (targetSlot == null) return false;

            targetSlot.SetGear(gear);
            lastTransferTime = Time.time;

            gearEvents?.OnTransferComplete?.Invoke(gear, type);
            return true;
        }

        private GearSlotUI GetTransferTarget(GearData gear, TransferType type)
        {
            var registry = GearSlotRegistry.Instance;
            return type switch
            {
                TransferType.Equip => registry.GetSlot(gear.type),
                TransferType.Drop => null,
                TransferType.Give => registry.GetAllySlot(gear.type),
                TransferType.Store => registry.GetStorageSlot(gear.type),
                _ => null
            };
        }
    }



    public class GearEvents : MonoBehaviour
    {
        public System.Action<GearData, GearTransferService.TransferType> OnTransferComplete;
    }
}
