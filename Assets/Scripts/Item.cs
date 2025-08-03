using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class Item
    {
        public ItemData data;

        // Core Info
        public string itemName;
        public string itemDescription;
        public ItemType itemType;
        public Rarity rarity;
        public float condition;
        public float weight = 1f;

        // Visuals
        public Sprite icon;
        public string id => data != null ? data.id : "";

        // Gear Slots
        public ClothingSlot clothingSlot;
        public SlotType slotType;
        public GearSlotType gearSlotType;

        // Inventory Support
        public bool isEquipped = false;
        public bool isContainer;
        public int storageCapacity = 0;
        public List<SlotType> slotTypes = new();
        public List<Item> internalStorage;

        // Cooking
        public bool isHot;
        public float hotDuration;

        public Item(ItemData sourceData)
        {
            data = sourceData;

            itemName = data.itemName;
            itemDescription = data.itemDescription;
            itemType = data.itemType;
            rarity = data.rarity;
            condition = data.condition;
            weight = data.weight > 0f ? data.weight : 1f;

            icon = data.Icon;
            clothingSlot = data.clothingSlot;
            slotType = data.slotType;
            gearSlotType = data.gearSlotType;

            isEquipped = false;
            isHot = data.isHot;
            hotDuration = data.hotDuration;
            isContainer = data.isContainer;

            if (itemType == ItemType.Bag || isContainer || data.containerCapacity > 0 || data.storageSlotCapacity > 0)
            {
                internalStorage = new List<Item>();
                storageCapacity = data.storageSlotCapacity > 0 ? data.storageSlotCapacity : data.containerCapacity;
                slotTypes = data.slotTypes != null
                    ? new List<SlotType>(data.slotTypes)
                    : new List<SlotType>();
            }
        }

        public bool CanBeStored()
        {
            return internalStorage == null || internalStorage.Count == 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Item other && data == other.data;
        }

        public override int GetHashCode()
        {
            return data != null ? data.GetHashCode() : base.GetHashCode();
        }
    }
}
