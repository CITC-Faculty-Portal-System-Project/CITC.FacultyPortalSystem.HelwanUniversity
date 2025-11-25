using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Abstraction.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.AsyncMessaging.Consumer
{
    public class ExternalDataConsumerClient : BackgroundService
    {

        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceScopeFactory _scopeFactory;

        public ExternalDataConsumerClient(IConfiguration config, IServiceScopeFactory scopeFactory) 
        {

            var factory = new ConnectionFactory
            {
                HostName = config["RabbitMQ:Host"] ?? "localhost",
                Port = int.Parse(config["RabbitMQ:Port"] ?? "5672"),
                UserName = config["RabbitMQ:Username"] ?? "guest",
                Password = config["RabbitMQ:Password"] ?? "guest"
            };
            _scopeFactory = scopeFactory;
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            SetupRabbitMq();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartConsumers(stoppingToken);
            return Task.CompletedTask;
        }

        private void SetupRabbitMq()
        {
            // 1. Exchange
            _channel.ExchangeDeclare(
                exchange: "data_exchange",
                type: ExchangeType.Direct,
                durable: true);

            // 2. Declare queues
            Declare("AcademicQualification-queue");
            Declare("employmentDegree-queue");
            Declare("ManagerialPosition-queue");

            // 3. Bind queues to routing keys
            _channel.QueueBind("AcademicQualification-queue", "data_exchange", "academicQualificationsRK");
            _channel.QueueBind("employmentDegree-queue", "data_exchange", "employmentDegreeRK");
            _channel.QueueBind("ManagerialPosition-queue", "data_exchange", "managerialPositionsRK");
        }
        private void Declare(string queueName)
        {
            _channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);
        }

        private void StartConsumers(CancellationToken stoppingToken)
        {
            StartConsumer("AcademicQualification-queue");
            StartConsumer("employmentDegree-queue");
            StartConsumer("ManagerialPosition-queue");
        }

        private void StartConsumer(string queueName)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, ea) =>
            {

                string msg = Encoding.UTF8.GetString(ea.Body.ToArray());
                string routing = ea.RoutingKey;

                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<IExternalDataHandlingService>();

                    switch (routing)
                    {
                        case "academicQualificationsRK":
                            var dto = await service.AcademicDataHandle(msg);
                            break;

                        case "employmentDegreeRK":
                            await service.EmploymentDataHandle(msg);
                            break;

                        case "managerialPositionsRK":
                            await service.ManagerialDataHandle(msg);
                            break;
                    }

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    _channel.BasicNack(ea.DeliveryTag, false, requeue: true);
                }
            };

            _channel.BasicConsume(queueName, autoAck: false, consumer);
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
