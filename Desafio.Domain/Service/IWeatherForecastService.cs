using Desafio.Domain.DTO;
using Desafio.Domain.Entity;

namespace Desafio.Domain.Service
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecastDTO>> GetList();
        Task<List<User>> UsersList();
    }
}
