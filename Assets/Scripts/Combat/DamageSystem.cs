// File: Assets/Scripts/Combat/DamageSystem.cs
using UnityEngine;

namespace Game.Combat
{
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField] private Player.PlayerStats stats;
        [SerializeField] private string statAffected = "Health";

        public void ApplyDamage(int amount)
        {
            stats.ModifyStat(statAffected, -amount);
            Debug.Log($"[DamageSystem] {amount} damage applied to {statAffected}");
        }

        public void Heal(int amount)
        {
            stats.ModifyStat(statAffected, +amount);
        }
    }
}
