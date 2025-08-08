// File: Assets/Scripts/DevTools/FlagUsageAudit.cs

using UnityEngine;
using System.Linq;
using Flags;

namespace DevTools
{
    /// <summary>
    /// Audits which story flags are referenced in dialogue assets.
    /// Useful for finding unused or forgotten flags.
    /// </summary>
    public class FlagUsageAudit : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private TextAsset[] dialogueAssets;

        private void Start()
        {
            if (flagSystem == null)
            {
                Debug.LogWarning("[FlagUsageAudit] FlagSystem reference is missing.");
                return;
            }

            if (dialogueAssets == null || dialogueAssets.Length == 0)
            {
                Debug.LogWarning("[FlagUsageAudit] No dialogue assets assigned.");
                return;
            }

            var allKeys = flagSystem.GetAllKeys().ToList();

            foreach (var asset in dialogueAssets)
            {
                if (asset == null) continue;

                var text = asset.text;
                foreach (var key in allKeys)
                {
                    if (!text.Contains(key))
                        Debug.Log($"[Audit] Flag '{key}' not referenced in {asset.name}");
                }
            }

            Debug.Log("[FlagUsageAudit] Audit complete.");
        }
    }
}
