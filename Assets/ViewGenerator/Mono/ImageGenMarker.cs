using UnityEngine;
using UnityEngine.UI;

public class ImageGenMarker : GenMarker
{
    public override Object GetNativeObject()
    {
        return GetComponent<Image>();
    }
}