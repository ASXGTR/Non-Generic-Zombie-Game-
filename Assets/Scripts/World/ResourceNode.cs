// File: Assets/Scripts/World/ResourceNode.cs
using Game.Inventory;
using UnityEngine;

namespace Game.World
{
    public class ResourceNode : MonoBehaviour
    {
        [SerializeField] private InventoryItem resourceType;
        [SerializeField] private int yieldAmount = 1;

        public InventoryItem Harvest() => resourceType;
        public int Yield() => yieldAmount;
    }
}
