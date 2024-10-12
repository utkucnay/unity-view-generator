using UnityEngine;
using UnityEngine.UI;

class RawImageGenMarker : GenMarker
{
    public override Object GetNativeObject()
    {
        return GetComponent<RawImage>();
    }
}