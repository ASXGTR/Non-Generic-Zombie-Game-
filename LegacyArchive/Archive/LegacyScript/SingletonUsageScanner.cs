// 🔒 Deprecated SingletonUsageScanner.cs
// Logic modularized into SingletonRegistry.cs, SceneAuditTool.cs, and DebugLogger.cs on 2025-08-05
// Retained for reference only. Do not use in runtime.
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class SingletonUsageScanner : EditorWindow
{
    private static readonly string[] Keywords = new[]
    {
        "ScriptableSingleton",
        "PackageManagerProjectSettings",
        "[InitializeOnLoad]",
        "EditorPrefs",
        "SettingsService"
    };

    private const string LogFolderPath = "Assets/EditorScanLogs";
    private const string LogFileName = "singleton_usages.txt";

    [MenuItem("Tools/Item Tools/Scan Editor Scripts for Singletons")]
    public static void ScanProjectForSingletons()
    {
        string[] allFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
        List<string> results = new();
        int hitCount = 0;

        foreach (string filePath in allFiles)
        {
            string normalizedPath = filePath.Replace('\\', '/');
            if (!normalizedPath.Contains("/Editor/"))
                continue;

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                foreach (var keyword in Keywords)
                {
                    if (line.Contains(keyword))
                    {
                        string relativePath = "Assets" + normalizedPath.Replace(Application.dataPath, "");
                        string result = $"[{keyword}] in {relativePath} (line {i + 1}): {line.Trim()}";
                        Debug.LogWarning($"?? {result}");
                        results.Add(result);
                        hitCount++;
                    }
                }
            }
        }

        // ?? Write results to log file
        if (hitCount > 0)
        {
            if (!Directory.Exists(LogFolderPath))
                Directory.CreateDirectory(LogFolderPath);

            string fullLogPath = Path.Combine(LogFolderPath, LogFileName);
            File.WriteAllLines(fullLogPath, results);
            AssetDatabase.Refresh();

            Debug.Log($"?? Scan complete: {hitCount} usages found.\n?? Log saved to: {fullLogPath}");
        }
        else
        {
            Debug.Log("? No singleton-related usage found in Editor scripts.");
        }
    }
}
