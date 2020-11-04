using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using MassTransit.MultiBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using TestMediatorContract;

namespace TestMediator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq((context,config) =>
                {
                    config.Host("115.159.155.126",30011,"my_vhost", rabbithost =>
                    {
                        rabbithost.Username("admin");
                        rabbithost.Password("admin");
                    });
                    
                  // config.ConfigureEndpoints(context);
                });
                
                cfg.AddRequestClient<CreateOrder>();
                
                // cfg.AddBus(p =>Bus.Factory.CreateUsingRabbitMq(
                //     x =>
                //     {
                //         x.Host("115.159.155.126",30011,"my_vhost", p =>
                //         {
                //             p.Username("admin");
                //             p.Password("admin");
                //         });
                //         
                //         x.ConfigureEndpoints(p);
                //     }
                //     
                // ));
             
              
            });

            services.AddMassTransitHostedService();
        }

       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}