using TMPro;
using UnityEngine;

namespace ViewGenerator
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshGenMarker : GenMarker
    {
        public override Object GetNativeObject()
        {
            return GetComponent<TextMeshProUGUI>();
        }
    }
}