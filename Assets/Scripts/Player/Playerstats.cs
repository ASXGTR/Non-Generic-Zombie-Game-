// File: Assets/Scripts/Player/PlayerStats.cs
using UnityEngine;

namespace Game.Player
{
    [System.Serializable]
    public class Stat
    {
        public string name;
        public int baseValue;
        public int modifier;
        public int Current => baseValue + modifier;
    }

    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private Stat[] stats;

        public int GetStat(string statName)
        {
            foreach (var s in stats)
                if (s.name == statName)
                    return s.Current;
            return 0;
        }

        public void ModifyStat(string statName, int delta)
        {
            foreach (var s in stats)
                if (s.name == statName)
                    s.modifier += delta;
        }
    }
}
