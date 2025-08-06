using System.Collections.Generic;

namespace Flags
{
    public static class StoryFlags
    {
        private static readonly Dictionary<string, bool> flags = new();

        public static bool GetFlag(string key) => flags.ContainsKey(key) && flags[key];

        public static void SetFlag(string key, bool value) => flags[key] = value;

        public static void Clear() => flags.Clear();
    }
}
