using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyMasstransit.Contract;

namespace MyMasstransitOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRequestClient<SubmitOrder> _requestClient;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IRequestClient<SubmitOrder> requestClient, ILogger<OrderController> logger)
        {
            _requestClient = requestClient;
            _logger = logger;
        }

        private void ShowCurrentId()
        {
            Console.WriteLine("show:"+Thread.CurrentThread.ManagedThreadId);
        }
        [HttpGet]
        public async Task<string> Get(Guid id,string num)
        {
          

            await _requestClient.GetResponse<OrderSubmissionAccepted>(new SubmitOrder
            {
                OrderId=id,
                TimeStamp=DateTime.Now,
                CustomerNum=num
                
            });
            return "";
        }
    }
}