namespace Survival.Editor
{
    public enum EditorToolType { Validator, Migrator, Archiver, Cleanup, Profiler }
    public enum ValidationSeverity { Info, Warning, Error, Critical }
    public enum ScriptState { Legacy, Migrating, Stable, Deprecated }
}
