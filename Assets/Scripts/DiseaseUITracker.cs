using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.Inventory;

public class DiseaseUITracker : MonoBehaviour
{
    [Header("Dependencies")]
    public DiseaseManager diseaseManager;
    public Transform trackerPanel;
    public GameObject diseaseStatusPrefab;

    private Dictionary<DiseaseType, GameObject> statusElements = new();

    void OnEnable()
    {
        DiseaseManager.OnDiseaseContracted += AddDiseaseUI;
        DiseaseManager.OnDiseaseCured += RemoveDiseaseUI;

        RefreshAll();
    }

    void OnDisable()
    {
        DiseaseManager.OnDiseaseContracted -= AddDiseaseUI;
        DiseaseManager.OnDiseaseCured -= RemoveDiseaseUI;
    }

    void Update()
    {
        foreach (var entry in statusElements)
        {
            if (!diseaseManager.activeDiseases.Exists(d => d.diseaseType == entry.Key)) continue;

            var instance = diseaseManager.activeDiseases.Find(d => d.diseaseType == entry.Key);
            if (instance != null && !instance.isCured)
            {
                var label = entry.Value.GetComponentInChildren<Text>();
                if (label != null)
                    label.text = instance.GetStatus();
            }
        }
    }

    void AddDiseaseUI(DiseaseType type)
    {
        if (statusElements.ContainsKey(type)) return;

        GameObject newEntry = Instantiate(diseaseStatusPrefab, trackerPanel);
        var label = newEntry.GetComponentInChildren<Text>();
        if (label != null)
            label.text = $"{type} 🟡 Starting...";

        statusElements.Add(type, newEntry);
    }

    void RemoveDiseaseUI(DiseaseType type)
    {
        if (statusElements.TryGetValue(type, out var element))
        {
            Destroy(element);
            statusElements.Remove(type);
        }
    }

    void RefreshAll()
    {
        foreach (Transform child in trackerPanel)
            Destroy(child.gameObject);

        statusElements.Clear();

        foreach (var instance in diseaseManager.activeDiseases)
        {
            if (!instance.isCured)
                AddDiseaseUI(instance.diseaseType);
        }
    }
}
