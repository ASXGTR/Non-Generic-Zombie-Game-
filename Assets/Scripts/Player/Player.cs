// File: Assets/Scripts/Player/Player.cs
using Core.Shared.Models;

namespace Project.Player
{
    using Game.Inventory;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        [Header("UI References")]
        public UIController UIController;
        public GameObject DeadScreen;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UIController.ToggleInventory(); // Optional shortcut
            }
        }

        public void OnPlayerDeath()
        {
            DeadScreen.SetActive(true);
            UIController.InventoryPanel.SetActive(false); // Hide inventory on death
        }

        public void OnItemPickup(ItemInstance item)
        {
            InventoryManager.Instance.AddItem(item);
            UIController.ToggleInventory(); // Optional: auto-show inventory
        }
    }
}
