// File: Assets/Scripts/Crafting/CraftingManager.cs

using Core.Shared.Models;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[AddComponentMenu("Crafting/Crafting Manager")]
public class CraftingManager : MonoBehaviour
{
    [Header("Dependencies")]
    private Game.Inventory.InventorySystem inventorySystem;
    private Game.Inventory.EquipmentManager equipmentManager;

    [Header("Crafting Recipes")]
    [SerializeField] private List<CraftingRecipe> recipes = new();

    void Awake()
    {
        inventorySystem = Object.FindFirstObjectByType<Game.Inventory.InventorySystem>();
        equipmentManager = Object.FindFirstObjectByType<Game.Inventory.EquipmentManager>();

        if (inventorySystem == null)
            Debug.LogError("[CraftingManager] ❌ InventorySystem not found.");

        if (equipmentManager == null)
            Debug.LogWarning("[CraftingManager] ⚠️ EquipmentManager not found. Gear checks will be skipped.");
    }

    public bool CanCraft(string recipeId)
    {
        var recipe = recipes.FirstOrDefault(r => r.id == recipeId);
        if (recipe == null)
        {
            Debug.LogWarning($"[CraftingManager] ❌ Recipe '{recipeId}' not found.");
            return false;
        }

        foreach (var req in recipe.requiredItems)
        {
            int count = inventorySystem.CountItemByName(req.itemName);
            if (count < req.quantity)
            {
                Debug.LogWarning($"[CraftingManager] ❌ Missing '{req.itemName}' x{req.quantity} (have {count})");
                return false;
            }
        }

        if (recipe.requiredGearSlots != null)
        {
            foreach (var gearReq in recipe.requiredGearSlots)
            {
                var equipped = equipmentManager?.GetEquippedItem(gearReq.slotName);
                if (equipped == null || equipped.Data.ItemName != gearReq.itemName)
                {
                    Debug.LogWarning($"[CraftingManager] ❌ Missing equipped '{gearReq.itemName}' in '{gearReq.slotName}'");
                    return false;
                }
            }
        }

        return true;
    }

    public void Craft(string recipeId)
    {
        if (!CanCraft(recipeId)) return;

        var recipe = recipes.First(r => r.id == recipeId);

        foreach (var req in recipe.requiredItems)
        {
            inventorySystem.RemoveItemsByName(req.itemName, req.quantity);
        }

        var resultInstance = new ItemInstance
        {
            Data = recipe.resultItem,
            Quantity = recipe.resultQuantity
        };

        inventorySystem.AddItem(resultInstance, recipe.resultQuantity);

        Debug.Log($"[CraftingManager] 🛠️ Crafted '{recipe.resultItem.ItemName}' x{recipe.resultQuantity}");
    }
}
