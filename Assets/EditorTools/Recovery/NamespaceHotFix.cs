using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class HotNamespaceFixer : EditorWindow
{
    private string rootPath = "Assets/Scripts";
    private string defaultNamespace = "Project";

    [MenuItem("Tools/Hot Namespace Fixer")]
    public static void ShowWindow()
    {
        GetWindow<HotNamespaceFixer>("Namespace Fixer");
    }

    void OnGUI()
    {
        GUILayout.Label("Namespace Fixer", EditorStyles.boldLabel);
        rootPath = EditorGUILayout.TextField("Root Path:", rootPath);
        defaultNamespace = EditorGUILayout.TextField("Default Namespace:", defaultNamespace);

        if (GUILayout.Button("Fix Namespaces"))
        {
            FixNamespaces();
        }
    }

    void FixNamespaces()
    {
        string[] scriptPaths = Directory.GetFiles(rootPath, "*.cs", SearchOption.AllDirectories);
        foreach (var path in scriptPaths)
        {
            string[] lines = File.ReadAllLines(path);
            bool hasNamespace = false;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("namespace "))
                {
                    hasNamespace = true;
                    break;
                }
            }

            if (!hasNamespace)
            {
                string folderNamespace = GetFolderNamespace(path);
                string newContent =
                    $"namespace {defaultNamespace}.{folderNamespace} {{\n" +
                    string.Join("\n", lines) +
                    "\n}";

                File.WriteAllText(path, newContent);
                Debug.Log($"Namespace added to: {path}");
            }
        }

        AssetDatabase.Refresh();
    }

    string GetFolderNamespace(string fullPath)
    {
        string relativePath = fullPath.Substring(fullPath.IndexOf("Assets"));
        string subpath = relativePath.Replace("Assets/Scripts/", "");
        string folderStructure = Path.GetDirectoryName(subpath).Replace("\\", ".").Replace("/", ".");
        return string.IsNullOrEmpty(folderStructure) ? "General" : folderStructure;
    }
}
