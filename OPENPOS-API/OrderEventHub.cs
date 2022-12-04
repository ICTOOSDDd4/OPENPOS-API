using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using OpenPOS_API.Models;

namespace OPENPOS_API
{
    public class EventHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Debug.WriteLine(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        internal Task SendAsync(string v, Order order)
        {
            throw new NotImplementedException();
        }
        
    }
}
