// File: Assets/Scripts/Scene/PrefabStoryBinder.cs
using UnityEngine;

namespace Game.DialogueSystem
{
    public class PrefabStoryBinder : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string[] flagsToCheck;
        [SerializeField] private GameObject[] activateIfTrue;

        private void Start()
        {
            for (int i = 0; i < flagsToCheck.Length; i++)
            {
                bool active = flagSystem.GetFlag(flagsToCheck[i]);
                activateIfTrue[i].SetActive(active);
            }
        }
    }
}
