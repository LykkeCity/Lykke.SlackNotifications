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
        private const string _timestampFormat = "yyyy-MM-dd HH:mm:ss";

        private readonly IMessageProducer<SlackMessageQueueEntity> _queue;
        private readonly bool _ownQueue;

        public SlackNotificationsSender(IMessageProducer<SlackMessageQueueEntity> queue, bool ownQueue = false)
        {
            _queue = queue;
            _ownQueue = ownQueue;
        }

        public async Task SendAsync(string type, string sender, string message)
        {
            var slackMessage = new SlackMessageQueueEntity
            {
                Type = type,
                Message = message,
                Sender = $"[{DateTime.UtcNow.ToString(_timestampFormat)}] {sender}",
            };

            await _queue.ProduceAsync(slackMessage);
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
