using Core.Shared.Models;
﻿using Game.Inventory;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Game.UI
{
    /// <summary>
    /// Visual slot for displaying either InventoryItem or ItemData info in the UI.
    /// Robust against nulls, missing fields, and namespace collisions.
    /// </summary>
    public class ItemSlotUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image icon;
        [SerializeField] private Image background;

        [Header("Color Settings")]
        [SerializeField] private Color activeColor = Color.white;
        [SerializeField] private Color emptyColor = new Color(0.3f, 0.3f, 0.3f);

        private Sprite sourceIcon;

        /// <summary>
        /// Assigns data using InventoryItem component.
        /// </summary>
        public void Initialize(InventoryItem item)
        {
            if (item != null && item.icon != null)
            {
                sourceIcon = item.icon;
            }
            else
            {
                sourceIcon = null;
            }

            UpdateVisuals();
        }

        /// <summary>
        /// Assigns data using base Item (non-MonoBehaviour fallback).
        /// </summary>
        public void Initialize(Item item)
        {
            if (item != null && item.icon != null)
            {
                sourceIcon = item.icon;
            }
            else
            {
                sourceIcon = null;
            }

            UpdateVisuals();
        }

        /// <summary>
        /// Assigns data using raw Sprite if available.
        /// </summary>
        public void Initialize(Sprite itemSprite)
        {
            sourceIcon = itemSprite;
            UpdateVisuals();
        }

        /// <summary>
        /// Updates the visuals based on current icon state.
        /// </summary>
        private void UpdateVisuals()
        {
            if (icon == null || background == null)
                return;

            bool hasIcon = sourceIcon != null;
            icon.enabled = hasIcon;
            icon.sprite = hasIcon ? sourceIcon : null;
            background.color = hasIcon ? activeColor : emptyColor;
        }

        /// <summary>
        /// Clears the slot to its empty visual state.
        /// </summary>
        public void ClearSlot()
        {
            sourceIcon = null;
            UpdateVisuals();
        }

        /// <summary>
        /// Returns true if the slot currently displays an item.
        /// </summary>
        public bool HasItem() => sourceIcon != null;
    }
}
