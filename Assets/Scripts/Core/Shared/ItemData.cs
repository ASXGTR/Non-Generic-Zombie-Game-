// File: Assets/Scripts/Core/Shared/Models/ItemData.cs

using System.Collections.Generic;
using UnityEngine;
using Core.Shared.Enums;

namespace Core.Shared.Models
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        [Header("🆔 Basic Info")]
        [Tooltip("Unique item ID (use GUID or meaningful string)")]
        [SerializeField] private string id;
        public string Id => string.IsNullOrEmpty(id) ? "Unnamed_Item" : id;

        [Tooltip("Item name used in UI")]
        [SerializeField] private string itemName;
        public string ItemName => string.IsNullOrEmpty(itemName) ? "Unnamed" : itemName;

        [Tooltip("Main tooltip description")]
        [TextArea(3, 5)]
        [SerializeField] private string tooltipText;
        public string TooltipText => tooltipText;

        [Header("🎨 Visuals")]
        [Tooltip("Main UI icon")]
        public Sprite icon;

        [Tooltip("Optional override when equipped")]
        public Sprite equippedSprite;

        [Header("⚙️ Item Properties")]
        [Tooltip("Item type used for slot compatibility")]
        public ItemTypeEnum Type;

        [Tooltip("Rarity tier for loot tables or display")]
        public ItemRarity Rarity;

        [Tooltip("Durability or condition (0–100)")]
        [Range(0f, 100f)]
        public float condition = 100f;

        [Tooltip("Weight of the item in inventory calculations")]
        [SerializeField] private float weight = 1f;
        public float Weight => weight;

        [Header("👕 Equip & Slot Info")]
        [Tooltip("How many slots this item occupies when equipped")]
        public int slotFootprint = 1;

        [Tooltip("Compatible slot types for equipping")]
        public List<ItemSlot> slotTypes = new();

        [Header("🔥 Cooking")]
        [Tooltip("Whether the item is hot")]
        public bool isHot = false;

        [Tooltip("Duration the item remains hot")]
        public float hotDuration = 0f;

        [Header("📦 Container Settings")]
        [Tooltip("Whether this item can store other items")]
        public bool isContainer = false;

        [Tooltip("Max number of items this container can hold")]
        public int containerCapacity = 0;

        [Tooltip("Max number of slots this container exposes")]
        public int storageSlotCapacity = 0;

        [Tooltip("Max number of items allowed in internal storage")]
        [SerializeField] private int storageCapacity = 0;
        public int StorageCapacity => storageCapacity;

        [Header("📚 Stacking")]
        [Tooltip("Maximum stack size for this item")]
        [SerializeField] private int maxStackSize = 1;
        public int MaxStackSize => maxStackSize;

        [Header("🏷️ Tags & Metadata")]
        [Tooltip("Custom tags for filtering, sorting, or logic")]
        [SerializeField] private List<string> tags = new();
        public List<string> Tags => tags;

        // ──────────────────────────────────────
        // 🧠 Utility Accessors & Helpers
        // ──────────────────────────────────────

        public bool IsStackable =>
            !isContainer && condition == 100f && weight < 5f;

        public bool HasStorage =>
            isContainer && containerCapacity > 0;

        public bool HasTag(string query) =>
            tags != null && tags.Contains(query);

        public override string ToString() =>
            $"{ItemName} ({Type}) {(IsStackable ? $"Stack x{MaxStackSize}" : "Single")}";
    }
}
