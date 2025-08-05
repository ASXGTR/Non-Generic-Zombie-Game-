// File: Assets/Scripts/Narrative/EmotionPrompt.cs
using UnityEngine;

namespace Game.Narrative
{
    public class EmotionPrompt : MonoBehaviour
    {
        [TextArea] public string emotionalText;

        public void TriggerEmotion()
        {
            Debug.Log($"[EmotionPrompt] Emotion triggered: {emotionalText}");
            // Optional: trigger ambient music or visual filter
        }
    }
}
