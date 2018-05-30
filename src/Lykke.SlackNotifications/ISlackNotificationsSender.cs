using System;
using System.Threading.Tasks;

namespace Lykke.SlackNotifications
{
    public interface ISlackNotificationsSender
    {
        Task SendAsync(string type, string sender, string message);
        Task SendAsync(DateTime moment, string type, string sender, string message);
    }
}
