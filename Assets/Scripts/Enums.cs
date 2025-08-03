namespace Game.Inventory
{
    /// <summary>
    /// High-level categorization for item behavior and use.
    /// </summary>
    public enum ItemType
    {
        Bag,
        Clothing,
        Consumable,
        Handheld,
        Holster,
        Misc
    }

    /// <summary>
    /// Subcategories for Consumable items.
    /// </summary>
    public enum ConsumableType
    {
        None,
        CookedFood,
        Drink,
        RawFood,
        SpoiledFood
    }

    /// <summary>
    /// Flags for cooking methods to allow multiple techniques per item.
    /// </summary>
    [System.Flags]
    public enum CookingMethod
    {
        None = 0,
        Grill = 1 << 0,
        Boil = 1 << 1,
        Bake = 1 << 2,
        Roast = 1 << 3
        // Futureproof: Add Smoke, Steam, Freeze, etc.
    }

    /// <summary>
    /// Categorization for environmental zones or biome states.
    /// </summary>
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
    }

    /// <summary>
    /// Gear slot mapping for clothing and equipment.
    /// </summary>
    public enum ClothingSlot
    {
        Backpack,
        Belt,
        Chest,
        ChestHolster,
        BeltHolster,     // ✅ NEW: Waist-mounted holsters
        Face,
        Gloves,
        HandLeft,
        HandRight,
        Head,
        Pants,
        Shoes,
        Vest
        // Futureproof: Neckwear, Eyewear, Jewelry, etc.
    }

    /// <summary>
    /// Defines what kind of storage slots a gear item can contribute.
    /// </summary>
    public enum SlotType
    {
        General,
        Utility,
        Medical,
        Ammo,
        Food,
        Clothing,
        KeyItem,
        Holster           // ✅ NEW: Slot type for mounted containers
        // Futureproof: Crafting, WeaponMods, Documents, etc.
    }

    /// <summary>
    /// UI-level mapping for gear slot visuals and equip zones.
    /// </summary>
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
        Accessory
        // Futureproof: Wrist, Neck, Eyes, Rings, Pockets, etc.
    }

    /// <summary>
    /// Item condition impacts stats, durability, or comfort.
    /// </summary>
    public enum Condition
    {
        Broken,
        Damaged,
        Good,
        Pristine,
        Worn
    }

    /// <summary>
    /// Gear wetness levels—used for survival impact or insulation loss.
    /// </summary>
    public enum Wetness
    {
        Dry,
        Damp,
        Soaked
    }

    /// <summary>
    /// Item rarity—for loot tables, trade value, or crafting bonuses.
    /// </summary>
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
        // Optional: Mythic, Seasonal, Event-based
    }

    /// <summary>
    /// Item lifecycle stage—used for dynamic gear or restorative systems.
    /// </summary>
    public enum DurabilityState
    {
        Depleted,
        InUse,
        Repaired,
        Unused
    }

    /// <summary>
    /// Disease types for survival mechanics and condition tracking.
    /// </summary>
    public enum DiseaseType
    {
        Dehydration,
        FatigueSyndrome,
        FoodPoisoning,
        Heatstroke,
        Hypothermia,
        Infection,
        Salmonella,
        ZombieVirus
    }
}
