using UnityEngine;
using System.Collections.Generic;
using Survival.Narrative.Core;

namespace Survival.Narrative.Parsing
{
    [CreateAssetMenu(fileName = "NarrativeNodeParser", menuName = "Narrative/Node Parser")]
    public class NarrativeNodeParser : ScriptableObject
    {
        [Header("Node Definitions")]
        [SerializeField] private List<NarrativeNode> allNodes;

        public NarrativeNode Parse(string nodeId)
        {
            var node = allNodes.Find(n => n.id == nodeId);
            if (node == null)
                Debug.LogWarning($"[NodeParser] Node ID '{nodeId}' not found.");
            return node;
        }
    }
}
