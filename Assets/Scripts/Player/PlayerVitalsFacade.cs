using UnityEngine;
using Systems;

public class PlayerVitalsFacade : MonoBehaviour
{
    public VitalsManager vitalsManager;
    public StaminaSystem staminaSystem;
    public SurvivalStats survivalStats;

    public float CurrentHealth => vitalsManager?.CurrentHealth ?? 0f;
    public bool IsAlive => vitalsManager?.IsAlive ?? false;

    public float CurrentStamina => staminaSystem?.CurrentStamina ?? 0f;
    public bool CanSprint => staminaSystem?.CanSprint ?? false;

    public float Hunger => survivalStats?.Hunger ?? 0f;
    public float Thirst => survivalStats?.Thirst ?? 0f;
    public float Fatigue => survivalStats?.Fatigue ?? 0f;

    public void ApplyDamage(float amount) => vitalsManager?.ApplyDamage(amount);

    public void Consume(Item item)
    {
        vitalsManager?.ApplyItemEffects(item);
        survivalStats?.ApplyItemEffects(item);
        staminaSystem?.Consume(item);
    }
}
