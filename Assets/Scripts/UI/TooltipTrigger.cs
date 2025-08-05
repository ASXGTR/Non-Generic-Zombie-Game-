using UnityEngine;
using UnityEngine.EventSystems;

namespace Survival.UI.Tooltip
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TooltipData tooltipData;
        public float delay = 0.5f;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipDisplay.Instance.Show(tooltipData, delay);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipDisplay.Instance.Hide();
        }
    }
}
