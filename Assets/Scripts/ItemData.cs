using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        [Header("Core Info")]
        public string id;
        public string itemName;
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        public string itemDescription;
        public ItemType itemType;
        public Rarity rarity;
        public float condition = 100f;

        [Header("Slot Assignment")]
        public ClothingSlot clothingSlot;
        public SlotType slotType;
        public GearSlotType gearSlotType;
        public List<SlotType> slotTypes = new List<SlotType>();

        [Header("Consumable Properties")]
        public ConsumableType consumableType;
        public float hungerRestore = 0f;
        public float hydrationRestore = 0f;
        public float sicknessChance = 0f;

        [Header("Cooking System")]
        public bool canBeCooked = false;
        public CookingMethod cookMethod;
        public ItemData cookedVariantBoil;
        public ItemData cookedVariantGrill;
        public ItemData cookedVariantBake;
        public ItemData cookedVariantRoast;
        public List<CookingRequirement> cookingRequirements = new List<CookingRequirement>();
        public bool isHot = false;
        public float hotDuration = 0f;

        [Header("Durability & Container Settings")]
        public float maxDurability = 0f;
        public bool isContainer = false;
        public int containerCapacity = 0;
        public int storageCapacity = 0;
        public int storageSlotCapacity = 0;
        public float weight = 1f;

        [Header("Tags")]
        public List<string> tags = new List<string>();

        [Header("Clothing Visuals")]
        [SerializeField] private Sprite equippedSprite;         // 👕 Icon shown when worn
        [SerializeField] private GameObject clothingPrefab;     // 🧍 Optional for in-world visuals

        public Sprite EquippedSprite => equippedSprite;
        public GameObject ClothingPrefab => clothingPrefab;
    }

    [System.Serializable]
    public class CookingRequirement
    {
        public CookingMethod method;
        public List<string> requiredItems = new List<string>();
        public List<string> validHeatSources = new List<string>();
        public List<string> requiredStructures = new List<string>();
        public bool requiresIndoorOven = false;
    }
}
