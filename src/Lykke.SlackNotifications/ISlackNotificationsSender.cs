using System.Threading.Tasks;

namespace Lykke.SlackNotifications
{
    public interface ISlackNotificationsSender
    {
        Task SendAsync(string type, string sender, string message);
    }

    public static class SlackNotificationsSenderExt
    {
        public static Task SendInfoAsync(this ISlackNotificationsSender src, string sender, string message)
        {
            return src.SendAsync("Info", sender, message);
        }

        public static Task SendErrorAsync(this ISlackNotificationsSender src, string sender, string message)
        {
            return src.SendAsync("Errors", sender, message);
        }

        public static Task SendWarningAsync(this ISlackNotificationsSender src, string sender, string message)
        {
            return src.SendAsync("Warning", sender, message);
        }
    }

}
