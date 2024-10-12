using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleGenMarker : GenMarker, IMarkerEvent
{
    static readonly string TOGGLE_VALUE_CHANGED = "_{0}.onValueChanged.AddListener( x => {1}( _{0}, new ToggleEventArgs(x)));";

    [ShowIf("isInclude")]
    public bool valueChangedEvent = false;
    [ShowIf("valueChangedEvent")]
    public string valueChangedCallbackName = string.Empty;

    public override Object GetNativeObject()
    {
        return GetComponent<Toggle>();
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
                        SubscribeEvent = string.Format(TOGGLE_VALUE_CHANGED, Name.FirstCharacterToLower(), valueChangedCallbackName),
                        ParamaterEvents = new MarkerParamaterEvent[] 
                        { 
                            new MarkerParamaterEvent() { Name = "sender", Type = typeof(object) },
                            new MarkerParamaterEvent() { Name = "eventArgs", Type = typeof(ToggleEventArgs) },
                        },
                    }
                );
        }
        
        return markerEvents;
    }
}