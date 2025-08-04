// PlayerStats.cs
using UnityEngine;
using System.Collections.Generic;
using DiseaseSystem;
using Game.Inventory; // ✅ Fully qualified types from InventorySystem

namespace Game.Stats
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Stats")]
        public int health;
        public float temperature;
        public int hydration;
        public int hunger;
        public int sickness;
        public int stamina;

        [Header("Inventory References")]
        public Game.Inventory.Inventory playerInventory;
        public Equipment playerEquipment;

        [HideInInspector] public GameManager gameManager;

        private bool isResting = false;
        private bool isSleeping = false;
        private int staminaRecoveryTarget = 0;

        private const string logTag = "[PlayerStats]";

        private void Start() => SetInitialStats();

        public void SetInitialStats()
        {
            health = 75;
            hydration = 75;
            hunger = 75;
            stamina = 75;
            sickness = 20;
            temperature = 20f;
        }

        public void ResetStats()
        {
            health = 100;
            hydration = 100;
            hunger = 100;
            stamina = 100;
            sickness = 0;
            temperature = 20f;
        }

        public void ApplyEffects(Effects effects)
        {
            if (effects == null)
            {
                Debug.LogWarning($"{logTag} Null Effects object passed.");
                return;
            }

            health += effects.healthChange;
            temperature += effects.temperatureChange;
            hydration += effects.hydrationChange;
            hunger += effects.hungerChange;
            sickness += effects.sicknessChange;
            stamina += effects.staminaChange;

            ClampStats();
            ApplyStatusPenalties();
        }

        private void ApplyStatusPenalties()
        {
            if (hydration <= 0) { health -= 5; Debug.Log($"{logTag} ⚠️ Dehydrated! Health -5."); }
            if (hunger <= 0) { health -= 5; Debug.Log($"{logTag} ⚠️ Starving! Health -5."); }
            if (temperature <= -10f || temperature >= 60f) { health -= 10; Debug.Log($"{logTag} ⚠️ Temp extremes! Health -10."); }
            if (sickness >= 75) { health -= 7; Debug.Log($"{logTag} ⚠️ Very sick! Health -7."); }

            health = Mathf.Clamp(health, 0, 100);
            if (health <= 0) OnPlayerDeath();
        }

        private void OnPlayerDeath()
        {
            if (gameManager != null)
                gameManager.TriggerGameOver();
        }

        private void ClampStats()
        {
            temperature = Mathf.Clamp(temperature, -30f, 100f);
            hydration = Mathf.Clamp(hydration, 0, 100);
            hunger = Mathf.Clamp(hunger, 0, 100);
            sickness = Mathf.Clamp(sickness, 0, 100);
            stamina = Mathf.Clamp(stamina, 0, 100);
        }

        public void StartRest()
        {
            isResting = true;
            isSleeping = false;
            staminaRecoveryTarget = Mathf.Min(stamina + 25, 100);
            Debug.Log($"{logTag} 🪑 Resting... Target stamina: {staminaRecoveryTarget}");
        }

        public void StartSleep()
        {
            isResting = false;
            isSleeping = true;
            staminaRecoveryTarget = Mathf.Min(stamina + 75, 100);
            Debug.Log($"{logTag} 🛌 Sleeping... Target stamina: {staminaRecoveryTarget}");
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
                    Debug.Log($"{logTag} ✅ Recovery complete. Final stamina: {stamina}");
                }
            }
        }

        public void InterruptRestOrSleep()
        {
            if (isResting || isSleeping)
            {
                isResting = false;
                isSleeping = false;
                Debug.Log($"{logTag} ❗ Rest/Sleep interrupted. Stamina: {stamina}");
            }
        }

        public void ApplyHotFoodTemperatureBoost(float boostAmount)
        {
            temperature += boostAmount;
            temperature = Mathf.Clamp(temperature, -50f, 50f);
            Debug.Log($"{logTag} 🌡️ Hot food! Temp +{boostAmount}°C ➜ Now: {temperature}°C");
        }

        public void ApplyClothingStatModifiers()
        {
            if (playerInventory == null) return;

            foreach (var gear in playerInventory.GetEquippedGear())
            {
                if (gear == null || gear.itemType != ItemType.Clothing) continue;

                if (gear.Tags != null && gear.Tags.Contains("insulated"))
                {
                    temperature += 5;
                    Debug.Log($"{logTag} 🧥 {gear.ItemName} adds insulation! Temp +5°C");
                }

                if (gear.Tags != null && gear.Tags.Contains("lightweight"))
                {
                    stamina += 10;
                    Debug.Log($"{logTag} 🏃 {gear.ItemName} adds agility! Stamina +10");
                }
            }

            ClampStats();
        }

        public void InitializeStartingGear(ItemDatabaseSO db)
        {
            if (db == null)
            {
                Debug.LogError($"{logTag} ❌ ItemDatabaseSO is null.");
                return;
            }

            if (playerInventory == null || playerEquipment == null)
            {
                Debug.LogError($"{logTag} ❌ Inventory or equipment not assigned.");
                return;
            }

            GameObject gearRoot = new("StartingGear");

            string[] defaultClothes = { "T-Shirt", "Jeans" };
            foreach (var itemName in defaultClothes)
            {
                var itemData = db.allItems.Find(i => i.ItemName == itemName);
                if (itemData != null)
                {
                    GameObject itemGO = new(itemName);
                    itemGO.transform.parent = gearRoot.transform;

                    var gear = itemGO.AddComponent<InventoryItem>();
                    gear.LoadFromData(itemData);

                    playerInventory.EquipItem(gear);
                    playerInventory.ownedItems.Add(gear);
                }
                else
                {
                    Debug.LogWarning($"{logTag} ⚠️ Missing gear: {itemName}");
                }
            }

            string[] miscItems = { "Glowstick", "Cloth Rag" };
            foreach (var itemName in miscItems)
            {
                var itemData = db.allItems.Find(i => i.ItemName == itemName);
                if (itemData != null)
                {
                    GameObject itemGO = new(itemName);
                    itemGO.transform.parent = gearRoot.transform;

                    var miscItem = itemGO.AddComponent<InventoryItem>();
                    miscItem.LoadFromData(itemData);

                    playerInventory.AddItem(miscItem);
                }
                else
                {
                    Debug.LogWarning($"{logTag} ⚠️ Missing item: {itemName}");
                }
            }

            List<string> fruits = new() { "Apple", "Pear", "Banana" };
            string fruitName = fruits[Random.Range(0, fruits.Count)];
            var fruitData = db.allItems.Find(i => i.ItemName == fruitName);

            if (fruitData != null)
            {
                GameObject itemGO = new(fruitName);
                itemGO.transform.parent = gearRoot.transform;

                var fruitItem = itemGO.AddComponent<InventoryItem>();
                fruitItem.LoadFromData(fruitData);

                playerInventory.AddItem(fruitItem);
            }
            else
            {
                Debug.LogWarning($"{logTag} ⚠️ Missing fruit: {fruitName}");
            }

            Debug.Log($"{logTag} 🎒 Gear initialized: clothes, tools, and fruit '{fruitName}'");
            ApplyClothingStatModifiers();
        }
    }
}
