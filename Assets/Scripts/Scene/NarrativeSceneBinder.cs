// File: Assets/Scripts/Scene/NarrativeSceneBinder.cs
using UnityEngine;

namespace Game.DialogueSystem
{
    public class NarrativeSceneBinder : MonoBehaviour
    {
        [SerializeField] private StoryFlags flagSystem;
        [SerializeField] private string sceneTriggerFlag;

        public void MarkSceneEntered() => flagSystem.SetFlag(sceneTriggerFlag, true);
    }
}
