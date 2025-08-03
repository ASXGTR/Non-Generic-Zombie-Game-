using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.Inventory; // Access to Item, SlotType, etc.
using Game.UI;        // ✅ Added for InventoryUIManager

public class CampSceneManager : MonoBehaviour
{
    [Header("Camp Buttons")]
    public Button sleepButton;
    public Button leaveCampButton;
    public Button inventoryButton;

    [Header("Chance Settings")]
    [Range(0, 100)]
    public int interruptChancePercent = 20;

    [Header("UI References")]
    public InventoryUIManager inventoryUIManager;

    void Start()
    {
        if (sleepButton != null)
            sleepButton.onClick.AddListener(OnSleep);

        if (leaveCampButton != null)
            leaveCampButton.onClick.AddListener(OnLeaveCamp);

        if (inventoryButton != null)
            inventoryButton.onClick.AddListener(OnOpenInventory);
    }

    void OnSleep()
    {
        if (GlobalGameStateHolder.Instance != null)
        {
            int roll = Random.Range(1, 101);

            if (roll <= interruptChancePercent)
            {
                GlobalGameStateHolder.Instance.campExitResult = GlobalGameStateHolder.CampExitResult.InterruptedSleep;
                Debug.Log("😨 Sleep was interrupted! Roll: " + roll);
            }
            else
            {
                GlobalGameStateHolder.Instance.campExitResult = GlobalGameStateHolder.CampExitResult.SleptWell;
                Debug.Log("😴 Slept peacefully. Roll: " + roll);
            }

            SceneManager.LoadScene("MainGame");
        }
    }

    void OnLeaveCamp()
    {
        if (GlobalGameStateHolder.Instance != null)
        {
            GlobalGameStateHolder.Instance.campExitResult = GlobalGameStateHolder.CampExitResult.LeftCamp;
            Debug.Log("🏕️ Left camp without sleeping.");
            SceneManager.LoadScene("MainGame");
        }
    }

    void OnOpenInventory()
    {
        if (inventoryUIManager != null)
        {
            inventoryUIManager.OpenInventory(); // You can define this later inside InventoryUIManager
        }
        else
        {
            Debug.LogWarning("⚠️ InventoryUIManager not assigned in CampSceneManager.");
        }
    }
}
