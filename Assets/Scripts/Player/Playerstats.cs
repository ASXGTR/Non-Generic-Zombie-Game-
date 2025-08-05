using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Core Stats")]
    public int health;
    public int stamina;
    public int strength;

    private EquipmentManager equipmentManager;

    void Awake()
    {
#if UNITY_2023_1_OR_NEWER
        equipmentManager = Object.FindFirstObjectByType<EquipmentManager>();
#else
        equipmentManager = FindObjectOfType<EquipmentManager>();
#endif
    }

    public InventoryItem GetEquippedItem(string slotName)
    {
        if (equipmentManager == null || string.IsNullOrEmpty(slotName))
            return null;

        return equipmentManager.GetEquippedItem(slotName);
    }

    public bool HasEquippedWeapon()
    {
        InventoryItem weapon = GetEquippedItem("Weapon");
        return weapon != null;
    }

    // Other stat-related methods...
}
