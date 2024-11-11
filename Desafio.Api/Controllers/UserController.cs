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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Registration")]
        public async Task<ActionResult> UserRegister(UserDTO userDTO)
        {
            try
            {
                await _userService.UserRegister(userDTO);

                return Ok();
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

        [HttpGet("Consult")]
        public async Task<ActionResult<PaginateDTO<UserDTO>>> UserSearch([FromQuery] UserFilterDTO userFilterDTO, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var consultUsersList = await _userService.UserSearch(userFilterDTO, pageNumber, pageSize);

                return consultUsersList;
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

        [HttpGet("ConsultChart")]
        public async Task<ActionResult<ChartsDTO>> ConsultChart()
        {
            try
            {
                var consultChart = await _userService.ConsultChart();

                return consultChart;
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

        [HttpGet("ConsultById/{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            try
            {
                var consultUser = await _userService.GetById(id);

                return consultUser;
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

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserDTO userDTO)
        {
            try
            {
                await _userService.UserUpdate(id, userDTO);

                return Ok();
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

        [HttpPut("UpdateStatus/{id}")]
        public async Task<ActionResult> UpdateStatusUser(int id, bool status)
        {
            try
            {
                await _userService.UpdateStatusUser(id, status);

                return Ok();
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

        [HttpPut("UpdateMyPassword/{id}")]
        public async Task<ActionResult> UpdateMyPassword(int id, NewPasswordDTO newPasswordDTO)
        {
            try
            {
                await _userService.UpdateMyPassword(id, newPasswordDTO);

                return Ok();
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

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);

                return Ok();
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
