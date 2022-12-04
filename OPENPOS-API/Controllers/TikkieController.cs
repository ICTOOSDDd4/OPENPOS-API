using Microsoft.AspNetCore.Mvc;
using OpenPOS_API.Models;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPENPOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TikkieController : ControllerBase
    {
        private readonly IHubContext<TikkieEventHub> _hubContext;

        private readonly IConfiguration _configuration;
        public TikkieController(IHubContext<TikkieEventHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> PaymentNotification([FromBody] string jsonbody)
        {
            dynamic eventObj = JsonConvert.DeserializeObject(jsonbody);
            Tikkie payment = new Tikkie();
            payment.paymentToken = eventObj.paymentToken;
            payment.paymentRequestToken = eventObj.paymentRequestToken;
            payment.notificationType = eventObj.notificationType;
            payment.subscriptionId = eventObj.subscriptionId;
            
            await _hubContext.Clients.All.SendAsync("PaymentConformation", payment);
            
            return Ok("Succes");
        }
    }
}
