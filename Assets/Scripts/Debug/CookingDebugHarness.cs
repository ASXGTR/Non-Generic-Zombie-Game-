using Game.Inventory;
using Inventory.DataModels;
using Systems.Cooking;
using UnityEngine;

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
