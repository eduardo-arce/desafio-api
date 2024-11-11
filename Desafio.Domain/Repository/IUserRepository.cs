using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUsersList(UserFilterDTO userFilterDTO, int pageNumber, int pageSize);
    }
}
