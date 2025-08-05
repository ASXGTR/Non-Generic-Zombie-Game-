using System;
using System.IO;
using System.Text.RegularExpressions;

public static class NamespaceFixer
{
    public static void FixNamespaces(string rootPath, string defaultNamespace)
    {
        var files = Directory.GetFiles(rootPath, "*.cs", SearchOption.AllDirectories);

        foreach (var path in files)
        {
            var content = File.ReadAllText(path);
            if (Regex.IsMatch(content, @"namespace\s+\w")) continue;

            string relativeFolder = Path.GetDirectoryName(path).Replace('\\', '.').Replace('/', '.');
            string folderNamespace = string.IsNullOrEmpty(relativeFolder) ? "General" : relativeFolder;
            string fullNamespace = $"{defaultNamespace}.{folderNamespace}";

            string wrapped = $"namespace {fullNamespace} {{\n{content}\n}}";
            File.WriteAllText(path, wrapped);

            Console.WriteLine($"✅ Added namespace to: {path}");
        }
    }
}
