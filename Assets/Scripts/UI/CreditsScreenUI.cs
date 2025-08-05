using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Survival.UI.TitleScreen
{
    public class CreditsScreenUI : MonoBehaviour
    {
        [Header("UI Containers")]
        [SerializeField] private GameObject creditsPanel;
        [SerializeField] private ScrollRect creditsScroll;

        [Header("Content Elements")]
        [SerializeField] private TMP_Text projectNameText;
        [SerializeField] private TMP_Text contributorsText;
        [SerializeField] private TMP_Text toolsUsedText;
        [SerializeField] private TMP_Text thanksText;

        [Header("Buttons")]
        [SerializeField] private Button backButton;

        private void Awake()
        {
            backButton.onClick.AddListener(HideCredits);
            PopulateCredits();
        }

        public void ShowCredits()
        {
            creditsPanel.SetActive(true);
            creditsScroll.verticalNormalizedPosition = 1f; // Start at top
        }

        private void HideCredits()
        {
            creditsPanel.SetActive(false);
        }

        private void PopulateCredits()
        {
            projectNameText.text = "Wasteland Threads: A Survival Narrative";

            contributorsText.text =
                "Developer: John (Solo Systems Architect)\n" +
                "AI Companion: Microsoft Copilot\n" +
                "Special Thanks: The Unity Dev Community\n";

            toolsUsedText.text =
                "Engine: Unity\n" +
                "Editor Tooling: Custom Validators & Migrators\n" +
                "DevOps: Gitleaks, Shell Scripts\n";

            thanksText.text =
                "Thanks for playing! This project was built with care, curiosity, and a love for modular systems.\n\n" +
                "Want to contribute or follow progress?\nCheck the README or community links in the game folder.";
        }
    }
}
