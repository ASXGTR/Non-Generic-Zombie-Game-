// File: Assets/Scripts/Quests/QuestLog.cs
using UnityEngine;
using System.Collections.Generic;

namespace Game.Quests
{
    [System.Serializable]
    public class Quest
    {
        public string id;
        public string description;
        public bool completed;
    }

    public class QuestLog : MonoBehaviour
    {
        [SerializeField] private List<Quest> activeQuests = new();

        public void AddQuest(string id, string description)
        {
            activeQuests.Add(new Quest { id = id, description = description, completed = false });
            Debug.Log($"[QuestLog] Added quest: {description}");
        }

        public void CompleteQuest(string id)
        {
            foreach (var quest in activeQuests)
            {
                if (quest.id == id)
                {
                    quest.completed = true;
                    Debug.Log($"[QuestLog] Completed quest: {quest.description}");
                }
            }
        }
    }
}
