using UnityEngine;

internal interface IGenMarker
{
    bool IsInclude { get; }
    string Name { get; }
    Object GetNativeObject();
}