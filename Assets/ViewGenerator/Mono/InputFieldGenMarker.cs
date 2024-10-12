using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

class InputFieldGenMarker : GenMarker, IMarkerEvent
{
    static readonly string INPUTFIELD_GENERIC_EVENT = "_{0}.{2}.AddListener( x => {1}( _{0}, new InputFieldEventArgs(x)));";

    [ShowIf("isInclude")]
    public bool valueChangedEvent = false;
    [ShowIf("valueChangedEvent")]
    public string valueChangedCallbackName = string.Empty;

    [ShowIf("isInclude")]
    public bool onEndEditEvent = false;
    [ShowIf("onEndEditEvent")]
    public string onEndEditCallbackName = string.Empty;

    [ShowIf("isInclude")]
    public bool onSelectEvent = false;
    [ShowIf("onSelectEvent")]
    public string onSelectCallbackName = string.Empty;

    [ShowIf("isInclude")]
    public bool onDeselectEvent = false;
    [ShowIf("onDeselectEvent")]
    public string onDeselectCallbackName = string.Empty;

    public override Object GetNativeObject()
    {
        return GetComponent<TMP_InputField>();
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
                        SubscribeEvent = string.Format(INPUTFIELD_GENERIC_EVENT, Name.FirstCharacterToLower(), valueChangedCallbackName, "onValueChanged"),
                        ParamaterEvents = new MarkerParamaterEvent[]
                        {
                            new MarkerParamaterEvent() { Name = "sender", Type = typeof(object) },
                            new MarkerParamaterEvent() { Name = "eventArgs", Type = typeof(InputFieldEventArgs) },
                        },
                    }
                );
        }


        if (onEndEditEvent)
        {
            markerEvents.Add(
                    new MarkerEventModel()
                    {
                        EventName = onEndEditCallbackName,
                        SubscribeEvent = string.Format(INPUTFIELD_GENERIC_EVENT, Name.FirstCharacterToLower(), onEndEditCallbackName, "onEndEdit"),
                        ParamaterEvents = new MarkerParamaterEvent[]
                        {
                            new MarkerParamaterEvent() { Name = "sender", Type = typeof(object) },
                            new MarkerParamaterEvent() { Name = "eventArgs", Type = typeof(InputFieldEventArgs) },
                        },
                    }
                );
        }

        if (onSelectEvent)
        {
            markerEvents.Add(
                    new MarkerEventModel()
                    {
                        EventName = onSelectCallbackName,
                        SubscribeEvent = string.Format(INPUTFIELD_GENERIC_EVENT, Name.FirstCharacterToLower(), onSelectCallbackName, "onSelect"),
                        ParamaterEvents = new MarkerParamaterEvent[]
                        {
                            new MarkerParamaterEvent() { Name = "sender", Type = typeof(object) },
                            new MarkerParamaterEvent() { Name = "eventArgs", Type = typeof(InputFieldEventArgs) },
                        },
                    }
                );
        }
        
        if (onDeselectEvent)
        {
            markerEvents.Add(
                    new MarkerEventModel()
                    {
                        EventName = onDeselectCallbackName,
                        SubscribeEvent = string.Format(INPUTFIELD_GENERIC_EVENT, Name.FirstCharacterToLower(), onDeselectCallbackName, "onDeselect"),
                        ParamaterEvents = new MarkerParamaterEvent[]
                        {
                            new MarkerParamaterEvent() { Name = "sender", Type = typeof(object) },
                            new MarkerParamaterEvent() { Name = "eventArgs", Type = typeof(InputFieldEventArgs) },
                        },
                    }
                );
        }

        return markerEvents;
    }
}