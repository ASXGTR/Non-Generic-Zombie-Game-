using Core.Shared.Models;
// File: Assets/Scripts/World/SpawnPointManager.cs
using UnityEngine;

namespace Game.World
{
    public class SpawnPointManager : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private GameObject playerPrefab;

        public void SpawnPlayer(int index)
        {
            if (index < spawnPoints.Length)
            {
                Instantiate(playerPrefab, spawnPoints[index].position, Quaternion.identity);
                Debug.Log($"[SpawnPointManager] Spawned player at index {index}");
            }
        }
    }
}
