using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextGenMarker : GenMarker
{
    public override Object GetNativeObject()
    {
        return GetComponent<Text>();
    }
}