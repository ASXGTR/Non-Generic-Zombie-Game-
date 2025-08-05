// File: Assets/Scripts/Environment/WeatherSimulator.cs
using UnityEngine;

namespace Game.Environment
{
    public enum WeatherType { Clear, Rain, Fog, Storm }

    public class WeatherSimulator : MonoBehaviour
    {
        [SerializeField] private WeatherType currentWeather;

        public void SetWeather(WeatherType type)
        {
            currentWeather = type;
            Debug.Log($"[WeatherSimulator] Weather changed to: {type}");
            // Extend: modify ambient text or disease risk
        }

        public WeatherType GetCurrentWeather() => currentWeather;
    }
}
