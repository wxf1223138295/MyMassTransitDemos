using System.Threading.Tasks;
using MassTransit;
using TestMediatorContract;

namespace TestMediatorHandler
{
    public class CreateOrderConsumer:IConsumer<CreateOrder>
    {
        public async Task Consume(ConsumeContext<CreateOrder> context)
        {
            await context.RespondAsync<OrderSubmissionAccecpt>(new
            {
                TimeSpam=InVar.Timestamp
            });
        }
    }
}