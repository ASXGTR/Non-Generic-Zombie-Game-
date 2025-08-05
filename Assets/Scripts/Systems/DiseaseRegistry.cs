using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Disease
{
    public static class DiseaseRegistry
    {
        private static Dictionary<string, DiseaseDefinition> _diseaseMap;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            _diseaseMap = new Dictionary<string, DiseaseDefinition>();
            var allDefinitions = Resources.LoadAll<DiseaseDefinition>("DiseaseDefinitions");

            foreach (var def in allDefinitions)
            {
                if (!_diseaseMap.ContainsKey(def.DiseaseName))
                    _diseaseMap.Add(def.DiseaseName, def);
            }
        }

        public static DiseaseDefinition Get(string name)
        {
            if (_diseaseMap == null)
                Initialize();

            _diseaseMap.TryGetValue(name, out var result);
            return result;
        }

        public static IEnumerable<DiseaseDefinition> GetAll()
        {
            if (_diseaseMap == null)
                Initialize();

            return _diseaseMap.Values;
        }
    }
}
