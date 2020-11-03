using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestMediatorContract;
using TestMediatorHandler;

namespace Host.Order
{
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
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder=new HostBuilder();
            builder.ConfigureServices((hostContext, services) =>
                {
                    services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
                    services.AddMassTransit(cfg =>
                    {

                        cfg.AddBus(p => Bus.Factory.CreateUsingRabbitMq(
                            x =>
                            {
                                x.Host("115.159.155.126",30011, "my_vhost",p =>
                                {
                                    p.Username("admin");
                                    p.Password("admin");
                                    
                                });
                                

                            }
                           
                        ));

                        cfg.AddConsumer<CreateOrderConsumer>();

                    });

                    services.AddHostedService<MassTransitConsoleHostedService>();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                });
            
            await builder.UseWindowsService().Build().RunAsync();
            
        }
    }
}