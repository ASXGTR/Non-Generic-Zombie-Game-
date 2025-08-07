using UnityEngine;

namespace Systems
{
    public enum DiseaseType
    {
        Flu,
        Cold,
        Infection
    }

    [System.Serializable]
    public class Item
    {
        public string name;
        public float healthRestore;
    }

    public class VitalsManager : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;

        public float CurrentHealth => currentHealth;
        public bool IsAlive => currentHealth > 0f;

        public void ApplyDamage(float amount)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0f);
            Debug.Log($"[VitalsManager] Damage applied: {amount}, Health now: {currentHealth}");
        }

        public void ApplyItemEffects(Item item)
        {
            currentHealth = Mathf.Min(currentHealth + item.healthRestore, maxHealth);
            Debug.Log($"[VitalsManager] Item used: {item.name}, Health restored to: {currentHealth}");
        }

        public void ApplyDiseaseEffect(DiseaseType type, float severity)
        {
            float damage = severity * 5f;
            ApplyDamage(damage);
            Debug.Log($"[VitalsManager] Disease effect applied: {type}, Severity: {severity}, Damage: {damage}");
        }

        public void RemoveDiseaseEffect(DiseaseType type)
        {
            Debug.Log($"[VitalsManager] Disease effect removed: {type}");
        }
    }
}
