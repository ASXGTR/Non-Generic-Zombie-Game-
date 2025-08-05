using UnityEngine;
using Survival.Narrative.Parsing;
using Survival.Narrative.Logic;
using Survival.Narrative.Triggers;
using Survival.UI.Narrative;

namespace Survival.Narrative.Core
{
    public class NarrativeCoordinator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private NarrativeNodeParser parser;
        [SerializeField] private ChoiceResolver choiceResolver;
        [SerializeField] private TriggerEvaluator triggerEvaluator;
        [SerializeField] private DialogueUIController uiController;

        public void LoadNode(string nodeId)
        {
            var node = parser.Parse(nodeId);
            if (node == null)
            {
                Debug.LogWarning($"[NarrativeCoordinator] Node '{nodeId}' not found.");
                return;
            }

            if (!triggerEvaluator.Evaluate(node.triggers))
            {
                Debug.Log($"[NarrativeCoordinator] Node '{nodeId}' blocked by triggers.");
                return;
            }

            uiController.RenderNode(node);
        }

        public void SelectChoice(int index)
        {
            var outcome = choiceResolver.Resolve(index);
            if (outcome != null)
            {
                LoadNode(outcome.nextNodeId);
            }
        }
    }
}
