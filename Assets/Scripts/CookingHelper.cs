using System.Collections.Generic;
using System.Linq;
using Game.Inventory;

public static class CookingHelper
{
    /// <summary>
    /// Checks if the item can be cooked using the given method and available resources.
    /// </summary>
    public static bool CanCook(
        ItemData item,
        CookingMethod method,
        List<string> playerItems,
        string heatSource,
        List<string> nearbyStructures,
        bool isNearOven)
    {
        var requirement = item.cookingRequirements.Find(r => r.method == method);
        if (requirement == null) return false;

        foreach (var neededItem in requirement.requiredItems)
        {
            if (!playerItems.Contains(neededItem))
                return false;
        }

        if (!requirement.validHeatSources.Contains(heatSource))
            return false;

        foreach (var structure in requirement.requiredStructures)
        {
            if (!nearbyStructures.Contains(structure))
                return false;
        }

        if (requirement.requiresIndoorOven && !isNearOven)
            return false;

        return true;
    }

    /// <summary>
    /// Returns a list of cooking methods the player *can currently perform* on the given item.
    /// </summary>
    public static List<CookingMethod> GetValidCookingMethods(
        ItemData item,
        List<string> playerItems,
        string heatSource,
        List<string> nearbyStructures,
        bool isNearOven)
    {
        List<CookingMethod> validMethods = new List<CookingMethod>();

        foreach (var requirement in item.cookingRequirements)
        {
            if (CanCook(item, requirement.method, playerItems, heatSource, nearbyStructures, isNearOven))
            {
                validMethods.Add(requirement.method);
            }
        }

        return validMethods;
    }

    /// <summary>
    /// Returns a human-readable list of missing requirements for a given method.
    /// </summary>
    public static List<string> GetMissingRequirements(
        ItemData item,
        CookingMethod method,
        List<string> playerItems,
        string heatSource,
        List<string> nearbyStructures,
        bool isNearOven)
    {
        List<string> missing = new List<string>();

        var requirement = item.cookingRequirements.Find(r => r.method == method);
        if (requirement == null)
        {
            missing.Add("No recipe defined for this method.");
            return missing;
        }

        foreach (var itemNeeded in requirement.requiredItems)
        {
            if (!playerItems.Contains(itemNeeded))
                missing.Add($"Missing item: {itemNeeded}");
        }

        if (!requirement.validHeatSources.Contains(heatSource))
            missing.Add($"Invalid heat source: {heatSource}");

        foreach (var structure in requirement.requiredStructures)
        {
            if (!nearbyStructures.Contains(structure))
                missing.Add($"Missing structure: {structure}");
        }

        if (requirement.requiresIndoorOven && !isNearOven)
            missing.Add("Must be near an indoor oven.");

        return missing;
    }
}

/// <summary>
/// Define your EnvironmentType enum somewhere in your project (or in this file if you prefer).
/// </summary>
public enum EnvironmentType
{
    Woods,
    House,
    Boat,
    Snow,
    Cave,
    Desert,
    Grassland
}

/// <summary>
/// Separate static class for environment cooking rules.
/// </summary>
public static class EnvironmentCookingRules
{
    // Mapping environment names (strings) to allowed cooking methods.
    public static Dictionary<string, List<CookingMethod>> environmentAllowedMethods = new Dictionary<string, List<CookingMethod>>
    {
        { "Woods", new List<CookingMethod> { CookingMethod.Roast, CookingMethod.Boil, CookingMethod.Grill } },
        { "House", new List<CookingMethod> { CookingMethod.Bake, CookingMethod.Grill } },
        { "Boat",  new List<CookingMethod> { CookingMethod.Boil } },
        { "Snow",  new List<CookingMethod> { CookingMethod.Boil } },
        { "Cave",  new List<CookingMethod> { CookingMethod.Roast } },
        { "Desert",new List<CookingMethod> { CookingMethod.Roast } },
        { "Grassland", new List<CookingMethod> { CookingMethod.Roast, CookingMethod.Grill } },
    };

    public static bool IsMethodAllowed(string environment, CookingMethod method)
    {
        if (environmentAllowedMethods.TryGetValue(environment, out var methods))
            return methods.Contains(method);

        return false;
    }

    // Returns a list of available cooking stations depending on environment enum
    public static List<string> GetAvailableStations(EnvironmentType env)
    {
        switch (env)
        {
            case EnvironmentType.Woods:
            case EnvironmentType.Grassland:
                return new List<string> { "Campfire", "CookingFire" };

            case EnvironmentType.House:
                return new List<string> { "Oven", "Grill" };

            case EnvironmentType.Boat:
                return new List<string> { "CampingStove", "Grill" };

            case EnvironmentType.Cave:
            case EnvironmentType.Snow:
                return new List<string> { "Campfire", "CampingStove" };

            case EnvironmentType.Desert:
                return new List<string> { "Campfire" };

            default:
                return new List<string>();
        }
    }
}
