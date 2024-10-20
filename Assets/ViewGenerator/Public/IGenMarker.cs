using UnityEngine;

namespace ViewGenerator
{
    public interface IGenMarker
    {
        bool IsInclude { get; }
        string Name { get; }
        Object GetNativeObject();
    }
}