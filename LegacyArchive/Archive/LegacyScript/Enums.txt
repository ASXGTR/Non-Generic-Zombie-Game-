// ARCHIVED SCRIPT — DO NOT MODIFY
// Original Role: Central enum storage across systems
// Replacement Scripts:
//   InventoryEnums.cs       — Systems/Inventory/Types
//   DiseaseEnums.cs         — Systems/Disease/Types
//   StatusEnums.cs          — Systems/Status/Types
//   AudioEnums.cs           — Systems/Audio/Types
//   UIEnums.cs              — UI/Common/Types
//   EditorEnums.cs          — Editor/Tools/Common/Types
// Date Archived: 2025-08-05
// Notes: All references have been split for modular clarity. Do not include in runtime assemblies.
using System;
using UnityEngine;

namespace Game.Inventory
{
    // ────────────────────────────────────────────────────────
    // 🧩 Item Classification
    // ────────────────────────────────────────────────────────

    public enum ItemType
    {
        Bag,
        Clothing,
        Consumable,
        Handheld,
        Holster,
        Misc
    }

    public enum ItemCategory
    {
        Gear,
        Tool,
        Weapon,
        Consumable,
        Resource,
        KeyItem,
        Junk,
        Container
    }

    public enum ToolType
    {
        None,
        Knife,
        Axe,
        Saw,
        Crowbar,
        Wrench,
        Shovel,
        Lockpick,
        Torch
        // Futureproof: MultiTool, Pickaxe, Spear
    }

    [Flags]
    public enum ItemFlag
    {
        None = 0,
        Perishable = 1 << 0,
        Throwable = 1 << 1,
        Stackable = 1 << 2,
        Unique = 1 << 3,
        QuestItem = 1 << 4,
        Ignitable = 1 << 5,
        Flammable = 1 << 6,
        Radioactive = 1 << 7,
        Hidden = 1 << 8
    }

    // ────────────────────────────────────────────────────────
    // 🥩 Consumables, Cooking, Temperature
    // ────────────────────────────────────────────────────────

    public enum ConsumableType
    {
        None,
        CookedFood,
        Drink,
        RawFood,
        SpoiledFood
        // Futureproof: Medicine, Syrup, Tea
    }

    [Flags]
    public enum CookingMethod
    {
        None = 0,
        Grill = 1 << 0,
        Boil = 1 << 1,
        Bake = 1 << 2,
        Roast = 1 << 3,
        Smoke = 1 << 4,
        Steam = 1 << 5,
        Freeze = 1 << 6,
        Microwave = 1 << 7
    }

    public enum TemperatureState
    {
        Normal,
        Warm,
        Hot,
        Cold,
        Frozen,
        Burnt
        // Futureproof: Icy, Boiling, Radiant
    }

    // ────────────────────────────────────────────────────────
    // 🌍 Environmental Context
    // ────────────────────────────────────────────────────────

    public enum EnvironmentType
    {
        Boat,
        Cave,
        Desert,
        Grassland,
        House,
        Snow,
        Urban,
        Volcanic,
        Woods
        // Futureproof: Marsh, Ruins, ToxicZone
    }

    // ────────────────────────────────────────────────────────
    // 🧥 Gear & Slot Mapping
    // ────────────────────────────────────────────────────────

    public enum ClothingSlot
    {
        Backpack,
        Belt,
        Chest,
        ChestHolster,
        BeltHolster,
        Face,
        Gloves,
        HandLeft,
        HandRight,
        Head,
        Pants,
        Shoes,
        Vest,
        Neckwear,
        Eyewear,
        Jewelry
    }

    public enum SlotType
    {
        General,
        Utility,
        Medical,
        Ammo,
        Food,
        Clothing,
        KeyItem,
        Holster,
        Crafting,
        WeaponMods,
        Documents
    }

    public enum GearSlotType
    {
        Head,
        Body,
        Legs,
        Feet,
        Hands,
        Backpack,
        Holster,
        Belt,
        Utility,
        Accessory,
        Wrist,
        Neck,
        Eyes,
        Rings,
        Pockets
    }

    // ────────────────────────────────────────────────────────
    // ⚠️ Condition & Status States
    // ────────────────────────────────────────────────────────

    public enum ItemCondition
    {
        Broken,
        Damaged,
        Good,
        Pristine,
        Worn
    }

    public enum DurabilityState
    {
        Depleted,
        InUse,
        Repaired,
        Unused
    }

    public enum WetnessLevel
    {
        Dry,
        Damp,
        Soaked
    }

    // ────────────────────────────────────────────────────────
    // 🧬 Disease & Survival Effects
    // ────────────────────────────────────────────────────────

    public enum DiseaseType
    {
        Dehydration,
        FatigueSyndrome,
        FoodPoisoning,
        Heatstroke,
        Hypothermia,
        InfectionStandard,
        Salmonella,
        ZombieVirus,
        Flu,
        Hayfever,
        Rabies,
        Asthma,
        Malaria
    }

    // ────────────────────────────────────────────────────────
    // 🌟 Rarity & Value Systems
    // ────────────────────────────────────────────────────────

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic,
        Seasonal,
        Event
    }

    // ────────────────────────────────────────────────────────
    // 🎮 UI & Player Interactions
    // ────────────────────────────────────────────────────────

    public enum InteractionType
    {
        Equip,
        Use,
        Consume,
        Store,
        Drop,
        Repair,
        Examine,
        Combine
        // Futureproof: Favorite, Inspect, Tag
    }
}
