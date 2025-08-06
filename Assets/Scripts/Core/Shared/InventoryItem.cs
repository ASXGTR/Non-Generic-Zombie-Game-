// File: Assets/Scripts/Core/Shared/InventoryItem.cs
using Core.Shared.Enums;
using UnityEngine;

namespace Core.Shared.Models
{
    [CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/Item")]
    public class InventoryItem : ScriptableObject
    {
        [Header("Basic Info")]
        public string ItemName;
        public Sprite Icon;
        public ItemTypeEnum Type;
        [TextArea] public string Description;

        [Header("Stacking")]
        public bool IsStackable = false;
        public int MaxStackSize = 1;

        [Header("Metadata")]
        public string Category;
        public int BaseValue;
        public bool IsQuestItem;
        public bool IsConsumable;

        [Header("Debug")]
        public bool ShowInDebugLog = false;

        public override string ToString()
        {
            return $"{ItemName} ({Type}) - {Category} | Stackable: {IsStackable} x{MaxStackSize}";
        }
    }
}
