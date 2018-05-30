using System;
using System.Threading.Tasks;
using Common;

namespace Lykke.SlackNotifications
{
    public class SlackMessageQueueEntity
    {
        public string Type { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
    }

    public class SlackNotificationsSender : ISlackNotificationsSender, IDisposable
    {
        private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss";

        private readonly IMessageProducer<SlackMessageQueueEntity> _queue;
        private readonly bool _ownQueue;

        public SlackNotificationsSender(IMessageProducer<SlackMessageQueueEntity> queue, bool ownQueue = false)
        {
            _queue = queue;
            _ownQueue = ownQueue;
        }

        public Task SendAsync(string type, string sender, string message)
        {
            return SendAsync(DateTime.UtcNow, type, sender, message);
        }

        public Task SendAsync(DateTime moment, string type, string sender, string message)
        {
            var slackMessage = new SlackMessageQueueEntity
            {
                Type = type,
                Message = message,
                Sender = $"[{moment.ToString(TimestampFormat)}] {sender}",
            };

            return _queue.ProduceAsync(slackMessage);
        }

        public void Dispose()
        {
            if (_ownQueue)
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                // There is AzureQueuePublisher<T> in the Lykke.AzureQueueIntegration nuget
                (_queue as IDisposable)?.Dispose();
            }
        }
    }

}
