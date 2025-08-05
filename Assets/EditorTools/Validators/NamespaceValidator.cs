// File: Assets/Editor/NamespaceValidator.cs
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class NamespaceValidator : EditorWindow
{
    [MenuItem("Tools/🧼 Validate & Fix Namespaces")]
    public static void ValidateNamespaces()
    {
        string rootPath = Application.dataPath + "/Scripts";
        var scriptFiles = Directory.GetFiles(rootPath, "*.cs", SearchOption.AllDirectories);

        foreach (var filePath in scriptFiles)
        {
            string[] lines = File.ReadAllLines(filePath);
            string relativePath = filePath.Replace(rootPath, "").Trim(Path.DirectorySeparatorChar);
            string[] folderSegments = relativePath.Split(Path.DirectorySeparatorChar);
            folderSegments = folderSegments.Take(folderSegments.Length - 1).ToArray(); // drop filename
            if (folderSegments.Length == 0) continue;

            string expectedNamespace = "Game." + string.Join(".", folderSegments);

            int nsIndex = lines.ToList().FindIndex(line => line.TrimStart().StartsWith("namespace "));
            if (nsIndex == -1)
            {
                // Insert new namespace block
                lines = WrapInNamespace(lines, expectedNamespace);
                Debug.Log($"🆕 Inserted namespace into: {relativePath}");
            }
            else
            {
                string currentNamespace = lines[nsIndex].Trim().Replace("namespace ", "").Trim();
                if (currentNamespace != expectedNamespace)
                {
                    lines[nsIndex] = $"namespace {expectedNamespace}";
                    Debug.Log($"🔄 Fixed namespace in: {relativePath} ➡️ {currentNamespace} → {expectedNamespace}");
                }
            }

            File.WriteAllLines(filePath, lines);
        }

        AssetDatabase.Refresh();
        Debug.Log("✅ Namespace validation and correction complete.");
    }

    private static string[] WrapInNamespace(string[] originalLines, string ns)
    {
        var indent = "    ";
        var wrapped = new[]
        {
            $"namespace {ns}",
            "{"
        }.Concat(originalLines.Select(line => indent + line)).Concat(new[] { "}" }).ToArray();

        return wrapped;
    }
}
