using Core.Shared;
using Core.Shared.Models;
using UnityEngine;

namespace Systems
{
    [AddComponentMenu("Systems/Weapon Manager")]
    public class WeaponManager : MonoBehaviour
    {
        [Header("Equipped Weapons")]
        [SerializeField] private ItemInstance activeWeapon;
        [SerializeField] private ItemInstance secondaryWeapon;

        [Header("Ammo Storage")]
        [SerializeField] private int totalAmmo = 120;

        public delegate void WeaponEvent();
        public event WeaponEvent OnWeaponChanged;

        public ItemInstance GetActiveWeapon() => activeWeapon;
        public ItemInstance GetSecondaryWeapon() => secondaryWeapon;

        public int GetTotalAmmo(ItemInstance weapon)
        {
            if (weapon == null || weapon.Data == null || !weapon.Data.HasTag("Weapon")) return 0;
            return totalAmmo;
        }

        public void EquipWeapon(ItemInstance newWeapon, bool asPrimary = true)
        {
            if (newWeapon == null || newWeapon.Data == null || !newWeapon.Data.HasTag("Weapon")) return;

            if (asPrimary)
                activeWeapon = newWeapon;
            else
                secondaryWeapon = newWeapon;

            Debug.Log($"[WeaponManager] 🎯 Equipped {(asPrimary ? "Primary" : "Secondary")}: {newWeapon.Data.ItemName}");

            OnWeaponChanged?.Invoke();
        }

        public void SwapWeapons()
        {
            (activeWeapon, secondaryWeapon) = (secondaryWeapon, activeWeapon);
            Debug.Log("[WeaponManager] 🔄 Swapped weapons.");
            OnWeaponChanged?.Invoke();
        }

        public void ConsumeAmmo(int amount)
        {
            totalAmmo = Mathf.Max(0, totalAmmo - amount);
            Debug.Log($"[WeaponManager] 🔫 Ammo used: {amount} → Remaining: {totalAmmo}");
        }

        public void AddAmmo(int amount)
        {
            totalAmmo += amount;
            Debug.Log($"[WeaponManager] ➕ Ammo added: {amount} → Total: {totalAmmo}");
        }
    }
}
