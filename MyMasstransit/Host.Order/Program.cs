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
                    //services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
                    services.AddMassTransit(cfg =>
                    {
                        cfg.SetKebabCaseEndpointNameFormatter();
                        cfg.AddConsumer<CreateOrderConsumer>();
                        cfg.UsingRabbitMq((context,config) =>
                        {
                            config.Host("115.159.155.126",30011,"my_vhost", rabbithost =>
                            {
                                rabbithost.Username("admin");
                                rabbithost.Password("admin");
                            });
                            
                           // config.ReceiveEndpoint("order-create",o =>
                           // {
                           //     o.Consumer(()=>new CreateOrderConsumer());
                           // });
                           config.ConfigureEndpoints(context);
                        });
                     
                    });

                    services.AddHostedService<MassTransitConsoleHostedService>();
                })
                .ConfigureAppConfiguration((context, app) =>
                {
                    
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Debug);
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
