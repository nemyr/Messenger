using DAL;
using MessengerAPI.OptionsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace MessengerAPI.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IDbContextFactory<ApplicationContext> contextFactory, IOptions<AuthOptions> options)
        {
            _logger = logger;

            //logger.LogDebug(options.Value.ISSUER);
            //logger.LogDebug(user.ToString());
            //throw new Exception("eee");
            /*
            var context = contextFactory.CreateDbContext();
            context.Users.Add(new DAL.Models.User() { Name = "123" });
            context.SaveChanges();
            */
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //int itm = (int)HttpContext.Items["itm"];
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}