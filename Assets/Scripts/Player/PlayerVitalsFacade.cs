using UnityEngine;
using Core.Shared.Models;

public class PlayerVitalsFacade : MonoBehaviour
{
    public int health;
    public int stamina;
    public ItemInstance equippedItem;

    public void Initialize(int startingHealth, int startingStamina, ItemInstance startingItem)
    {
        health = startingHealth;
        stamina = startingStamina;
        equippedItem = startingItem;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0) health = 0;
    }

    public void UseStamina(int amount)
    {
        stamina -= amount;
        if (stamina < 0) stamina = 0;
    }

    public void EquipItem(ItemInstance newItem)
    {
        equippedItem = newItem;
    }
}
