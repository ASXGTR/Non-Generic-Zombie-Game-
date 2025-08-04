using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public static class RepoScanner
{
    [MenuItem("Tools/Repo/Scan for Secrets")]
    public static void RunGitleaksScan()
    {
        string gitleaksPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "go", "bin", "gitleaks.exe");
        string projectPath = Directory.GetCurrentDirectory();
        string arguments = $"detect --source=\"{projectPath}\" --verbose --redact";

        if (!File.Exists(gitleaksPath))
        {
            UnityEngine.Debug.LogError($"Gitleaks not found at: {gitleaksPath}\nMake sure it's installed and in the default Go bin folder.");
            return;
        }

        try
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = gitleaksPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(output))
                    UnityEngine.Debug.Log($"🔍 Gitleaks Output:\n{output}");
                if (!string.IsNullOrEmpty(error))
                    UnityEngine.Debug.LogWarning($"⚠️ Gitleaks Errors:\n{error}");
            }
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError($"🚨 Failed to run Gitleaks:\n{ex.Message}");
        }
    }
}
