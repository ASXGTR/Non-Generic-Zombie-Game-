using Core.Shared.Models;
// File: Assets/Scripts/World/SleepCycle.cs
using UnityEngine;

namespace Game.World
{
    public class SleepCycle : MonoBehaviour
    {
        public bool isResting;
        public float restTime;

        public void Sleep(float hours)
        {
            isResting = true;
            restTime += hours;
            Debug.Log($"[SleepCycle] Slept for {hours} hours.");
            // Extend: reset fatigue stat, pass time in SurvivalClock
        }

        public void WakeUp()
        {
            isResting = false;
            Debug.Log("[SleepCycle] Woke up.");
        }
    }
}
