using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour
{
    public TMP_Text storyText;
    public Button[] choiceButtons; // Ensure array has 4+ buttons assigned
    public PlayerStats playerStats;
    public HUDManager hudManager;

    [Header("Game Over UI")]
    public GameObject gameOverScreen;
    public Button respawnButton;
    public Button quitButton;

    private StoryNode currentNode;

    void Start()
    {
        if (choiceButtons.Length < 4)
            Debug.LogWarning("Assign 4+ choice buttons for full functionality.");

        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        if (playerStats != null)
        {
            playerStats.ResetStats();
            playerStats.gameManager = this;
        }

        if (GlobalGameStateHolder.Instance != null &&
            GlobalGameStateHolder.Instance.campExitResult != GlobalGameStateHolder.CampExitResult.None)
        {
            HandleCampReturn();
        }
        else
        {
            LoadNode("shoreline_wake_up");
        }

        if (hudManager != null)
            hudManager.UpdateHUD(playerStats);

        if (respawnButton != null)
            respawnButton.onClick.AddListener(ResetGame);
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitToTitle);
    }

    private string RemoveEmojis(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return Regex.Replace(input, @"[\uD800-\uDBFF][\uDC00-\uDFFF]", "");
    }

    public void LoadNode(string nodeId)
    {
        var node = NodeLoader.LoadNode(nodeId);

        if (node == null)
        {
            Debug.LogError($"Node '{nodeId}' could not be loaded.");
            return;
        }

        currentNode = node;

        if (storyText != null)
            storyText.text = RemoveEmojis(node.text);
        else
            Debug.LogError("StoryText not assigned.");

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].onClick.RemoveAllListeners();

            if (node.choices != null && i < node.choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = RemoveEmojis(node.choices[i].text);

                int index = i;
                choiceButtons[i].onClick.AddListener(() =>
                {
                    // Apply stat effects
                    playerStats.ApplyEffects(currentNode.choices[index].effects);

                    // Debug: Confirm HUDManager reference status
                    Debug.Log("📢 HUDManager is " + (hudManager != null ? "linked ✅" : "missing ❌"));

                    if (hudManager != null)
                    {
                        Debug.Log("📊 UpdateHUD called with PlayerStats:");
                        Debug.Log($"Health: {playerStats.health}, Stamina: {playerStats.stamina}, Hunger: {playerStats.hunger}, Hydration: {playerStats.hydration}, Temp: {playerStats.temperature}, Sickness: {playerStats.sickness}");
                        hudManager.UpdateHUD(playerStats);
                    }

                    if (playerStats.health <= 0)
                    {
                        TriggerGameOver();
                        return;
                    }

                    LoadNode(currentNode.choices[index].nextNode);
                });
            }
            else if (i == 3) // Always show camp option on 4th button
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = "Make Camp Here";

                choiceButtons[i].onClick.AddListener(() =>
                {
                    if (GlobalGameStateHolder.Instance != null)
                        GlobalGameStateHolder.Instance.lastNodeId = currentNode.id;

                    SceneManager.LoadScene("CampScene");
                });
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void HandleCampReturn()
    {
        var gameState = GlobalGameStateHolder.Instance;

        string returnNodeText = "";

        switch (gameState.campExitResult)
        {
            case GlobalGameStateHolder.CampExitResult.SleptWell:
                returnNodeText = "You slept peacefully and wake refreshed to a new day.";
                playerStats.stamina += 25;
                break;
            case GlobalGameStateHolder.CampExitResult.InterruptedSleep:
                returnNodeText = "Your sleep was interrupted. You wake groggy and alert to danger.";
                playerStats.stamina += 10;
                break;
            case GlobalGameStateHolder.CampExitResult.LeftCamp:
                returnNodeText = "You packed up your camp and are ready to continue.";
                break;
        }

        gameState.campExitResult = GlobalGameStateHolder.CampExitResult.None;

        currentNode = new StoryNode
        {
            id = "return_from_camp",
            text = returnNodeText,
            choices = new System.Collections.Generic.List<Choice>
            {
                new Choice { text = "Look around", nextNode = gameState.lastNodeId },
                new Choice { text = "Move on", nextNode = gameState.lastNodeId },
                new Choice { text = "Check supplies", nextNode = gameState.lastNodeId }
            }
        };

        if (storyText != null)
            storyText.text = RemoveEmojis(currentNode.text);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].onClick.RemoveAllListeners();

            if (i < currentNode.choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = RemoveEmojis(currentNode.choices[i].text);

                int index = i;
                choiceButtons[i].onClick.AddListener(() =>
                {
                    LoadNode(currentNode.choices[index].nextNode);
                });
            }
            else if (i == 3)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = "Make Camp Here";

                choiceButtons[i].onClick.AddListener(() =>
                {
                    gameState.lastNodeId = currentNode.id;
                    SceneManager.LoadScene("CampScene");
                });
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }

        if (hudManager != null)
            hudManager.UpdateHUD(playerStats);
    }

    public void TriggerGameOver()
    {
        Debug.Log("☠️ Player died. Triggering Game Over.");
        ShowGameOverScreen();
    }

    void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        foreach (Button btn in choiceButtons)
            btn.gameObject.SetActive(false);

        if (storyText != null)
            storyText.text = "You have died. Game Over.";
    }

    public void ResetGame()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        if (playerStats != null)
            playerStats.ResetStats();

        if (hudManager != null)
            hudManager.UpdateHUD(playerStats);

        LoadNode("shoreline_wake_up");
    }

    public void QuitToTitle()
    {
        Debug.Log("Quit to Title pressed. Implement scene loading here.");
        Application.Quit();
    }
}
