using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyMasstransitOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            SynchronizationContext currentcontext = SynchronizationContext.Current;
            if (currentcontext == null)
            {
                currentcontext=new SynchronizationContext();
            }
            Console.WriteLine("start:"+Thread.CurrentThread.ManagedThreadId);
           
            await Task.Run(()=>
            {
                Console.WriteLine("run"+Thread.CurrentThread.ManagedThreadId);

                currentcontext.Post(new SendOrPostCallback(delegate(object? state)
                {
                    Console.WriteLine("show:"+Thread.CurrentThread.ManagedThreadId);
                }), null);
            });

           
            Thread.Sleep(5000);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}