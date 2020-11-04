using System;
using System.Threading.Tasks;
using MassTransit;
using TestMediatorContract;

namespace TestMediatorHandler
{
    public class CreateOrderConsumer:IConsumer<CreateOrder>
    {
        public async Task Consume(ConsumeContext<CreateOrder> context)
        {
            await context.RespondAsync<OrderSubmissionAccecpt>(new OrderSubmissionAccecpt
            {
                OrderId=context.Message.OrderId,
                CustomorNum=$"收到：{context.Message.CustomorNum}",
                TimeSpam=InVar.Timestamp
            });
        }
    }
}