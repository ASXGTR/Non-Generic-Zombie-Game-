using Core.Shared.Models;
using UnityEngine;

namespace Survival.UI.Tooltip
{
    [System.Serializable]
    public class TooltipData
    {
        public string text;
        public Sprite icon;
        public Color backgroundColor = Color.black;
    }
}
