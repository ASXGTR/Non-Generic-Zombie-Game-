using System.Collections.Generic;

/// <summary>
/// Represents a single node in the story.
/// </summary>
[System.Serializable]
public class StoryNode
{
    public string id;
    public string text;
    public List<Choice> choices;
    public StartStats startStats; // Optional, used in the first node to set up initial game state
}

/// <summary>
/// Represents a player choice from a StoryNode.
/// </summary>
[System.Serializable]
public class Choice
{
    public string text;           // The visible text of the choice
    public string nextNode;       // ID of the next node to load if chosen
    public Effects effects;       // Stat or gameplay effects from this choice
}

/// <summary>
/// Stat changes applied when a choice is made.
/// </summary>
[System.Serializable]
public class Effects
{
    public int healthChange = 0;
    public int temperatureChange = 0;
    public int hydrationChange = 0;
    public int hungerChange = 0;
    public int sicknessChange = 0;
    public int staminaChange = 0;
}

/// <summary>
/// Initial player stats and inventory configuration for the very first story node.
/// </summary>
[System.Serializable]
public class StartStats
{
    public int health = 100;
    public int temperature = 20;
    public int hydration = 100;
    public int hunger = 100;
    public int sickness = 0;

    public List<string> inventory = new List<string>();  // Item names
    public List<string> clothing = new List<string>();   // Worn item names
}
