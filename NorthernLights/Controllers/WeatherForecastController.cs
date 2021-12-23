using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NorthernLights.Models;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NorthernLights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpClient _httpClient;
        private string url = "onecall?lat={lat}&lon={long}&appid={apikey}&units=metric";


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory factory, IOptions<OpenWeatherOptions> options)
        {
            _logger = logger;
            // HttpClient shouldn't be created everytime we want to make an http call.
            // it's lifetime should be handled by the framework
            // the factory will create or reuse/return the OpenWeatherApi specific client, specified in Startup.cs
            _httpClient = factory.CreateClient("OpenWeatherApi");
            url = url.Replace("{apikey}", options.Value.ApiKey);
        }


        [HttpGet]
        public async Task<IActionResult> Get(string latitude, string longitude, CancellationToken token)
        {
            _logger.LogInformation($"Requesting Weather forecast for: {latitude} {longitude}");
            var response = await GetAsync(url
                .Replace("{lat}", latitude)
                .Replace("{long}", longitude), token).ConfigureAwait(false);
            _logger.LogInformation($"Requested Weather forecast for: {latitude} {longitude}");
            return response;

        }

        // Send a GET request to the specified Uri as an asynchronous operation.
        private async Task<IActionResult> GetAsync(string uri, CancellationToken token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);

                var response = await _httpClient.SendAsync(request, token).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

/*              // Uses the Extensions.cs to transform date&time for some elements
 *              // This is the "backend way" that I chose not to implement for now
                foreach (var daily in reply.daily)
                {
                    daily.dt_DateTime = daily.dt.GetDate();
                    daily.sunriseTime = daily.sunrise.GetTime();
                    daily.sunsetTime = daily.sunset.GetTime();
                }*/

                return Ok(await response.Content.ReadAsStreamAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Weather forecast request failed");
                return BadRequest(ex.Message);
            }

        }
    }
}
