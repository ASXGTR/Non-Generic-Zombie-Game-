using Core.Shared.Models;       // ✅ For ItemData
using Systems.Cooking;          // ✅ For CookingValidator, CookingOutcomeCalculator, RecipeData
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
                Debug.Log($"Burn chance: {burnChance}%");
            }
        }
    }
}
