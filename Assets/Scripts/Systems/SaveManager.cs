// File: Assets/Scripts/Systems/SaveManager.cs
using UnityEngine;
using System.IO;

namespace Game.Systems
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private Player.PlayerStats playerStats;

        private string SavePath => $"{Application.persistentDataPath}/save.json";

        public void Save()
        {
            var json = JsonUtility.ToJson(playerStats);
            File.WriteAllText(SavePath, json);
            Debug.Log("[SaveManager] Game saved.");
        }

        public void Load()
        {
            if (File.Exists(SavePath))
            {
                var json = File.ReadAllText(SavePath);
                JsonUtility.FromJsonOverwrite(json, playerStats);
                Debug.Log("[SaveManager] Game loaded.");
            }
        }
    }
}
