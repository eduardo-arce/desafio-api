using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Service
{
    public interface IUpdateNotificationService
    {
        Task SendMessage(string user, string message);
    }
}
