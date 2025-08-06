using Core.Shared.Models;
// File: Assets/Scripts/Lore/LoreNode.cs
using UnityEngine;

namespace Game.Lore
{
    public class LoreNode : MonoBehaviour
    {
        [TextArea] public string loreText;
        public bool discovered;

        public void RevealLore()
        {
            if (!discovered)
            {
                discovered = true;
                Debug.Log($"[LoreNode] Discovered: {loreText}");
                // Extend: Display in LoreLog UI or write to journal file
            }
        }
    }
}
