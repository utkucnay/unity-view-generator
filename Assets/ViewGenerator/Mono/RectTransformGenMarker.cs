using UnityEngine;

namespace ViewGenerator
{
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformGenMarker : GenMarker
    {
        public override Object GetNativeObject()
        {
            return GetComponent<RectTransform>();
        }
    }
}
