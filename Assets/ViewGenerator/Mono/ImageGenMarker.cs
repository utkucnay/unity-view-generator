using UnityEngine;
using UnityEngine.UI;

namespace ViewGenerator
{
    [RequireComponent(typeof(Image))]
    public class ImageGenMarker : GenMarker
    {
        public override Object GetNativeObject()
        {
            return GetComponent<Image>();
        }
    }
}