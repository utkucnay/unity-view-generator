using UnityEngine;

public class RectTransformGenMarker : GenMarker
{
    public override Object GetNativeObject()
    {
        return GetComponent<RectTransform>();
    }
}
