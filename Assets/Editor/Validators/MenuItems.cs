using UnityEditor;

namespace DialogueSystem.EditorTools
{
    public static class MenuItems
    {
        [MenuItem("Tools/DialogueSystem/Run All Validators")]
        public static void RunAll()
        {
            PrefabValidator.RunPrefabScan();
            SceneFlagScanner.ScanAllScenes();
        }
    }
}
