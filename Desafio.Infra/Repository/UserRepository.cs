using Desafio.Domain.Entity;
using Desafio.Domain.Repository;
using Desafio.Infra.Context;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

    }
}
