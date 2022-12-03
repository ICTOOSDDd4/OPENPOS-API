using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;

namespace OPENPOS_API
{
    public class EventHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Debug.WriteLine(Context.ConnectionId);
            return base.OnConnectedAsync();
        }
    }
}
