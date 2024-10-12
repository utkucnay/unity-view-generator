using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshGenMarker : GenMarker
{
    public override Object GetNativeObject()
    {
        return GetComponent<TextMeshProUGUI>();
    }
}