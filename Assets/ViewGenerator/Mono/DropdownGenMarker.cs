using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using TMPro;

namespace ViewGenerator
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownGenMarker : GenMarker, IMarkerEvent
    {
        static readonly string DROPDOWN_VALUE_CHANGED = "_{0}.onValueChanged.AddListener( x => {1}( _{0}, new DropdownEventArgs(x, _{0}.options)));";

        [ShowIf("isInclude")]
        public bool valueChangedEvent = false;
        [ShowIf("valueChangedEvent")]
        public string valueChangedCallbackName = string.Empty;

        public override Object GetNativeObject()
        {
            return GetComponent<TMP_Dropdown>();
        }

        public List<MarkerEventModel> GetMarkerEvents()
        {
            List<MarkerEventModel> markerEvents = new();

            if (valueChangedEvent)
            {
                markerEvents.Add(
                        new MarkerEventModel()
                        {
                            EventName = valueChangedCallbackName,
                            SubscribeEvent = string.Format(DROPDOWN_VALUE_CHANGED, Name.FirstCharacterToLower(), valueChangedCallbackName),
                            ParamaterEvents = new MarkerParamaterEvent[]
                            {
                                new MarkerParamaterEvent() { Name = "sender", Type = typeof(object) },
                                new MarkerParamaterEvent() { Name = "eventArgs", Type = typeof(DropdownEventArgs) },
                            },
                        }
                    );
            }

            return markerEvents;
        }
    }
}
