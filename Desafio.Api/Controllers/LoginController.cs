using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Filter;
using Desafio.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using static Desafio.Domain.Utils.Exceptions.Exceptions;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<AccessProfileDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                var login = await _loginService.Login(loginDTO);

                return login;
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
