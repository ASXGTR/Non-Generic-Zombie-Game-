using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaHandler : MonoBehaviour
{
    public enum SimDevice { None, GalaxyS7, iPhoneX }
    public SimDevice simulationDevice = SimDevice.None;

    private RectTransform rectTransform;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0);

    private readonly Rect[] NSA_GalaxyS7 = new Rect[]
    {
        new Rect(0f, 0f, 1f, 1f), // Portrait — Galaxy S7 has no notch
        new Rect(0f, 0f, 1f, 1f)  // Landscape
    };

    private readonly Rect[] NSA_iPhoneX = new Rect[]
    {
        new Rect(0f, 102f / 2436f, 1f, 2202f / 2436f), // Portrait
        new Rect(132f / 2436f, 63f / 1125f, 2172f / 2436f, 1062f / 1125f) // Landscape
    };

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplySafeArea(GetSafeArea());
    }

    void Update()
    {
        Rect current = GetSafeArea();
        if (current != lastSafeArea)
            ApplySafeArea(current);
    }

    Rect GetSafeArea()
    {
        Rect safeArea = Screen.safeArea;

#if UNITY_EDITOR
        if (simulationDevice != SimDevice.None)
        {
            Rect[] NSA = simulationDevice switch
            {
                SimDevice.GalaxyS7 => NSA_GalaxyS7,
                SimDevice.iPhoneX => NSA_iPhoneX,
                _ => NSA_GalaxyS7
            };

            bool isPortrait = Screen.height > Screen.width;
            Rect simulated = NSA[isPortrait ? 0 : 1];

            safeArea = new Rect(
                Screen.width * simulated.x,
                Screen.height * simulated.y,
                Screen.width * simulated.width,
                Screen.height * simulated.height
            );
        }
#endif

        return safeArea;
    }

    void ApplySafeArea(Rect r)
    {
        lastSafeArea = r;

        Vector2 anchorMin = r.position;
        Vector2 anchorMax = r.position + r.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
