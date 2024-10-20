using System.Collections.Generic;

namespace ViewGenerator
{
    public interface IMarkerEvent
    {
        List<MarkerEventModel> GetMarkerEvents();
    }
}