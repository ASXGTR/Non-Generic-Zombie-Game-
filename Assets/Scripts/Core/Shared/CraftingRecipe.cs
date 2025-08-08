// File: Assets/Scripts/Core/Shared/CraftingRecipe.cs

using Core.Shared.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Shared.Models
{
    [Serializable]
    public class CraftingRecipe
    {
        [Tooltip("Unique internal ID for this recipe (used for lookup)")]
        public string id;

        [Tooltip("Display name for the recipe")]
        public string recipeName;

        [Tooltip("Item produced by crafting")]
        public ItemData resultItem;

        [Tooltip("Quantity of result item")]
        public int resultQuantity = 1;

        [Tooltip("Items required to craft")]
        public List<CraftingIngredient> requiredItems; // ✅ Renamed from requiredIngredients

        [Tooltip("Optional gear requirements")]
        public List<GearRequirement> requiredGearSlots;
    }

    [Serializable]
    public class CraftingIngredient
    {
        [Tooltip("Name of the required item")]
        public string itemName;

        [Tooltip("Quantity required")]
        public int quantity;
    }

    [Serializable]
    public class GearRequirement
    {
        [Tooltip("Slot name (e.g. Hands, Head, Body)")]
        public string slotName;

        [Tooltip("Required item name in that slot")]
        public string itemName;
    }
}
