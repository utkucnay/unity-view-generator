using Unity.VisualScripting;

internal class PropertyGenerator : IGeneratorable
{
    internal static readonly string PROP_FORMAT = "{3} {0} {1} => _{2};";

    string fieldType;
    string fieldName;
    GenAccess genAccess;

    internal PropertyGenerator(string fieldType, string fieldName, GenAccess genAccess)
    {
        this.fieldType = fieldType;
        this.fieldName = fieldName;
        this.genAccess = genAccess;
    }

    public string Generate()
    {
        return string.Format(PROP_FORMAT, fieldType, fieldName.FirstCharacterToUpper(), fieldName.FirstCharacterToLower(),
            genAccess.ToString().ToLower());
    }
}
