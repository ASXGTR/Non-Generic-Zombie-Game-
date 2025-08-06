// File: Assets/Scripts/Core/Shared/CraftingRecipe.cs
using System;
using System.Collections.Generic;
using Core.Shared.Models;

namespace Core.Shared.Models
{
    [Serializable]
    public class CraftingRecipe
    {
        public string recipeName;
        public ItemData resultItem;
        public List<CraftingIngredient> requiredIngredients;
    }

    [Serializable]
    public class CraftingIngredient
    {
        public string itemName;
        public int quantity;
    }
}
