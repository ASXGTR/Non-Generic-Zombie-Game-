using Core.Shared.Enums;
using UnityEngine;

namespace UI.Styling
{
    public static class RarityColorMap
    {
        public static Color GetColor(ItemRarity rarity)
        {
            return rarity switch
            {
                ItemRarity.Common => new Color(0.6f, 0.6f, 0.6f),
                ItemRarity.Uncommon => new Color(0.3f, 0.8f, 0.3f),
                ItemRarity.Rare => new Color(0.2f, 0.4f, 0.9f),
                ItemRarity.Epic => new Color(0.6f, 0.2f, 0.8f),
                ItemRarity.Legendary => new Color(1.0f, 0.6f, 0.0f),
                _ => Color.white
            };
        }
    }
}
