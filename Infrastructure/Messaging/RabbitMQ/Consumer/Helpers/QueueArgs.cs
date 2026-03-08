using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.AsyncMessaging.Consumer.Helpers
{
	public record QueueArgs(string QueueName, string RoutingKey);
}
