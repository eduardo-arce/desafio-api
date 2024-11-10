using Desafio.Domain.Entity;
using Desafio.Domain.Repository;
using Desafio.Infra.Context;
using Desafio.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DesafioContext _context;

        public UnitOfWork(DesafioContext context)
        {
            _context = context;
        }

        public IUserRepository User => new UserRepository(_context);

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            if (_context != null)
            {
                _context.DisposeAsync();
                GC.SuppressFinalize(_context);
            }
        }
    }
}
