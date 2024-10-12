internal class UsingNamespaceGenerator : IGeneratorable
{
    static string NAMESPACE_FORMAT = "using {0};";

    string namespaceName;

    internal UsingNamespaceGenerator(string namespaceName)
    {
        this.namespaceName = namespaceName;
    }

    public string Generate()
    {
        return string.Format(NAMESPACE_FORMAT, namespaceName);
    }
}
