using UnityEngine;

namespace Systems.World
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private ItemInstance[] lootPool;
        [SerializeField] private Transform spawnLocation;

        public void SpawnLoot()
        {
            if (lootPool.Length > 0)
            {
                var item = lootPool[Random.Range(0, lootPool.Length)];
                Debug.Log($"[LootSpawner] Spawned loot: {item.Item.name}");
                // Extend: Instantiate loot prefab, or log in inventory
            }
        }
    }

    // 🧩 Stubbed Dependencies

    [System.Serializable]
    public class ItemInstance
    {
        public Item Item;
    }

    [System.Serializable]
    public class Item
    {
        public string name;
        public float value;
        // Add other fields as needed
    }
}
