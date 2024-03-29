﻿using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using OpenPOS_API.Models;
using static System.Net.Mime.MediaTypeNames;

namespace OPENPOS_API
{
    public class OrderEventHub : Hub
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
