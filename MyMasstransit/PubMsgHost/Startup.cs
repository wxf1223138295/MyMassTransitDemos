using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using MassTransit.MongoDbIntegration.MessageData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PubMsgContract;
using PubMsgHandler;

namespace PubMsgHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMassTransit(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();
              
                cfg.UsingRabbitMq((context,config) =>
                {
                    config.Host("115.159.155.126",30011,"my_vhost", rabbithost =>
                    {
                        rabbithost.Username("admin");
                        rabbithost.Password("admin");
                        
                        
                    });
                    
                    MessageDataDefaults.ExtraTimeToLive = TimeSpan.FromDays(1);
                    MessageDataDefaults.Threshold = 2000;
                    MessageDataDefaults.AlwaysWriteToRepository = false;

                    config.UseMessageData(new MongoDbMessageDataRepository("mongodb://127.0.0.1", "attachments"));
                    
                    
                    config.ConfigureEndpoints(context);
                });
                
                cfg.AddRequestClient<CreateOrder>(new Uri($"queue:{KebabCaseEndpointNameFormatter.Instance.Consumer<OrderCreateHandler>()}"));
            });
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