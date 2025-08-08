// File: Assets/Scripts/Flags/StoryFlags.cs

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Flags
{
    /// <summary>
    /// Global registry for story progression flags.
    /// Serialized as a ScriptableObject for scene and system access.
    /// </summary>
    [CreateAssetMenu(fileName = "StoryFlags", menuName = "Flags/StoryFlags")]
    public class StoryFlags : ScriptableObject
    {
        [Serializable]
        public struct FlagEntry
        {
            public string key;
            public bool value;
        }

        [SerializeField] private List<FlagEntry> serializedFlags = new();

        private Dictionary<string, bool> flags = new();

        private void OnEnable()
        {
            flags.Clear();
            foreach (var entry in serializedFlags)
                flags[entry.key] = entry.value;
        }

        /// <summary>Checks if a flag is set to true.</summary>
        public bool IsSet(string key) =>
            flags.TryGetValue(key, out var value) && value;

        /// <summary>Sets a flag to true.</summary>
        public void Set(string key)
        {
            Set(key, true);
        }

        /// <summary>Sets a flag to a specific boolean value.</summary>
        public void Set(string key, bool value)
        {
            flags[key] = value;
            UpdateSerializedEntry(key, value);
        }

        /// <summary>Clears all flags.</summary>
        public void Clear()
        {
            flags.Clear();
            serializedFlags.Clear();
        }

        /// <summary>Returns all currently tracked flag keys.</summary>
        public IEnumerable<string> GetAllKeys()
        {
            return flags.Keys;
        }

        // ──────────────────────────────────────
        // 🧭 Legacy Aliases
        // ──────────────────────────────────────

        public bool GetFlag(string key) => IsSet(key);
        public void SetFlag(string key) => Set(key);
        public void SetFlag(string key, bool value) => Set(key, value);

        // ──────────────────────────────────────
        // 🧠 Internal Sync
        // ──────────────────────────────────────

        private void UpdateSerializedEntry(string key, bool value)
        {
            for (int i = 0; i < serializedFlags.Count; i++)
            {
                if (serializedFlags[i].key == key)
                {
                    serializedFlags[i] = new FlagEntry { key = key, value = value };
                    return;
                }
            }

            serializedFlags.Add(new FlagEntry { key = key, value = value });
        }

        public override string ToString() =>
            $"StoryFlags ({flags.Count} flags)";
    }
}
