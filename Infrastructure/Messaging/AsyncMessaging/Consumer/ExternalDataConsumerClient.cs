using Messaging.AsyncMessaging.Consumer.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Abstraction.Contracts;
using System.Text;

namespace Messaging.AsyncMessaging.Consumer
{
	/*public class ExternalDataConsumerClient : BackgroundService
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
    }*/

	public class ExternalDataConsumerClient : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IRabbitMQConnection _connection;
        private List<IModel> _channels = new();


        public ExternalDataConsumerClient(IRabbitMQConnection rabbitMQConnection, IServiceScopeFactory scopeFactory)
        {
			_connection = rabbitMQConnection;
			_scopeFactory = scopeFactory;

        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                InitializeRabbitMQ();
                return base.StartAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"==> Error during RabbitMQ Initialization: {ex.Message}");
				throw;
			}
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                StartConsumers(stoppingToken);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"==> Error during starting consumers: {ex.Message}");
                throw;
            }
        }

		public override void Dispose()
		{
			Console.WriteLine("--> Disposing consumer channels...");

			foreach (var channel in _channels)
			{
				try
				{
					if (channel.IsOpen)
						channel.Close();
					channel.Dispose();
				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Failed to dispose channel: {ex.Message}");
				}
			}

			Console.WriteLine("--> Consumer disposed (connection kept alive).");

			base.Dispose();
		}


		#region Helpers
		private void InitializeRabbitMQ()
        {
			using var setupChannel = _connection.GetConnection().CreateModel();
			setupChannel.ExchangeDeclare(RabbitMQConstants.ExchangeName, ExchangeType.Direct, durable: true);

			var queues = QueueInitializer.InitializeQueues();

			foreach (var queue in queues)
			{
				setupChannel.QueueDeclare(queue.QueueName, durable: true, exclusive: false, autoDelete: false);
				setupChannel.QueueBind(queue.QueueName, RabbitMQConstants.ExchangeName, queue.RoutingKey);
			}
		}

        private void StartConsumers(CancellationToken cancellationToken)
        {
            var queues = QueueInitializer.InitializeQueues();

            foreach(var queue in queues)
            {
				var channel = _connection!.GetConnection().CreateModel();
				_channels.Add(channel);
				StartConsumer(channel, queue.QueueName,queue.RoutingKey);
			}
		}

        private void StartConsumer(IModel channel, string queueName, string routingKey)
        {
            var consumer = new AsyncEventingBasicConsumer(channel);

			consumer.Received += async (sender, ea) =>
			{
				var message = Encoding.UTF8.GetString(ea.Body.ToArray());
				try
				{
					await HandleMessageAsync(ea.RoutingKey, message);
					channel.BasicAck(ea.DeliveryTag, multiple: false);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"==> Error processing message from '{queueName}': {ex.Message}");
					channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
				}
			};

			channel.BasicConsume(queue: queueName, autoAck: false, consumer);
		}

        //Can be later moved into a separate Class
		private async Task HandleMessageAsync(string routingKey, string message)
		{
			using var scope = _scopeFactory.CreateScope();
			var service = scope.ServiceProvider.GetRequiredService<IExternalDataHandlingService>();

			switch (routingKey)
			{
				case RabbitMQConstants.AcademicQualificationRoutingKey:
					await service.AcademicDataHandle(message);
					break;
				case RabbitMQConstants.EmploymentDegreeRoutingKey:
					await service.EmploymentDataHandle(message);
					break;
				case RabbitMQConstants.ManagerialPositionRoutingKey:
				    await service.ManagerialDataHandle(message);
					break;
				case RabbitMQConstants.ContactDataRoutingKey:
				    await service.ContactDataHandle(message);
					break;
				case RabbitMQConstants.PersonalDataRoutingKey:
					await service.PersonalDataHandle(message);
					break;
				/*case RabbitMQConstants.SpecializationRoutingKey:
					//await service.SpecializationDataHandle(message);
					break;*/
				
                case RabbitMQConstants.ScientificDutyRoutingKey:
					await service.ScientificDutyDataHandle(message);
					break;
				case RabbitMQConstants.TrainingProgramRoutingKey:
					await service.TrainingProgramDataHandle(message);
					break;
				case RabbitMQConstants.ThesisSupervisionRoutingKey:
					await service.ThesisSupervisingDataHandle(message);
					break;
				case RabbitMQConstants.ThesisDataRoutingKey:
					await service.ThesisDataHandle(message);
					break;
				default:
					throw new InvalidOperationException($"Unknown routing key: {routingKey}");
			}
		}
		#endregion

	}
}
