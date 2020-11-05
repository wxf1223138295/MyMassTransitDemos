using System.Threading.Tasks;
using MassTransit;
using PubMsgContract;

namespace PubMsgHandler
{
    public class OrderCreateHandler:IConsumer<CreateOrder>
    {
        public Task Consume(ConsumeContext<CreateOrder> context)
        {
            return Task.CompletedTask;
        }
    }
}