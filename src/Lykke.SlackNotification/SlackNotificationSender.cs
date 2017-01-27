using System.Threading.Tasks;
using AzureStorage.Queue;

namespace Lykke.SlackNotifications
{

    public class SlackMessageQueueEntity
    {
        public string Type { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
    }

    public class SlackNotificationSender : ISlackNotificationsSender
    {
        private readonly IQueueExt _queue;

        public SlackNotificationSender(IQueueExt queue)
        {
            _queue = queue;
        }


        public async Task SendAsync(string type, string sender, string message)
        {
            var slackMessage = new SlackMessageQueueEntity
            {
                Type = type,
                Message = message,
                Sender = sender
            };

            await _queue.PutMessageAsync(slackMessage);
        }
    }


    public class SlackNotificationSenderViaAzureQueue : SlackNotificationSender
    {
        public SlackNotificationSenderViaAzureQueue(SlackNotificationsSettings settings) 
            : base(new AzureQueueExt(settings.ConnectionString, settings.QueueName))
        {
        }

    }

}
