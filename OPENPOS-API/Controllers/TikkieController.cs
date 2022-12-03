using Microsoft.AspNetCore.Mvc;
using OpenPOS_API.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPENPOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TikkieController : ControllerBase
    {
        private readonly IHubContext<EventHub> _hubContext;

        private readonly IConfiguration _configuration;
        public TikkieController(IConfiguration configuration, IHubContext<EventHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }
        // GET: api/<TikkieController>
        [HttpPost]
        public async Task<IActionResult> Post([Required][FromHeader] string secret)
        {
            await _hubContext.Clients.All.SendAsync("newPayment", new Order() { Id = 59 });
            return Ok("Success");
        }
    }
}
