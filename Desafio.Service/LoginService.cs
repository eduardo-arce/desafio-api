using AutoMapper;
using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Filter;
using Desafio.Domain.Repository;
using Desafio.Domain.Service;
using Desafio.Domain.Utils.Hash;
using Desafio.Domain.Utils.Jwt;

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

            if (user == null || !HashService.VerifyPasswordOk(loginDTO.Password, user.Password))
                throw new Exception("Usuário ou senha inválidos");

            if (!user.IsActive)
                throw new Exception("Usuário inativo");

            var jwtService = new JwtService();
            string token = jwtService.GenerateToken(user.Id);

            var accessUser = new AccessProfileDTO
            {
                FullName = $"{user.Name} {user.Surname}",
                Profile = user.AcessLevel,
                Token = token
            };

            return accessUser;
        }
    }
}
