using UnityEngine;

namespace Environment.Spawn
{
    public static class CampObjectSpawner
    {
        public static void Spawn(GameObject[] prefabs, CampData data)
        {
            // TODO: Implement logic to spawn camp objects based on CampData
            Debug.Log($"[CampObjectSpawner] Spawning camp '{data.campName}' at {data.location}");
        }
    }

    [System.Serializable]
    public class CampData
    {
        public string campName;
        public Vector3 location;
        // Add other fields like rotation, size, owner, etc. as needed
    }
}
