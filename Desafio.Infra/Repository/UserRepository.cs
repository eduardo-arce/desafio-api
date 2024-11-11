using Desafio.Domain.DTO;
using Desafio.Domain.Entity;
using Desafio.Domain.Filter;
using Desafio.Domain.Repository;
using Desafio.Infra.Context;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DesafioContext context) : base(context)
        {

        }

        public async Task<List<User>> GetUsersList(UserFilterDTO userFilterDTO, int pageNumber, int pageSize)
        {
            var consultUserDatabase = await Task.FromResult(_context.Users.OrderByDescending(x => x.Id).ToList());

            if (userFilterDTO?.FirstName != null)
            {
                consultUserDatabase = consultUserDatabase.Where(x => x.Name.ToLower().Contains(userFilterDTO.FirstName.ToLower())).ToList();
            }

            if (userFilterDTO?.LastName != null)
            {
                consultUserDatabase = consultUserDatabase.Where(x => x.Surname.ToLower().Contains(userFilterDTO.LastName.ToLower())).ToList();
            }

            if (userFilterDTO?.Email != null)
            {
                consultUserDatabase = consultUserDatabase.Where(x => x.Email.ToLower().Contains(userFilterDTO.Email.ToLower())).ToList();
            }

            if (userFilterDTO?.Profile != null)
            {
                consultUserDatabase = consultUserDatabase.Where(x => x.AcessLevel.Equals(userFilterDTO.Profile)).ToList();
            }

            if (userFilterDTO?.Status != null)
            {
                consultUserDatabase = consultUserDatabase.Where(x => x.IsActive.Equals(userFilterDTO.Status)).ToList();
            }

            return consultUserDatabase;
        }
    }
}
