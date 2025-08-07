using System;

namespace Core.Shared.Enums
{
    /// <summary>
    /// Defines logical gear slot categories for equippable items.
    /// Used for filtering, UI mapping, and gameplay logic.
    /// </summary>
    [Serializable]
    public enum GearSlotType
    {
        /// <summary>Headwear like helmets, hats, or masks.</summary>
        Head,

        /// <summary>Torso gear such as jackets, armor, or shirts.</summary>
        Torso,

        /// <summary>Legwear including pants or armor plating.</summary>
        Legs,

        /// <summary>Footwear like boots or shoes.</summary>
        Feet,

        /// <summary>Gloves or hand-based gear.</summary>
        Hands,

        /// <summary>Main weapon slot (melee or ranged).</summary>
        Weapon,

        /// <summary>Offhand slot for shields, secondary weapons, or tools.</summary>
        Offhand,

        /// <summary>Accessories like rings, necklaces, or trinkets.</summary>
        Accessory,

        /// <summary>Back-mounted gear such as backpacks or quivers.</summary>
        Backpack,

        /// <summary>Holsters for sidearms or throwable gear.</summary>
        Holster,

        /// <summary>Utility gear like belts, pouches, or gadgets.</summary>
        Utility,

        /// <summary>Container-type gear that can store other items.</summary>
        Container
    }
}
