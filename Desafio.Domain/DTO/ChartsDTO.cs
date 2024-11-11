using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.DTO
{
    public class ChartsDTO
    {
        public List<IndividualChartDTO> Status { get; set; }

        public List<IndividualChartDTO> Profile { get; set; }
    }
}
