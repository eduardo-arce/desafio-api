using AutoMapper;
using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Filter;
using Desafio.Domain.Repository;
using Desafio.Domain.Service;

namespace Desafio.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IUpdateNotificationService _updateNotificationService;

        public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWork uow, IUpdateNotificationService updateNotificationService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _uow = uow;
            _updateNotificationService = updateNotificationService;
        }

        public async Task UserRegister(UserDTO userDTO)
        {
            var userMapper = _mapper.Map<User>(userDTO);

            await _userRepository.AddAsync(userMapper);
            await _uow.CommitAsync();

            await _updateNotificationService.SendMessage(userDTO.FirstName, "created successfully");
        }

        public async Task<PaginateDTO<UserDTO>> UserSearch(UserFilterDTO userFilterDTO, int pageNumber, int pageSize)
        {
            var consultUsers = await _userRepository.GetUsersList(userFilterDTO, pageNumber, pageSize);

            var usersListDTO = _mapper.Map<List<UserDTO>>(consultUsers);

            var paginated = PaginateDTO<UserDTO>.CreatePagination(usersListDTO, pageNumber, pageSize);

            return paginated;
        }

        public async Task<ChartsDTO> ConsultChart()
        {
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
            var consultUserById = await GetByIdWithValidate(id);

            var userDTO = _mapper.Map<UserDTO>(consultUserById);

            return userDTO;
        }

        public async Task UserUpdate(int id, UserDTO userDTO)
        {
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
            var consultUserById = await GetByIdWithValidate(id);
            consultUserById.IsActive = status;

            await _userRepository.UpdateAsync(consultUserById);
            await _uow.CommitAsync();

            await _updateNotificationService.SendMessage(consultUserById.Name, "updated successfully");
        }

        public async Task UpdateMyPassword(int id, NewPasswordDTO newPasswordDTO)
        {
            var consultUserById = await GetByIdWithValidate(id);
            consultUserById.Password = newPasswordDTO.NewPassword;

            await _userRepository.UpdateAsync(consultUserById);
            await _uow.CommitAsync();
        }

        public async Task DeleteUser(int id)
        {
            var consultUserById = await GetByIdWithValidate(id);

            await _userRepository.DeleteAsync(consultUserById);
            await _uow.CommitAsync();

            await _updateNotificationService.SendMessage(consultUserById.Name, "deleted successfully");
        }

        public async Task<User> GetByIdWithValidate(int id)
        {
            var consultUserById = await _userRepository.GetById(id);

            if (consultUserById == null)
                throw new Exception($"Usuário não encontrado na base de dados");

            return consultUserById;
        }
    }
}
