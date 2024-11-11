using Desafio.Domain.Service;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Desafio.Service
{
    public class UpdateNotificationService : Hub, IUpdateNotificationService
    {
        private readonly IHubContext<UpdateNotificationService> _hub;

        public UpdateNotificationService(IHubContext<UpdateNotificationService> hub)
        {
            _hub = hub;
        }

        public async Task SendMessage(string user, string message)
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
