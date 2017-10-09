using System.Threading.Tasks;

namespace Lykke.SlackNotifications
{
    public static class SlackNotificationsSenderExt
    {
        public static Task SendInfoAsync(this ISlackNotificationsSender src, string message, string sender = null)
        {
            return src.SendAsync("Info", sender ?? ":exclamation:", message);
        }

        public static Task SendErrorAsync(this ISlackNotificationsSender src, string message, string sender = null)
        {
            return src.SendAsync("Errors", sender ?? ":exclamation:", message);
        }

        public static Task SendWarningAsync(this ISlackNotificationsSender src, string message, string sender = null)
        {
            return src.SendAsync("Warning", sender ?? ":warning:", message);
        }

        public static Task SendMonitorAsync(this ISlackNotificationsSender src, string message, string sender = null)
        {
            return src.SendAsync("Monitor", sender ?? ":information_source:", message);
        }
    }
}
