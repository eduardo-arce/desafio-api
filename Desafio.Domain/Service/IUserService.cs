using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Domain.Service
{
    public interface IUserService
    {
        Task UserRegister(UserDTO userDTO);

        Task<PaginateDTO<UserDTO>> UserSearch(UserFilterDTO userFilterDTO, int pageNumber, int pageSize);

        Task<ChartsDTO> ConsultChart();

        Task<UserDTO> GetById(int id);

        Task UserUpdate(int id, UserDTO userDTO);

        Task UpdateStatusUser(int id, bool status);

        Task UpdateMyPassword(int id, NewPasswordDTO newPasswordDTO);

        Task DeleteUser(int id);
    }
}
