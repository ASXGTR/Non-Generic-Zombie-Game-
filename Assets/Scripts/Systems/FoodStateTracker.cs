using UnityEngine;

namespace Survival.Inventory.Food
{
    public class FoodStateTracker : MonoBehaviour
    {
        [Header("Temperature Settings")]
        [SerializeField] private float currentTemperature = 100f;
        [SerializeField] private float hotThreshold = 60f;

        public bool IsHot => currentTemperature >= hotThreshold;

        public void SetTemperature(float value)
        {
            currentTemperature = value;
        }

        public void AdjustTemperature(float delta)
        {
            currentTemperature += delta;
        }

        public void ResetTemperature()
        {
            currentTemperature = 100f;
        }
    }
}
