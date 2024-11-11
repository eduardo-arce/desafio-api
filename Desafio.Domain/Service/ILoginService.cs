using Desafio.Domain.DTO;

namespace Desafio.Domain.Service
{
    public interface ILoginService
    {
        Task<AccessProfileDTO> Login(LoginDTO loginDTO);
    }
}
