using Desafio.Domain.Service;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Desafio.Service
{
    public class UpdateNotificationService : Hub, IUpdateNotificationService
    {
        private readonly IHubContext<UpdateNotificationService> _hub;
        private readonly ILogger<UpdateNotificationService> _logger;

        public UpdateNotificationService(IHubContext<UpdateNotificationService> hub, ILogger<UpdateNotificationService> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public async Task SendMessage(string user, string message)
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", user, message);
            _logger.LogInformation($"user: {user} - message: {message}");
        }
    }
}
