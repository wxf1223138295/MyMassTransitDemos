using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Mediator;
using MassTransit.RabbitMqTransport.Integration;
using Microsoft.AspNetCore.Mvc;
using TestMediatorContract;

namespace TestMediator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRequestClient<CreateOrder> _requestClient;

        public OrderController(IRequestClient<CreateOrder> requestClient)
        {
            _requestClient = requestClient;
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid id,string customernum)
        {
            id=Guid.NewGuid();
            customernum = new Random(2).Next().ToString();
            var response= await _requestClient.GetResponse<OrderSubmissionAccecpt>(values: new CreateOrder
            {
                OrderId=id,
                CustomorNum=customernum,
                TimeSpam=InVar.Timestamp
            });
            
            
            return Ok(response.Message);
        }
    }
}