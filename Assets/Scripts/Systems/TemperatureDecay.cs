using UnityEngine;

namespace Survival.Environment.Temperature
{
    public class TemperatureDecay : MonoBehaviour
    {
        [Header("Decay Settings")]
        [SerializeField] private float decayRate = 5f;
        [SerializeField] private float updateInterval = 1f;

        private Survival.Inventory.Food.FoodStateTracker tracker;

        private void Awake()
        {
            tracker = GetComponent<Survival.Inventory.Food.FoodStateTracker>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(DecayTemperature), updateInterval, updateInterval);
        }

        private void DecayTemperature()
        {
            if (tracker != null)
            {
                tracker.AdjustTemperature(-decayRate * updateInterval);
            }
        }
    }
}
