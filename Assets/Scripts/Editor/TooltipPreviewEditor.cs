using Survival.UI.Tooltip;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(TooltipTrigger))]
public class TooltipPreviewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TooltipTrigger trigger = (TooltipTrigger)target;
        if (trigger.tooltipData != null)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("🔍 Tooltip Preview", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(trigger.tooltipData.text, MessageType.Info);

            if (trigger.tooltipData.icon != null)
            {
                GUILayout.Label(trigger.tooltipData.icon.texture, GUILayout.Width(64), GUILayout.Height(64));
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No TooltipData assigned.", MessageType.Warning);
        }
    }
}
