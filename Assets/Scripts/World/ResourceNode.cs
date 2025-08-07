// File: Assets/Scripts/World/ResourceNode.cs
using UnityEngine;
using Core.Shared.Models;

namespace Game.World
{
    public class ResourceNode : MonoBehaviour
    {
        [SerializeField] private ItemInstance resourceType;
        [SerializeField] private int yieldAmount = 1;

        public ItemInstance Harvest() => resourceType;
        public int Yield() => yieldAmount;
    }
}
