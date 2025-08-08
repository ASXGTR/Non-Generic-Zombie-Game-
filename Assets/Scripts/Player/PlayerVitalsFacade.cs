using UnityEngine;
using InventorySystem; // ✅ This brings in your actual Item.cs

public class PlayerVitalsFacade : MonoBehaviour
{
    public int health;
    public int stamina;
    public Item equippedItem;

    public void Initialize(int startingHealth, int startingStamina, Item startingItem)
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

    public void EquipItem(Item newItem)
    {
        equippedItem = newItem;
    }
}
