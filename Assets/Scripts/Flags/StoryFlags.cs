// File: Assets/Scripts/Flags/StoryFlags.cs
using UnityEngine;
using System.Collections.Generic;

namespace Game.DialogueSystem
{
    [CreateAssetMenu(fileName = "StoryFlags", menuName = "Game/StoryFlags")]
    public class StoryFlags : ScriptableObject
    {
        public Dictionary<string, bool> flags = new();

        public bool GetFlag(string key) => flags.ContainsKey(key) && flags[key];

        public void SetFlag(string key, bool value) => flags[key] = value;
    }
}
