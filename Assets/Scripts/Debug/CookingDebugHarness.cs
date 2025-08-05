using Game.Inventory;
using Inventory;
using Inventory.DataModels;
using Systems.Cooking;
using UnityEngine;

namespace Debug
{
    public class CookingDebugHarness : MonoBehaviour
    {
        public ItemData[] testIngredients;
        public RecipeData recipe;

        void Start()
        {
            if (CookingValidator.CanBeCooked(testIngredients[0]))
            {
                float burnChance = CookingOutcomeCalculator.GetBurnChance(recipe, testIngredients);
                UnityEngine.Debug.Log($"Burn chance: {burnChance}%");
            }
        }
    }
}
