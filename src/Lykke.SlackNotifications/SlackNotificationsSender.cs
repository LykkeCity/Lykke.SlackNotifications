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

    public class SlackNotificationsSender : ISlackNotificationsSender
    {
        private readonly IMessageProducer<SlackMessageQueueEntity> _queue;

        public SlackNotificationsSender(IMessageProducer<SlackMessageQueueEntity> queue)
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

            await _queue.ProduceAsync(slackMessage);
        }
    }

}
