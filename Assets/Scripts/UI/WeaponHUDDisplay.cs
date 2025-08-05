using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponHUDDisplay : MonoBehaviour
{
    [Header("Ammo")]
    public TMP_Text magazineAmmoUI;
    public TMP_Text totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapons")]
    public Image activeWeaponUI;
    public Image secondaryWeaponUI;
    public Sprite emptySlot;

    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = Object.FindFirstObjectByType<WeaponManager>();
    }

    void Update()
    {
        if (weaponManager == null) return;

        Weapon active = weaponManager.GetActiveWeapon();
        Weapon secondary = weaponManager.GetSecondaryWeapon();

        if (active != null)
        {
            magazineAmmoUI.text = $"{active.GetMagazineCount()}";
            totalAmmoUI.text = $"{weaponManager.GetTotalAmmo(active)}";
            activeWeaponUI.sprite = GetWeaponSprite(active.Model);
            ammoTypeUI.sprite = GetAmmoSprite(active.Model);
        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";
            activeWeaponUI.sprite = emptySlot;
            ammoTypeUI.sprite = emptySlot;
        }

        secondaryWeaponUI.sprite = secondary != null
            ? GetWeaponSprite(secondary.Model)
            : emptySlot;
    }

    private Sprite GetWeaponSprite(WeaponModel model)
    {
        return Resources.Load<Sprite>($"{model}_Weapon") ?? emptySlot;
    }

    private Sprite GetAmmoSprite(WeaponModel model)
    {
        return Resources.Load<Sprite>($"{model}_Ammo") ?? emptySlot;
    }
}
