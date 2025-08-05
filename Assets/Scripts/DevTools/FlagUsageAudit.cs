// File: Assets/Scripts/DevTools/FlagUsageAudit.cs
using UnityEngine;
using System.Linq;

namespace Game.DialogueSystem
{
    public class FlagUsageAudit : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private TextAsset[] dialogueAssets;

        private void Start()
        {
            foreach (var asset in dialogueAssets)
            {
                var text = asset.text;
                foreach (var key in flagSystem.flags.Keys)
                {
                    if (!text.Contains(key))
                        Debug.Log($"[Audit] Flag '{key}' not referenced in {asset.name}");
                }
            }
        }
    }
}
