using System.Collections.Generic;

/// <summary>
/// Represents a single node in the story.
/// </summary>
[System.Serializable]
public class StoryNode
{
    public string id;
    public string text;
    public List<Choice> choices = new();
    public StartStats startStats;

    public StoryNode() => choices = new List<Choice>();
}

/// <summary>
/// Represents a player choice from a StoryNode.
/// </summary>
[System.Serializable]
public class Choice
{
    public string text;
    public string nextNode;
    public Effects effects = new();

    public override string ToString() => $"Choice: '{text}' ➜ {nextNode}";
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

    public bool HasAnyEffect()
    {
        return healthChange != 0 || temperatureChange != 0 ||
               hydrationChange != 0 || hungerChange != 0 ||
               sicknessChange != 0 || staminaChange != 0;
    }

    public override string ToString()
    {
        return $"Effects [HP:{healthChange}, Temp:{temperatureChange}, Hyd:{hydrationChange}, Hung:{hungerChange}, Sick:{sicknessChange}, Stam:{staminaChange}]";
    }
}

/// <summary>
/// Initial player stats and inventory configuration for the first story node.
/// </summary>
[System.Serializable]
public class StartStats
{
    public bool useCustomStats = false;
    public int health = 100;
    public int temperature = 20;
    public int hydration = 100;
    public int hunger = 100;
    public int sickness = 0;
    public int stamina = 100;

    public List<string> inventory = new();
    public List<string> clothing = new();

    public StartStats()
    {
        inventory = new List<string>();
        clothing = new List<string>();
    }
}
