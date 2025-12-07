using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.AsyncMessaging.Consumer.Helpers
{
	public class QueueInitializer
	{
		//Returns the list of Queues to be initialized and Consumed from.
		public static List<QueueArgs> InitializeQueues()
		{
			return new List<QueueArgs>
			{
				new QueueArgs(RabbitMQConstants.AcademicQualificationQueue,RabbitMQConstants.AcademicQualificationRoutingKey),
				new QueueArgs(RabbitMQConstants.EmploymentDegreeQueue,RabbitMQConstants.EmploymentDegreeRoutingKey),
				new QueueArgs(RabbitMQConstants.ManagerialPositionQueue,RabbitMQConstants.ManagerialPositionRoutingKey),
				new QueueArgs(RabbitMQConstants.ContactDataQueue,RabbitMQConstants.ContactDataRoutingKey),
				new QueueArgs(RabbitMQConstants.PersonalDataQueue,RabbitMQConstants.PersonalDataRoutingKey),
				new QueueArgs(RabbitMQConstants.SpecializationQueue,RabbitMQConstants.SpecializationRoutingKey),
				new QueueArgs(RabbitMQConstants.ScientificDutyQueue,RabbitMQConstants.ScientificDutyRoutingKey),
				new QueueArgs(RabbitMQConstants.TrainingProgramQueue,RabbitMQConstants.TrainingProgramRoutingKey),
				new QueueArgs(RabbitMQConstants.ThesisSupervisionQueue,RabbitMQConstants.ThesisSupervisionRoutingKey),
				new QueueArgs(RabbitMQConstants.ThesisDataQueue,RabbitMQConstants.ThesisDataRoutingKey)
			};
		}
	}
}
