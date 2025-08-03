using System.IO;
using UnityEditor;
using UnityEngine;

public class SingletonUsageScanner : EditorWindow
{
    [MenuItem("Tools/Scan for ScriptableSingleton Usage")]
    public static void ScanProjectForSingletons()
    {
        string[] allFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
        bool foundAny = false;

        foreach (string filePath in allFiles)
        {
            // Only scan files inside Editor folders
            if (!filePath.Replace('\\', '/').Contains("/Editor/"))
                continue;

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("ScriptableSingleton") || line.Contains("PackageManagerProjectSettings"))
                {
                    string relativePath = "Assets" + filePath.Replace(Application.dataPath, "").Replace('\\', '/');
                    Debug.LogWarning($"Found singleton usage in {relativePath} at line {i + 1}: {line.Trim()}");
                    foundAny = true;
                }
            }
        }

        if (!foundAny)
        {
            Debug.Log("No ScriptableSingleton or PackageManagerProjectSettings usage found in Editor scripts.");
        }
        else
        {
            Debug.Log("Scan complete. Check warnings above for singleton usage.");
        }
    }
}
