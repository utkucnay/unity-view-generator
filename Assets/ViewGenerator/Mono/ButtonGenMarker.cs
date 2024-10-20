using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ViewGenerator
{
    [RequireComponent(typeof(Button))]
    public class ButtonGenMarker : GenMarker, IMarkerEvent
    {
        static readonly string BUTTON_SUBEVENT_FORMAT = "_{0}.onClick.AddListener( () => {1}( _{0}, EventArgs.Empty));";

        [ShowIf("isInclude")]
        public bool clickButtonEvent = false;
        [ShowIf("clickButtonEvent")]
        public string buttonCallbackName = string.Empty;

        public override Object GetNativeObject()
        {
            return GetComponent<Button>();
        }

        public List<MarkerEventModel> GetMarkerEvents()
        {
            List<MarkerEventModel> markerEvents = new();

            if (clickButtonEvent) 
            {
                markerEvents.Add(new MarkerEventModel()
                {
                    EventName = buttonCallbackName,
                    SubscribeEvent = string.Format(BUTTON_SUBEVENT_FORMAT, Name.FirstCharacterToLower(), buttonCallbackName)
                });
            }

            return markerEvents;
        }
    }
}