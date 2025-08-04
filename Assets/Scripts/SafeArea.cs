using UnityEngine;

[RequireComponent(typeof(RectTransform))]
/// <summary>
/// Ensures a UI element stays within the device's safe screen bounds (not obscured by notches, rounded corners, etc.).
/// </summary>
public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect lastSafeArea = Rect.zero;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    private void Update()
    {
        // Use approximate check to avoid floating point precision issues
        if (!RectEquals(lastSafeArea, Screen.safeArea))
        {
            ApplySafeArea();
            lastSafeArea = Screen.safeArea;
        }
    }

    /// <summary>
    /// Applies the safe area anchors to the attached RectTransform.
    /// </summary>
    [ContextMenu("Force Apply Safe Area")]
    private void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;

        if (safeArea.width <= 0 || safeArea.height <= 0)
        {
            Debug.LogWarning("[SafeArea] Screen safe area dimensions are invalid.");
            return;
        }

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }

    /// <summary>
    /// Checks if two rects are approximately equal (within a small threshold).
    /// </summary>
    private bool RectEquals(Rect a, Rect b)
    {
        return Mathf.Approximately(a.x, b.x) &&
               Mathf.Approximately(a.y, b.y) &&
               Mathf.Approximately(a.width, b.width) &&
               Mathf.Approximately(a.height, b.height);
    }
}
