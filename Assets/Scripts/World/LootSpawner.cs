using Core.Shared.Models;
// File: Assets/Scripts/World/LootSpawner.cs
using UnityEngine;

namespace Game.World
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private Inventory.InventoryItem[] lootPool;
        [SerializeField] private Transform spawnLocation;

        public void SpawnLoot()
        {
            if (lootPool.Length > 0)
            {
                var item = lootPool[Random.Range(0, lootPool.Length)];
                Debug.Log($"[LootSpawner] Spawned loot: {item.itemName}");
                // Extend: Instantiate loot prefab, or log in inventory
            }
        }
    }
}
