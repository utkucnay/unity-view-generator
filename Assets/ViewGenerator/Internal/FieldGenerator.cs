using Unity.VisualScripting;

internal class FieldGenerator : IGeneratorable
{
    internal static readonly string FIELD_FORMAT = "{2} {0} _{1};";

    string fieldType;
    string fieldName;
    GenAccess genAccess;

    internal FieldGenerator(string fieldType, string fieldName, GenAccess genAccess)
    {
        this.fieldType = fieldType;
        this.fieldName = fieldName;
        this.genAccess = genAccess;
    }

    public string Generate()
    {
        return string.Format(FIELD_FORMAT, fieldType, fieldName.FirstCharacterToLower(), genAccess.ToString().ToLower());
    }
}
