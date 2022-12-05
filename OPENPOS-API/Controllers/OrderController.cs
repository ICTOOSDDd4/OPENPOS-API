using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OpenPOS_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace OPENPOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IHubContext<OrderEventHub> _hubContext;

        private readonly IConfiguration _configuration;
        public OrderController(IConfiguration configuration, IHubContext<OrderEventHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Send a new event with order
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([Required][FromHeader] string secret)
        {
            await _hubContext.Clients.All.SendAsync("newOrder", new Order() {Id = 59});
            return Ok("Success");
        }
    }
}
