
using UnityEngine;

internal class ScopeGenerator : IGeneratorable
{
    bool isFirst;
    bool complete;

    internal ScopeGenerator()
    {
        isFirst = true;
        complete = false;
    }

    public string Generate()
    {
        if (complete)
        {
            Debug.LogError("Scope Not Generate Correctly");
            return null;
        }

        if (isFirst)
        {
            isFirst = !isFirst;
            return "{";
        }
        else
        {
            isFirst = !isFirst;
            complete = true;
            return "}";
        }
    }
}