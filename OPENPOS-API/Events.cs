namespace OPENPOS_API
{
    public class Events
    {
        public static void Initialize(WebApplication app)
        {
            app.MapHub<EventHub>("/event_hub");

            app.MapHub<TikkieEventHub>("/tikkie_event");
        }
    }
}
