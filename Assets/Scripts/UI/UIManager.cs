using Core.Shared.Models;
// File: Assets/Scripts/UI/UIManager.cs
using UnityEngine;

namespace Game.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject statusOverlay;

        public void TogglePanel(string panelName, bool active)
        {
            switch (panelName)
            {
                case "Dialogue": dialoguePanel.SetActive(active); break;
                case "Inventory": inventoryPanel.SetActive(active); break;
                case "Status": statusOverlay.SetActive(active); break;
            }
        }
    }
}
