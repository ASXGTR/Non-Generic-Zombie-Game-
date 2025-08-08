using Core.Shared.Models;
using System.Collections.Generic;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        ItemInstance active = weaponManager.GetActiveWeapon();
        ItemInstance secondary = weaponManager.GetSecondaryWeapon();

        if (active != null && active.Data != null)
        {
            string modelName = GetTagValue(active.Data.Tags, "Model") ?? "Default";

            magazineAmmoUI.text = $"{active.GetMagazineCount()}";
            totalAmmoUI.text = $"{weaponManager.GetTotalAmmo(active)}";
            activeWeaponUI.sprite = GetWeaponSprite(modelName);
            ammoTypeUI.sprite = GetAmmoSprite(modelName);
        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";
            activeWeaponUI.sprite = emptySlot;
            ammoTypeUI.sprite = emptySlot;
        }

        if (secondary != null && secondary.Data != null)
        {
            string secondaryModel = GetTagValue(secondary.Data.Tags, "Model") ?? "Default";
            secondaryWeaponUI.sprite = GetWeaponSprite(secondaryModel);
        }
        else
        {
            secondaryWeaponUI.sprite = emptySlot;
        }
    }

    private string GetTagValue(List<string> tags, string key)
    {
        foreach (var tag in tags)
        {
            if (tag.StartsWith($"{key}:"))
                return tag.Substring(key.Length + 1);
        }
        return null;
    }

    private Sprite GetWeaponSprite(string modelName)
    {
        return Resources.Load<Sprite>($"{modelName}_Weapon") ?? emptySlot;
    }

    private Sprite GetAmmoSprite(string modelName)
    {
        return Resources.Load<Sprite>($"{modelName}_Ammo") ?? emptySlot;
    }
}
