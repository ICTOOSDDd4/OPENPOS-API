using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OpenPOS_API.Models;

namespace OPENPOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IHubContext<EventHub> _hubContext;

        public OrderController(IHubContext<EventHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _hubContext.Clients.All.SendAsync("newOrder", new Order() {Id = 59});
            return Ok("Success");
        }
    }
}
