using UnityEngine;
using UnityEngine.UI;

namespace ViewGenerator
{
    [RequireComponent(typeof(Text))]
    public class TextGenMarker : GenMarker
    {
        public override Object GetNativeObject()
        {
            return GetComponent<Text>();
        }
    }
}