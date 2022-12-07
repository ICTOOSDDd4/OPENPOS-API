namespace OPENPOS_API
{
    public class Events
    {
        public static void Initialize(WebApplication app)
        {
            app.MapHub<OrderEventHub>("/order_event");

            app.MapHub<TikkieEventHub>("/tikkie_event");
        }
    }
}
