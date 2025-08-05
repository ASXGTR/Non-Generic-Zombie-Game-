using UnityEngine;

namespace DialogueSystem.EditorTools
{
    public static class ValidatorPassLogger
    {
        public static void LogSuccess(string context)
        {
            Debug.Log($"[✔] Validated: {context}");
        }

        public static void LogFailure(string context, string reason)
        {
            Debug.LogWarning($"[✘] Failed: {context} — {reason}");
        }
    }
}
