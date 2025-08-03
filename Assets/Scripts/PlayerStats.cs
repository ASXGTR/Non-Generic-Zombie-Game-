using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public float temperature;
    public int hydration;
    public int hunger;
    public int sickness;
    public int stamina;

    [HideInInspector] public GameManager gameManager;

    public Inventory playerInventory;
    public Equipment playerEquipment;

    private bool isResting = false;
    private bool isSleeping = false;
    private int staminaRecoveryTarget = 0;

    void Start()
    {
        SetInitialStats();
    }

    public void SetInitialStats()
    {
        health = 75;
        hydration = 75;
        hunger = 75;
        stamina = 75;
        sickness = 20;
        temperature = 20f;
    }

    public void InitializeStartingGear(ItemDatabaseSO db)
    {
        if (db == null)
        {
            Debug.LogError("ItemDatabaseSO is null! Cannot initialize starting gear.");
            return;
        }

        if (playerInventory == null || playerEquipment == null)
        {
            Debug.LogError("Inventory or Equipment reference missing!");
            return;
        }

        var tshirt = db.allItems.Find(i => i.itemName == "T-Shirt");
        var jeans = db.allItems.Find(i => i.itemName == "Jeans");

        if (tshirt != null)
        {
            Item gear = new Item(tshirt);
            playerInventory.EquipItem(gear);
            playerInventory.ownedItems.Add(gear);
        }
        else Debug.LogWarning("Starting T-Shirt not found in database!");

        if (jeans != null)
        {
            Item gear = new Item(jeans);
            playerInventory.EquipItem(gear);
            playerInventory.ownedItems.Add(gear);
        }
        else Debug.LogWarning("Starting Jeans not found in database!");

        var glowstick = db.allItems.Find(i => i.itemName == "Glowstick");
        var clothRag = db.allItems.Find(i => i.itemName == "Cloth Rag");

        if (glowstick != null)
            playerInventory.AddItem(new Item(glowstick));
        else
            Debug.LogWarning("Glowstick not found in database!");

        if (clothRag != null)
            playerInventory.AddItem(new Item(clothRag));
        else
            Debug.LogWarning("Cloth Rag not found in database!");

        List<string> fruits = new List<string> { "Apple", "Pear", "Banana" };
        string chosenFruitName = fruits[Random.Range(0, fruits.Count)];
        var fruit = db.allItems.Find(i => i.itemName == chosenFruitName);

        if (fruit != null)
            playerInventory.AddItem(new Item(fruit));
        else
            Debug.LogWarning($"Fruit '{chosenFruitName}' not found in database!");

        Debug.Log($"🎒 Starting gear initialized: T-Shirt, Jeans, Glowstick, Cloth Rag, and {chosenFruitName}");

        ApplyClothingStatModifiers();
    }

    public void ApplyEffects(Effects effects)
    {
        if (effects == null)
        {
            Debug.LogWarning("ApplyEffects called with null Effects object.");
            return;
        }

        health += effects.healthChange;
        temperature += effects.temperatureChange;
        hydration += effects.hydrationChange;
        hunger += effects.hungerChange;
        sickness += effects.sicknessChange;
        stamina += effects.staminaChange;

        ClampStats();

        if (hydration <= 0)
        {
            health -= 5;
            Debug.Log("⚠️ Dehydrated! Losing health.");
        }

        if (hunger <= 0)
        {
            health -= 5;
            Debug.Log("⚠️ Starving! Losing health.");
        }

        if (temperature <= -10f || temperature >= 60f)
        {
            health -= 10;
            Debug.Log("⚠️ Temperature extremes! Losing health.");
        }

        if (sickness >= 75)
        {
            health -= 7;
            Debug.Log("⚠️ Very sick! Losing health.");
        }

        health = Mathf.Clamp(health, 0, 100);

        if (health <= 0 && gameManager != null)
        {
            gameManager.TriggerGameOver();
        }
    }

    private void ClampStats()
    {
        temperature = Mathf.Clamp(temperature, -30f, 100f);
        hydration = Mathf.Clamp(hydration, 0, 100);
        hunger = Mathf.Clamp(hunger, 0, 100);
        sickness = Mathf.Clamp(sickness, 0, 100);
        stamina = Mathf.Clamp(stamina, 0, 100);
    }

    public void ResetStats()
    {
        health = 100;
        temperature = 20f;
        hydration = 100;
        hunger = 100;
        sickness = 0;
        stamina = 100;
    }

    public void StartRest()
    {
        isResting = true;
        isSleeping = false;
        staminaRecoveryTarget = Mathf.Min(stamina + 25, 100);
        Debug.Log("🪑 Resting... target stamina: " + staminaRecoveryTarget);
    }

    public void StartSleep()
    {
        isSleeping = true;
        isResting = false;
        staminaRecoveryTarget = Mathf.Min(stamina + 75, 100);
        Debug.Log("🛌 Sleeping... target stamina: " + staminaRecoveryTarget);
    }

    public void ProcessRecoveryStep()
    {
        if ((isResting || isSleeping) && stamina < staminaRecoveryTarget)
        {
            stamina += 5;
            if (stamina >= staminaRecoveryTarget)
            {
                stamina = staminaRecoveryTarget;
                isResting = false;
                isSleeping = false;
                Debug.Log("✅ Recovery complete. Final stamina: " + stamina);
            }
        }
    }

    public void ApplyHotFoodTemperatureBoost(float boostAmount)
    {
        temperature += boostAmount;
        temperature = Mathf.Clamp(temperature, -50f, 50f);
        Debug.Log($"🌡️ Hot food consumed! Temp +{boostAmount}°C. Current temp: {temperature}°C");
    }

    public void InterruptRestOrSleep()
    {
        if (isResting || isSleeping)
        {
            isResting = false;
            isSleeping = false;
            Debug.Log("❗Rest/Sleep interrupted. Current stamina: " + stamina);
        }
    }

    public void ApplyClothingStatModifiers()
    {
        if (playerInventory == null) return;

        foreach (Item gear in playerInventory.GetEquippedGear())
        {
            if (gear?.data == null || gear.itemType != ItemType.Clothing)
                continue;

            if (gear.data.tags.Contains("insulated"))
            {
                temperature += 5;
                Debug.Log($"🧣 {gear.itemName} boosts temperature! +5°C");
            }

            if (gear.data.tags.Contains("lightweight"))
            {
                stamina += 10;
                Debug.Log($"🏃 {gear.itemName} boosts stamina! +10");
            }
        }

        ClampStats();
    }
}
