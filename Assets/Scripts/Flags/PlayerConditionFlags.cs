namespace Flags
{
    /// <summary>
    /// Bitwise flags representing player conditions. Supports multi-status tracking.
    /// </summary>
    [System.Flags]
    public enum PlayerConditionFlags
    {
        None = 0,
        Injured = 1 << 0,
        Starving = 1 << 1,
        Overencumbered = 1 << 2,
        Exhausted = 1 << 3,
        Sick = 1 << 4,
        Dehydrated = 1 << 5,
        Cold = 1 << 6,
        Hot = 1 << 7,
        Poisoned = 1 << 8,

        // Mental states
        Calm = 1 << 9,
        Anxious = 1 << 10,
        Angry = 1 << 11,
        Focused = 1 << 12
    }
}
