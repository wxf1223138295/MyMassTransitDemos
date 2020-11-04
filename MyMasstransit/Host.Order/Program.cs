using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestMediatorHandler;

namespace Host.Order
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hosr =new HostBuilder();
 
            hosr.ConfigureServices((context, services) =>
                {
                    services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
                    services.AddMassTransit(cfg =>
                    {
       
                        cfg.AddConsumersFromNamespaceContaining<CreateOrderConsumer>();
                        cfg.AddBus(p => Bus.Factory.CreateUsingRabbitMq(
                            x =>
                            {
                                x.Host("115.159.155.126:30012", p =>
                                {
                                    p.Username("admin");
                                    p.Password("admin");
                                });


                            }));
                    });

                    services.AddHostedService<MassTransitConsoleHostedService>();
                })
                .ConfigureAppConfiguration((context, app) =>
                {
                    
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConsole();
                });


            await hosr.RunConsoleAsync();
        }
    }
    
    public class MassTransitConsoleHostedService :
        IHostedService
    {
        readonly IBusControl _bus;

        public MassTransitConsoleHostedService(IBusControl bus)
        {
            _bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _bus.StopAsync(cancellationToken);
        }
    }
}
