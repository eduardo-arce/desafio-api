using AutoMapper;
using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Filter;
using Desafio.Domain.Repository;
using Desafio.Domain.Service;

namespace Desafio.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AccessProfileDTO> Login(LoginDTO loginDTO)
        {
            var user = await _userRepository.Get(x => x.Email.Equals(loginDTO.Email));

            if (user == null || !user.Password.Equals(loginDTO.Password))
                throw new Exception("Usuário ou senha inválidos");

            if (!user.IsActive)
                throw new Exception("Usuário inativo");

            var accessUser = new AccessProfileDTO
            {
                FullName = $"{user.Name} {user.Surname}",
                Profile = user.AcessLevel,
                Token = "adhjkkjhgfds"
            };

            return accessUser;
        }
    }
}
