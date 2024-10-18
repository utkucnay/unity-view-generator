using UnityEngine;

public interface IGenMarker
{
    bool IsInclude { get; }
    string Name { get; }
    Object GetNativeObject();
}