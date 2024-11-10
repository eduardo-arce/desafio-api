using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task AddRangeAsync(List<T> entity);

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(List<T> entity);

        Task DeleteAsync(T entity);

        Task DeleteRangeAsync(List<T> entity);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null);

        Task<T> Get(Expression<Func<T, bool>> expression);

        Task<T> GetById(int key);
    }
    }
