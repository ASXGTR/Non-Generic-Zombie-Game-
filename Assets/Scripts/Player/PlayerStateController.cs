using UnityEngine;
using InventorySystem;
using Core.Shared.Models;

public class PlayerStateController : MonoBehaviour
{
    [Header("Subsystem References")]
    public PlayerVitalsFacade vitals;
    public PlayerConditionManager conditionManager;

    [Header("Equipped Item")]
    public Item equippedItem;

    private void Awake()
    {
        // Optional: auto-fetch components if not assigned
        if (vitals == null) vitals = GetComponentInChildren<PlayerVitalsFacade>();
        if (conditionManager == null) conditionManager = GetComponentInChildren<PlayerConditionManager>();
    }

    public void InitializePlayerState(int health, int stamina, Item startingItem)
    {
        vitals.Initialize(health, stamina, startingItem);
        equippedItem = startingItem;
    }

    public void ApplyCondition(PlayerConditionFlags condition)
    {
        conditionManager.AddCondition(condition);
    }

    public void RemoveCondition(PlayerConditionFlags condition)
    {
        conditionManager.RemoveCondition(condition);
    }

    public bool HasCondition(PlayerConditionFlags condition)
    {
        return conditionManager.HasCondition(condition);
    }

    public void EquipItem(Item newItem)
    {
        equippedItem = newItem;
        vitals.EquipItem(newItem);
    }

    // Placeholder for future systems
    public void ApplyDamage(int amount)
    {
        vitals.TakeDamage(amount);
    }

    public void UseStamina(int amount)
    {
        vitals.UseStamina(amount);
    }
}
