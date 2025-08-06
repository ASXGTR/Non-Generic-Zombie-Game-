using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEditor.Graphs;
using UnityEngine;

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
        // public ItemTypeEnum itemType;
        // public Rarity rarity;
        public float condition = 100f;
        public float weight = 1f;

        [Header("👕 Equip & Slot Info")]
        // public ClothingSlot clothingSlot;
        // public SlotType slotType;
        // public GearSlotType gearSlotType;
        public int slotFootprint = 1;

        [Header("🔥 Cooking")]
        public bool isHot = false;
        public float hotDuration = 0f;

        [Header("📦 Container Settings")]
        public bool isContainer = false;
        public int containerCapacity = 0;
        public int storageSlotCapacity = 0;
        public int storageCapacity = 0;
        public List<object> slotTypes = new(); // Temporarily use object

        [Header("🏷️ Tags & Metadata")]
        [SerializeField] private List<string> tags = new();
        public List<string> Tags => tags;

        // ──────────────────────────────────────
        // 🧠 Utility Accessors & Helpers
        // ──────────────────────────────────────

        // public bool IsEquippable =>
        //     itemType == ItemTypeEnum.Clothing || itemType == ItemTypeEnum.Holster;

        public bool IsStackable =>
            !isContainer && condition == 100f && weight < 5f;

        public bool HasStorage =>
            isContainer && containerCapacity > 0;

        // public bool AcceptsSlotType(SlotType type) =>
        //     slotTypes != null && slotTypes.Contains(type);

        public bool HasTag(string query) =>
            tags != null && tags.Contains(query);
    }
}
