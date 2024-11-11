using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.DTO
{
    public class PaginateDTO<T>
    {
        public int Current { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public List<T> Data { get; set; }

        public static PaginateDTO<T> CreatePagination(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var data = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginateDTO<T>
            {
                Current = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Data = data
            };
        }
    }
}
