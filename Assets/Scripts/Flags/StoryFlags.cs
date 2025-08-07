// File: Assets/Scripts/Flags/StoryFlags.cs

using System.Collections.Generic;

namespace Flags
{
    /// <summary>
    /// Global static registry for story progression flags.
    /// Used to track narrative state across scenes and systems.
    /// </summary>
    public static class StoryFlags
    {
        private static readonly Dictionary<string, bool> flags = new();

        /// <summary>
        /// Checks if a flag is set to true.
        /// </summary>
        public static bool IsSet(string key) =>
            flags.ContainsKey(key) && flags[key];

        /// <summary>
        /// Sets a flag to true.
        /// </summary>
        public static void Set(string key) =>
            flags[key] = true;

        /// <summary>
        /// Sets a flag to a specific boolean value.
        /// </summary>
        public static void Set(string key, bool value) =>
            flags[key] = value;

        /// <summary>
        /// Clears all flags.
        /// </summary>
        public static void Clear() =>
            flags.Clear();

        // --- Aliases for legacy calls ---

        /// <summary>
        /// Alias for IsSet. Preserves compatibility with GetFlag calls.
        /// </summary>
        public static bool GetFlag(string key) => IsSet(key);

        /// <summary>
        /// Alias for Set. Preserves compatibility with SetFlag calls.
        /// </summary>
        public static void SetFlag(string key) => Set(key);

        /// <summary>
        /// Alias for Set with value. Preserves compatibility with SetFlag(key, value).
        /// </summary>
        public static void SetFlag(string key, bool value) => Set(key, value);
    }
}
