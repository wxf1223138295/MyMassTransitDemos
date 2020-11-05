using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PubMsgHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly ISendEndpoint _sendEndpoint;
        private readonly ILogger<OrderController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(ISendEndpoint sendEndpoint, ILogger<OrderController> logger, IPublishEndpoint publishEndpoint)
        {
            _sendEndpoint = sendEndpoint;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<string> Get()
        {
           //await _publishEndpoint.<CreatedResult>(new object());
           return "";
        }
    }
}