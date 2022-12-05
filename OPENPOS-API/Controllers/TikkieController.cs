﻿using Microsoft.AspNetCore.Mvc;
using OpenPOS_API.Models;
using System.ComponentModel.DataAnnotations;
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

        private readonly IConfiguration _configuration;
        private Tikkie _tikkie;
        private Dictionary<string, string> _listeners = new();
        public TikkieController(IHubContext<TikkieEventHub> hubContext, IConfiguration configuration)
        {
            _hubContext = hubContext;
            if (configuration.GetValue<string>("TikkieAppToken").Length == 0)
            {
                TikkieService.CreateTikkieAppToken(configuration);
            }
            else
            {
                TikkieService.SubscribeToNotifications(configuration);
            }
        }

        [HttpGet]
        [Route("GetAppToken")]
        public Task<string> GetAppToken([Required] [FromHeader] string secret)
        {
            return Task.FromResult(TikkieService._tikkieAppToken);
        }

        [HttpPost]
        [Route("AddToPaymentListener")]
        public async Task<IActionResult> AddToListener([Required] [FromHeader] string connectionId, [Required] [FromHeader] string paymentRequestToken)
        {
            _listeners.Add(paymentRequestToken, connectionId);
            return Ok("Added with success");
        }
        
        [HttpPost]
        [Route("paymentNotification")]
        public async Task<IActionResult> PaymentNotification([FromBody] Tikkie payment )
        {
            // if (payment.notificationType == "PAYMENT")
            // {
                if(_listeners.TryGetValue(payment.paymentRequestToken, out string connectionId))
                {
                    await _hubContext.Clients.Client(connectionId).SendAsync("PaymentConformation", payment);
                    return Ok("Success"); 
                }
            // }
            return Problem("Error");
        }
        
    }
}
