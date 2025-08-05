// File: Assets/Scripts/UI/TypewriterDisplay.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game.UI
{
    public class TypewriterDisplay : MonoBehaviour
    {
        [SerializeField] private Text uiText;
        [SerializeField] private float typingSpeed = 0.05f;

        public void DisplayText(string fullText)
        {
            StartCoroutine(TypeText(fullText));
        }

        private IEnumerator TypeText(string text)
        {
            uiText.text = "";
            foreach (char c in text)
            {
                uiText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
}
