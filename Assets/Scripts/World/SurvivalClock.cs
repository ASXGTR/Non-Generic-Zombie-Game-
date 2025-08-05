// File: Assets/Scripts/World/SurvivalClock.cs
using UnityEngine;

namespace Game.World
{
    public class SurvivalClock : MonoBehaviour
    {
        public float timePassed; // in hours
        public float timeStep = 0.05f;

        private void Update()
        {
            timePassed += Time.deltaTime * timeStep;
        }

        public string GetTimeReport()
        {
            int hours = Mathf.FloorToInt(timePassed);
            return $"[SurvivalClock] Time elapsed: {hours}h";
        }
    }
}
