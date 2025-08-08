// File: Assets/Scripts/Systems/Cooking/CookingPlaceholders.cs

namespace Systems.Cooking
{
    public static class CookingValidator
    {
        public static bool CanBeCooked(object ingredient) => true;
    }

    public static class CookingOutcomeCalculator
    {
        public static float GetBurnChance(object recipe, object[] ingredients) => 0f;
    }

    public class RecipeData
    {
        public string recipeName = "Placeholder Recipe";
        public int cookTime = 0;
    }
}
