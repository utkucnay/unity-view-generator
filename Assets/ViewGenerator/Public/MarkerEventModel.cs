using System;

namespace ViewGenerator
{
    public struct MarkerParamaterEvent
    {
        public Type Type;
        public string Name;
    }

    public struct MarkerEventModel
    {
        public string EventName;
        public string SubscribeEvent;
        public MarkerParamaterEvent[] ParamaterEvents;

        public static MarkerParamaterEvent[] DefaultParameterEvents => new MarkerParamaterEvent[]
        {
            new() { Name = "sender", Type = typeof(object) },
            new() { Name = "eventArgs", Type = typeof(EventArgs) },
        };
    }
}