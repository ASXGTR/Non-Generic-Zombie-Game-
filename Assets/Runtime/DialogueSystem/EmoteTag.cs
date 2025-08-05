using UnityEngine;
using System.Text.RegularExpressions;

namespace DialogueSystem
{
    public static class EmoteTag
    {
        public static string StripEmotes(string input)
        {
            return Regex.Replace(input, @"

\[(.*?)\]

", "");
        }

        public static string[] ExtractEmotes(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"

\[(.*?)\]

");
            string[] emotes = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                emotes[i] = matches[i].Groups[1].Value;
            }
            return emotes;
        }
    }
}
