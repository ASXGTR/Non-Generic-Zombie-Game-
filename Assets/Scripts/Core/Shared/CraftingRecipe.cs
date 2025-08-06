using Game.Inventory;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;

namespace Game.Core.Shared
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
