using System;

class Program
{
    static void Main(string[] args)
    {
        string rootPath = args.Length > 0 ? args[0] : "Scripts";
        string defaultNamespace = args.Length > 1 ? args[1] : "Project";

        Console.WriteLine($"🔧 Fixing namespaces in: {rootPath} using base: {defaultNamespace}");
        NamespaceFixer.FixNamespaces(rootPath, defaultNamespace);
    }
}
