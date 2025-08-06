using Core.Shared.Models;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Survival.UI.Tooltip
{
    public class TooltipDisplay : MonoBehaviour
    {
        public static TooltipDisplay Instance;

        [SerializeField] private Text tooltipText;
        [SerializeField] private Image tooltipIcon;
        [SerializeField] private CanvasGroup canvasGroup;

        private Coroutine showCoroutine;

        private void Awake() => Instance = this;

        public void Show(TooltipData data, float delay)
        {
            if (showCoroutine != null)
                StopCoroutine(showCoroutine);

            showCoroutine = StartCoroutine(ShowDelayed(data, delay));
        }

        public void Hide()
        {
            if (showCoroutine != null)
                StopCoroutine(showCoroutine);

            canvasGroup.alpha = 0;
        }

        private IEnumerator ShowDelayed(TooltipData data, float delay)
        {
            yield return new WaitForSeconds(delay);

            tooltipText.text = data.text;

            if (tooltipIcon != null && data.icon != null)
            {
                tooltipIcon.sprite = data.icon;
                tooltipIcon.enabled = true;
            }
            else if (tooltipIcon != null)
            {
                tooltipIcon.enabled = false;
            }

            canvasGroup.alpha = 1;
        }
    }
}
