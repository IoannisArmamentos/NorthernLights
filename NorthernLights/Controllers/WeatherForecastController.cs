using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NorthernLights.Controllers
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
        public async Task<Root> Get(string latitude, string longitude)
        { 
            var apiKey = "5fb1dc554c8f1fda3541e7f155802c28";
            string url = $"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";
            var response = await GetAsync(url).ConfigureAwait(false);
            return response;

        }

        //Send a GET request to the specified Uri as an asynchronous operation.
        public async Task<Root> GetAsync(string uri)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var reply = JsonConvert.DeserializeObject<Root>(responseBody);
            
            //Uses the Extensions.cs to transform date&time for some Weatherforecast.cs elements
            foreach(var daily in reply.daily)
            {
                daily.dt_DateTime = daily.dt.GetDate();
                daily.sunriseTime = daily.sunrise.GetTime();
                daily.sunsetTime = daily.sunset.GetTime();
            }

            return reply;
        }
    }
}
