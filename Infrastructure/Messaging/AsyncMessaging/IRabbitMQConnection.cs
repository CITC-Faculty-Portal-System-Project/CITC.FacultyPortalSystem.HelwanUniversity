using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.AsyncMessaging
{
    public interface IRabbitMQConnection
    {
        public IConnection GetConnection();
    }
}
