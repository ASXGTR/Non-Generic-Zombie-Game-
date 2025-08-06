using Core.Shared.Models;
using UnityEngine;
using Inventory.DataModels;

namespace UI.Styling
{
    public static class RarityColorMap
    {
        public static Color GetColor(RarityType rarity)
        {
            return rarity switch
            {
                RarityType.Common => new Color(0.6f, 0.6f, 0.6f),
                RarityType.Uncommon => new Color(0.3f, 0.8f, 0.3f),
                RarityType.Rare => new Color(0.2f, 0.4f, 0.9f),
                RarityType.Epic => new Color(0.6f, 0.2f, 0.8f),
                RarityType.Legendary => new Color(1.0f, 0.6f, 0.0f),
                _ => Color.white
            };
        }
    }
}
