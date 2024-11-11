using AutoMapper;
using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Filter;
using Desafio.Domain.Repository;
using Desafio.Domain.Service;
using Desafio.Domain.Utils.Email;
using Desafio.Domain.Utils.Hash;
using Desafio.Domain.Utils.Jwt;
using Microsoft.AspNetCore.Http;
using static Desafio.Domain.Utils.Exceptions.Exceptions;

namespace Desafio.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IUpdateNotificationService _updateNotificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWork uow, IUpdateNotificationService updateNotificationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _uow = uow;
            _updateNotificationService = updateNotificationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task UserRegister(UserDTO userDTO)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await ValidateToken(token); 
            await ValidateEmail(userDTO.Email);

            var userMapper = _mapper.Map<User>(userDTO);
            userMapper.Password = HashService.HashPassword(userDTO.Password);

            await _userRepository.AddAsync(userMapper);
            await _uow.CommitAsync();

            await _updateNotificationService.SendMessage(userDTO.FirstName, "created successfully");
        }

        public async Task<PaginateDTO<UserDTO>> UserSearch(UserFilterDTO userFilterDTO, int pageNumber, int pageSize)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await ValidateToken(token);

            var consultUsers = await _userRepository.GetUsersList(userFilterDTO, pageNumber, pageSize);

            var usersListDTO = _mapper.Map<List<UserDTO>>(consultUsers);

            var paginated = PaginateDTO<UserDTO>.CreatePagination(usersListDTO, pageNumber, pageSize);

            return paginated;
        }

        public async Task<ChartsDTO> ConsultChart()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await ValidateToken(token);

            var consultUserDatabase = (await _userRepository.GetAll()).ToList();

            var userGroups = consultUserDatabase
                .GroupBy(user => new { user.IsActive, user.AcessLevel })
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );

            var status = new List<IndividualChartDTO>
            {
                new IndividualChartDTO { Name = "Ativo", Value = userGroups.Where(x => x.Key.IsActive).Sum(x => x.Value) },
                new IndividualChartDTO { Name = "Inativo", Value = userGroups.Where(x => !x.Key.IsActive).Sum(x => x.Value) }
            };

            var profile = new List<IndividualChartDTO>
            {
                new IndividualChartDTO { Name = "Admin", Value = userGroups.Where(x => x.Key.AcessLevel.Equals("Admin")).Sum(x => x.Value) },
                new IndividualChartDTO { Name = "Comum", Value = userGroups.Where(x => x.Key.AcessLevel.Equals("Comum")).Sum(x => x.Value) }
            };

            var chartsDTO = new ChartsDTO
            {
                Status = status,
                Profile = profile
            };

            return chartsDTO;
        }

        public async Task<UserDTO> GetById(int id)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await ValidateToken(token);

            var consultUserById = await GetByIdWithValidate(id);

            var userDTO = _mapper.Map<UserDTO>(consultUserById);

            return userDTO;
        }

        public async Task UserUpdate(int id, UserDTO userDTO)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int updaterId = await ValidateToken(token);
            await ValideUserAdmin(id, updaterId);
            await ValidateEmail(userDTO.Email);

            var consultUserById = await GetByIdWithValidate(id);

            var user = _mapper.Map<User>(userDTO);
            user.Id = id;
            user.CreatedAt = consultUserById.CreatedAt;

            await _userRepository.UpdateAsync(user);
            await _uow.CommitAsync();

            await _updateNotificationService.SendMessage(consultUserById.Name, "updated successfully");
        }

        public async Task UpdateStatusUser(int id, bool status)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int updaterId = await ValidateToken(token);
            await ValideUserAdmin(id, updaterId);

            var consultUserById = await GetByIdWithValidate(id);
            consultUserById.IsActive = status;
            consultUserById.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(consultUserById);
            await _uow.CommitAsync();

            await _updateNotificationService.SendMessage(consultUserById.Name, "updated successfully");
        }

        public async Task UpdateMyPassword(int id, NewPasswordDTO newPasswordDTO)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int updaterId = await ValidateToken(token);
            await ValideUserAdmin(id, updaterId);

            var consultUserById = await GetByIdWithValidate(id);
            consultUserById.Password = HashService.HashPassword(newPasswordDTO.NewPassword);
            consultUserById.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(consultUserById);
            await _uow.CommitAsync();
        }

        public async Task DeleteUser(int id)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int updaterId = await ValidateToken(token);
            await ValideUserAdmin(id, updaterId);

            var consultUserById = await GetByIdWithValidate(id);

            await _userRepository.DeleteAsync(consultUserById);
            await _uow.CommitAsync();

            await _updateNotificationService.SendMessage(consultUserById.Name, "deleted successfully");
        }

        public async Task<User> GetByIdWithValidate(int id)
        {
            var consultUserById = await _userRepository.GetById(id);
            if (consultUserById == null)
                throw new NotFoundException($"Usuário não encontrado na base de dados");

            return consultUserById;
        }

        public async Task<int> ValidateToken(string token)
        {
            var jwtService = new JwtService();
            int userId = jwtService.ValidateToken(token);
            if (userId == 0)
                throw new UnauthorizedException("Acesso negado");

            var user = await GetByIdWithValidate(userId);
            if (!user.IsActive)
                throw new ForbiddenException("Acesso negado");

            return userId;
        }

        public async Task ValideUserAdmin(int id, int updaterId)
        {
            if (id == 1)
                throw new ForbiddenException("Parâmetros do usuário admin não podem ser alterados");

            var user = await GetByIdWithValidate(updaterId);
            if (!user.AcessLevel.Equals("Admin"))
                throw new ForbiddenException("Usuário não tem permissão para escrita de dados");
        }

        public async Task ValidateEmail(string email)
        {
            bool emailOk = EmailValidatorService.IsValidEmail(email);
            if (!emailOk)
                throw new BadRequestException("Email está incorreto");
        }
    }
}
