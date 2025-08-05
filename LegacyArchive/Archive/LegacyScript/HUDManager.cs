
// 🔒 Deprecated HUDManager.cs
// Logic modularized into WeaponHUDDisplay.cs and supporting systems on 2025-08-05
// Retained for reference only. Do not use in runtime.


using Game.Inventory;
using Game.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Stat Icons & Labels")]
    public Image hydrationIcon;
    public Image hungerIcon;
    public Image temperatureIcon;
    public Image sicknessIcon;

    [Header("Optional Symbol Sprites")]
    public Sprite hydrationSprite;
    public Sprite hungerSprite;
    public Sprite temperatureSprite;
    public Sprite sicknessSprite;

    [Header("Stat Values")]
    public TMP_Text hydrationValue;
    public TMP_Text hungerValue;
    public TMP_Text temperatureValue;
    public TMP_Text sicknessValue;

    [Header("Time & Compass Overlays")]
    public Image watchIcon;
    public TMP_Text timeDisplay;

    public Image compassIcon;
    public TMP_Text compassDirectionText;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        InitializeIcons();
        HideOptionalOverlays();
    }

    void InitializeIcons()
    {
        if (hydrationIcon != null && hydrationSprite != null)
            hydrationIcon.sprite = hydrationSprite;

        if (hungerIcon != null && hungerSprite != null)
            hungerIcon.sprite = hungerSprite;

        if (temperatureIcon != null && temperatureSprite != null)
            temperatureIcon.sprite = temperatureSprite;

        if (sicknessIcon != null && sicknessSprite != null)
            sicknessIcon.sprite = sicknessSprite;
    }

    void HideOptionalOverlays()
    {
        if (watchIcon != null) watchIcon.enabled = false;
        if (compassIcon != null) compassIcon.enabled = false;
        if (sicknessIcon != null) sicknessIcon.enabled = false;
    }

    public void UpdateHUD(PlayerStats stats)
    {
        if (stats == null || inventoryManager == null)
        {
            Debug.LogWarning("[HUDManager] Cannot update — missing stats or inventoryManager.");
            return;
        }

        // 🧪 Sickness Logic
        bool isDiseased = stats.sickness > 0f;
        if (sicknessIcon != null)
            sicknessIcon.enabled = isDiseased;
        if (sicknessValue != null)
            sicknessValue.text = $"{stats.sickness:0}";

        // 🕒 Time Logic via Wrist Slot
        InventoryItem watchItem = inventoryManager.allItems.Find(item =>
            item.isEquipped && item.clothingSlot == ClothingSlot.Wrist && item.tracksTime);

        if (watchItem != null)
        {
            if (watchIcon != null) watchIcon.enabled = true;
            if (timeDisplay != null)
                timeDisplay.text = FormatTime(watchItem.hoursSinceAcquired);
        }
        else
        {
            if (watchIcon != null) watchIcon.enabled = false;
            if (timeDisplay != null) timeDisplay.text = "";
        }

        // 🧭 Compass Logic via Utility Slot
        InventoryItem compassItem = inventoryManager.allItems.Find(item =>
            item.isEquipped && item.clothingSlot == ClothingSlot.Utility && item.hasCompassFeature);

        if (compassItem != null)
        {
            if (compassIcon != null) compassIcon.enabled = true;
            if (compassDirectionText != null)
                compassDirectionText.text = compassItem.taggedDirection;
        }
        else
        {
            if (compassIcon != null) compassIcon.enabled = false;
            if (compassDirectionText != null) compassDirectionText.text = "";
        }

        // 🌡️ Temperature
        if (temperatureValue != null)
            temperatureValue.text = $"{stats.temperature:0}°";

        // 🥩 Hunger
        if (hungerValue != null)
            hungerValue.text = $"{stats.hunger:0}";

        // 💧 Hydration
        if (hydrationValue != null)
            hydrationValue.text = $"{stats.hydration:0}";
    }

    private string FormatTime(float hours)
    {
        int totalMinutes = Mathf.RoundToInt(hours * 60);
        int h = totalMinutes / 60;
        int m = totalMinutes % 60;
        return $"{h:D2}:{m:D2}";
    }
}
