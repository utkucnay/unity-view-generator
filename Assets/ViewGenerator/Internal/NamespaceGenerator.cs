using System;

internal class NamespaceGenerator : IGeneratorable, IScopeGenerator, IDisposable
{
    internal static string NAMESPACE_FORMAT = "namespace {0}";

    string namespaceName;

    public event Action DisposeEvent;

    internal NamespaceGenerator(string namespaceName)
    {
        this.namespaceName = namespaceName;
    }

    public string Generate()
    {
        return string.Format(NAMESPACE_FORMAT, namespaceName);
    }

    public void Dispose() 
    {
        DisposeEvent?.Invoke();
    }
}
