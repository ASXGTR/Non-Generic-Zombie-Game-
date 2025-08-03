using UnityEngine;
using TMPro;

[ExecuteAlways]
public class HUDManager : MonoBehaviour
{
    [Header("HUD Text References")]
    public TMP_Text HealthText;
    public TMP_Text HydrationText;
    public TMP_Text HungerText;
    public TMP_Text TemperatureText;
    public TMP_Text SicknessText;
    public TMP_Text StaminaText;

    [Header("HUD Visibility Control")]
    public CanvasGroup hudCanvasGroup; // ✅ Optional: assign this in Inspector if you want fading
    public bool showDebugLogs = false; // ✅ Toggle logging from Inspector

    void Start()
    {
        if (Application.isPlaying && hudCanvasGroup != null)
        {
            ShowHUDInstant(); // Ensures HUD is visible on start
        }
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            UpdatePreviewValues();
        }
        else if (showDebugLogs)
        {
            DebugHUDStatus();
        }
    }

    private void UpdatePreviewValues()
    {
        if (HealthText != null) HealthText.text = "<color=#00FF00>Health: 100</color>";
        if (HydrationText != null) HydrationText.text = "<color=#FFFFFF>Hydration: 100</color>";
        if (HungerText != null) HungerText.text = "<color=#FFFFFF>Hunger: 100</color>";
        if (TemperatureText != null) TemperatureText.text = "<color=#FF4500>Temperature: 100°</color>";
        if (SicknessText != null) SicknessText.text = "<color=#00FF00>Sickness: 0</color>";
        if (StaminaText != null) StaminaText.text = "<color=#1E90FF>Stamina: 100</color>";
    }

    public void UpdateHUD(PlayerStats stats)
    {
        if (stats == null)
        {
            Debug.LogWarning("⚠️ HUDManager: PlayerStats reference is null.");
            return;
        }

        string healthColor = stats.health < 30 ? "#FF0000" : (stats.health < 60 ? "#FFFF00" : "#00FF00");
        string hydrationColor = stats.hydration < 20 ? "#FF0000" : (stats.hydration < 40 ? "#FFFF00" : "#FFFFFF");
        string hungerColor = stats.hunger < 20 ? "#FF0000" : (stats.hunger < 40 ? "#FFFF00" : "#FFFFFF");
        string sicknessColor = stats.sickness > 70 ? "#FF0000" : (stats.sickness > 40 ? "#FFFF00" : "#00FF00");
        string staminaColor = stats.stamina < 30 ? "#FF0000" : (stats.stamina < 60 ? "#FFFF00" : "#1E90FF");
        string tempColor = stats.temperature <= 0 ? "#00BFFF" :
                           stats.temperature >= 40 ? "#FF4500" : "#00FF00";

        if (HealthText != null) HealthText.text = $"<color={healthColor}>Health: {stats.health}</color>";
        if (HydrationText != null) HydrationText.text = $"<color={hydrationColor}>Hydration: {stats.hydration}</color>";
        if (HungerText != null) HungerText.text = $"<color={hungerColor}>Hunger: {stats.hunger}</color>";
        if (TemperatureText != null) TemperatureText.text = $"<color={tempColor}>Temperature: {stats.temperature}°C</color>";
        if (SicknessText != null) SicknessText.text = $"<color={sicknessColor}>Sickness: {stats.sickness}</color>";
        if (StaminaText != null) StaminaText.text = $"<color={staminaColor}>Stamina: {stats.stamina}</color>";
    }

    public void ShowHUDInstant()
    {
        gameObject.SetActive(true);
        if (hudCanvasGroup != null)
        {
            hudCanvasGroup.alpha = 1f;
            hudCanvasGroup.interactable = true;
            hudCanvasGroup.blocksRaycasts = true;
        }
    }

    public void HideHUDInstant()
    {
        if (hudCanvasGroup != null)
        {
            hudCanvasGroup.alpha = 0f;
            hudCanvasGroup.interactable = false;
            hudCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void DebugHUDStatus()
    {
        Debug.Log("🩺 HUDManager: GameObject active? " + gameObject.activeSelf);

        if (transform.parent != null)
        {
            Debug.Log("🩺 HUDManager: Parent active? " + transform.parent.gameObject.activeSelf);
        }
        else
        {
            Debug.Log("🩺 HUDManager: No parent object.");
        }

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            Debug.Log("🩺 HUDManager: Canvas render mode = " + canvas.renderMode);
            Debug.Log("🩺 HUDManager: Canvas sorting order = " + canvas.sortingOrder);
        }
        else
        {
            Debug.Log("🩺 HUDManager: No canvas found in parent.");
        }
    }
}
