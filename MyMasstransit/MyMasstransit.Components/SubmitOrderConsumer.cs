using System;
using System.Threading.Tasks;
using MassTransit;
using MyMasstransit.Contract;

namespace MyMasstransit.Components
{
    public class SubmitOrderConsumer:IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            await context.RespondAsync<OrderSubmissionAccepted>(new OrderSubmissionAccepted
            {
                
            });
        }
    }
}