using UnityEngine;
using Survival.Inventory.Food;
using Survival.Environment.Temperature;
using Survival.Stats.Modifiers;

namespace Survival.Inventory.Food.Controllers
{
    public class FoodTemperatureManager : MonoBehaviour
    {
        private FoodStateTracker foodState;
        private TemperatureDecay decay;
        private HeatBonusApplier bonus;

        [Header("Debug Settings")]
        [SerializeField] private bool applyBonusOnStart;
        [SerializeField] private bool logStateOnStart;

        private void Awake()
        {
            foodState = GetComponent<FoodStateTracker>();
            decay = GetComponent<TemperatureDecay>();
            bonus = GetComponent<HeatBonusApplier>();
        }

        private void Start()
        {
            if (logStateOnStart)
            {
                Debug.Log($"[FoodTemperatureManager] IsHot: {foodState?.IsHot}");
            }

            if (applyBonusOnStart && foodState?.IsHot == true)
            {
                bonus?.ApplyBonus();
            }
        }

        public void Consume()
        {
            if (foodState != null && foodState.IsHot)
            {
                Debug.Log("[FoodTemperatureManager] Consumed hot food — applying bonus.");
                bonus?.ApplyBonus();
            }
            else
            {
                Debug.Log("[FoodTemperatureManager] Consumed cold food — no bonus applied.");
            }
        }
    }
}
