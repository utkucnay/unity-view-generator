using UnityEngine;
using UnityEngine.UI;

namespace ViewGenerator
{
    [RequireComponent(typeof(RawImage))]
    public class RawImageGenMarker : GenMarker
    {
        public override Object GetNativeObject()
        {
            return GetComponent<RawImage>();
        }
    }
}