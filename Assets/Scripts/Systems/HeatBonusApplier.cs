using UnityEngine;

namespace Survival.Stats.Modifiers
{
    public class HeatBonusApplier : MonoBehaviour
    {
        [Header("Buff Settings")]
        [SerializeField] private float staminaBoost = 10f;
        [SerializeField] private float duration = 30f;

        public void ApplyBonus()
        {
            // TODO: Replace with actual stat system integration
            Debug.Log($"Applied hot food bonus: +{staminaBoost} stamina for {duration} seconds.");
        }
    }
}
