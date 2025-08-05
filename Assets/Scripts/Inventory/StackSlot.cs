using UnityEngine;

namespace Game.Inventory
{
    /// <summary>
    /// Serializable stack slot for item and quantity tracking.
    /// Used in global inventory systems.
    /// </summary>
    [System.Serializable]
    public class StackSlot
    {
        public InventoryItem item;
        public int quantity;

        public bool IsEmpty => item == null || quantity <= 0;

        public void Add(int amount) => quantity += amount;
    }
}
