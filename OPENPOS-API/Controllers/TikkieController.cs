using Microsoft.AspNetCore.Mvc;
using OpenPOS_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;
using OPENPOS_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPENPOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TikkieController : ControllerBase
    {
        private readonly IHubContext<TikkieEventHub> _hubContext;
        
        public TikkieController(IHubContext<TikkieEventHub> hubContext, IConfiguration configuration)
        {
            _hubContext = hubContext;
            // Setting up Tikkie API
            if (configuration.GetValue<string>("TikkieAppToken").Length == 0)
            {
                TikkieService.CreateTikkieAppToken(configuration);
            }
            else
            {
                TikkieService.SetAppToken(configuration);
                TikkieService.SubscribeToNotifications(configuration);
            }
        }

        [HttpGet]
        [Route("GetAppToken")]
        public IActionResult GetAppToken([Required] [FromHeader] string secret)
        {
            return Ok(TikkieService.TikkieAppToken);
        }
        
        [HttpGet]
        [Route("RemovePaymentRequest")]
        public IActionResult RemovePaymentRequest([Required] [FromHeader] string paymentRequestToken)
        {
            Listeners.ListenersDict.Remove(paymentRequestToken);
            return Ok();
        }

        [HttpPost]
        [Route("AddToPaymentListener")]
        public IActionResult AddToListener([FromBody] Listener listen )
        {
            Listeners.ListenersDict.Add(listen.PaymentRequestToken, listen.ConnectionId);
            return Ok("Added with success");
        }
        
        [HttpPost]
        [Route("paymentNotification")]
        public async Task<IActionResult> PaymentNotification([FromBody] Tikkie payment )
        {
            if (payment.NotificationType == "PAYMENT")
            {
                if(Listeners.ListenersDict.TryGetValue(payment.PaymentRequestToken, out var connectionId))
                {
                    await _hubContext.Clients.Client(connectionId).SendAsync("PaymentConformation", payment);
                    return Ok("Success"); 
                }
            }
            return Problem($"Error: {Listeners.ListenersDict.Count} " );
        }
    }
}
