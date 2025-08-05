using UnityEngine;
using System.Collections.Generic;
using Inventory.DataModels;
using UI.Inventory;

namespace Systems.Equipment
{
    public class GearSlotRegistry : MonoBehaviour
    {
        private Dictionary<GearType, GearSlotUI> slotMap = new();

        void Awake()
        {
            foreach (var slot in FindObjectsByType<GearSlotUI>(FindObjectsSortMode.None))
            {
                if (!slotMap.ContainsKey(slot.type))
                    slotMap.Add(slot.type, slot);
            }
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
}
