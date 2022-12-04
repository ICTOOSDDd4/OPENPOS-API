using System.Net;

namespace OPENPOS_API
{
    public class Events
    {
        public static void MapEvents(WebApplication app)
        {
            app.MapHub<EventHub>("/event_hub");
            app.MapHub<TikkieEventHub>("/tikkie_event");
        }
    }
}
