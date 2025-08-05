using UnityEditor;
using UnityEngine;

namespace Editor.Disease
{
    public static class DiseaseEditorUtility
    {
        [MenuItem("Tools/Disease/Validate Registry")]
        public static void ValidateRegistry()
        {
            var allDefs = Resources.LoadAll<Gameplay.Disease.DiseaseDefinition>("DiseaseDefinitions");
            foreach (var def in allDefs)
            {
                if (string.IsNullOrEmpty(def.DiseaseName))
                    Debug.LogWarning($"DiseaseDefinition missing name: {def.name}");
            }

            Debug.Log($"Validated {allDefs.Length} disease definitions.");
        }
    }
}
