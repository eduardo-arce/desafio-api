using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Repository;
using Desafio.Domain.Service;

namespace Desafio.Service
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IUnitOfWork _uow;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<WeatherForecastDTO>> GetList()
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecastDTO
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList();

            return result;
        }

        public async Task<List<User>> UsersList()
        {
            var consultUsers = (await _uow.User.GetAll()).ToList();

            return consultUsers;
        }
    }
}
