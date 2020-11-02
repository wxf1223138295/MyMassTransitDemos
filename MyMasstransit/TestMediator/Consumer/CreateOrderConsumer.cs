using System.Threading.Tasks;
using MassTransit;
using TestMediator.InterfaceModel;

namespace TestMediator.Consumer
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