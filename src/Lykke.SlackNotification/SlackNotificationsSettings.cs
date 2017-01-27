using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lykke.SlackNotifications
{
    public class SlackNotificationsSettings
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
