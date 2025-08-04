using UnityEngine;
using Game.Inventory; // Recognizes the Rarity enum

/// <summary>
/// Maps item rarity levels to standardized UI colors.
/// </summary>
public static class RarityColorUtility
{
    /// <summary>
    /// Gets the base color for a given rarity with full opacity.
    /// </summary>
    /// <param name="rarity">The item's rarity enum.</param>
    /// <returns>Color with alpha = 1.</returns>
    public static Color GetColor(Rarity rarity)
    {
        return GetColor(rarity, 1f);
    }

    /// <summary>
    /// Gets the base rarity color adjusted for transparency.
    /// </summary>
    /// <param name="rarity">The item's rarity enum.</param>
    /// <param name="alpha">Alpha (0–1).</param>
    /// <returns>Color adjusted for UI overlays, highlights, etc.</returns>
    public static Color GetColor(Rarity rarity, float alpha)
    {
        Color color = rarity switch
        {
            Rarity.Common => new Color(0.6f, 0.6f, 0.6f),  // Soft gray
            Rarity.Uncommon => Color.green,
            Rarity.Rare => Color.blue,
            Rarity.Epic => new Color(0.6f, 0f, 0.8f),   // Purple
            Rarity.Legendary => new Color(1f, 0.5f, 0f),     // Orange
            _ => Color.white                 // Fallback
        };

        color.a = Mathf.Clamp01(alpha);
        return color;
    }
}
