using UnityEngine;
using System.Collections.Generic;
using Game.Inventory;

namespace Game.Crafting
{
    /// <summary>
    /// Handles crafting logic using predefined recipes and available inventory.
    /// </summary>
    public class CraftingManager : MonoBehaviour
    {
        [Header("References")]
        public Inventory playerInventory;
        public List<CraftingRecipe> availableRecipes;

        private const string logTag = "[CraftingManager]";

        /// <summary>
        /// Attempts to craft an item by recipe name.
        /// </summary>
        public bool TryCraft(string recipeName)
        {
            CraftingRecipe recipe = availableRecipes.Find(r => r.recipeName == recipeName);
            if (recipe == null)
            {
                Debug.LogWarning($"{logTag} ❌ Recipe '{recipeName}' not found.");
                return false;
            }

            if (!HasRequiredIngredients(recipe))
            {
                Debug.LogWarning($"{logTag} ⚠️ Missing ingredients for '{recipeName}'.");
                return false;
            }

            ConsumeIngredients(recipe);
            CreateCraftedItem(recipe);
            Debug.Log($"{logTag} ✅ Crafted: {recipe.resultItem.ItemName}");
            return true;
        }

        private bool HasRequiredIngredients(CraftingRecipe recipe)
        {
            foreach (var ingredient in recipe.requiredIngredients)
            {
                int count = playerInventory.CountItemByName(ingredient.itemName);
                if (count < ingredient.quantity)
                    return false;
            }
            return true;
        }

        private void ConsumeIngredients(CraftingRecipe recipe)
        {
            foreach (var ingredient in recipe.requiredIngredients)
            {
                playerInventory.RemoveItemsByName(ingredient.itemName, ingredient.quantity);
            }
        }

        private void CreateCraftedItem(CraftingRecipe recipe)
        {
            GameObject itemGO = new(recipe.resultItem.ItemName);
            InventoryItem newItem = itemGO.AddComponent<InventoryItem>();
            newItem.LoadFromData(recipe.resultItem);

            playerInventory.AddItem(newItem);
        }
    }

    [System.Serializable]
    public class CraftingRecipe
    {
        public string recipeName;
        public ItemData resultItem;
        public List<CraftingIngredient> requiredIngredients;
    }

    [System.Serializable]
    public class CraftingIngredient
    {
        public string itemName;
        public int quantity;
    }
}
