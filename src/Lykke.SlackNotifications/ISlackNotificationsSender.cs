using System.Threading.Tasks;

namespace Lykke.SlackNotifications
{
    public interface ISlackNotificationsSender
    {
        Task SendAsync(string type, string sender, string message);
    }

    public static class SlackNotificationsSenderExt
    {
        public static Task SendInfoAsync(this ISlackNotificationsSender src, string message)
        {
            return src.SendAsync("Info", ":exclamation:", message);
        }

        public static Task SendErrorAsync(this ISlackNotificationsSender src, string message)
        {
            return src.SendAsync("Errors", ":exclamation:", message);
        }

        public static Task SendWarningAsync(this ISlackNotificationsSender src, string message)
        {
            return src.SendAsync("Warning", ":warning:", message);
        }
    }

}
