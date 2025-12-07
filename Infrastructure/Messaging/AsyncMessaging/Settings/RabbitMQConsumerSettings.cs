using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.AsyncMessaging.Settings
{
	public class RabbitMQConsumerSettings
	{
		public string Host { get; set; } = "localhost";
		public int Port { get; set; } = 5672;
		public string Username { get; set; } = "guest";
		public string Password { get; set; } = "guest";
	}
}
