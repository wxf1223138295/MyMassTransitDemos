using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using TestMediatorContract;

namespace TestMediator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRequestClient<CreateOrder> _requestClient;
        private readonly IMediator _mediator;

        public OrderController(IRequestClient<CreateOrder> requestClient, IMediator mediator)
        {
            _requestClient = requestClient;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid id,string customernum)
        {
            var response= await _requestClient.GetResponse<OrderSubmissionAccecpt>(values: new
            {
                OrderId=id,
                CustomorNum=customernum,
                TimeSpam=InVar.Timestamp
            });

        
            
            return Ok(response.Message);
        }
    }
}