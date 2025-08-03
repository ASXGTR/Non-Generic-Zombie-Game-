using UnityEngine;
using Game.Inventory;  // Needed to recognize the Rarity enum

public static class RarityColorUtility
{
    public static Color GetColor(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return Color.gray;
            case Rarity.Uncommon:
                return Color.green;
            case Rarity.Rare:
                return Color.blue;
            case Rarity.Epic:
                return new Color(0.6f, 0f, 0.8f); // Purple
            case Rarity.Legendary:
                return new Color(1f, 0.5f, 0f);   // Orange
            default:
                return Color.white;
        }
    }
}
