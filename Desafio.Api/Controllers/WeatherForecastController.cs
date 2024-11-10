using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet("GetWeatherForecast")]
        public async Task<List<WeatherForecastDTO>> Get()
        {
            var result = await _weatherForecastService.GetList();

            return result;
        }

        [HttpGet("Users")]
        public async Task<List<User>> GetUsers()
        {
            var result = await _weatherForecastService.UsersList();

            return result;
        }
    }
}
