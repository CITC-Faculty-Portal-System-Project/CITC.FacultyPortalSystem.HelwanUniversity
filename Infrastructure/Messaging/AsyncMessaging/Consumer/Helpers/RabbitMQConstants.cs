using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.AsyncMessaging.Consumer.Helpers
{
	//This class holds all RabbitMQ related constants such as exchange names, queue names, and routing keys.
	public static class RabbitMQConstants
	{
		public const string ExchangeName = "data_exchange";

		//Academic Qualification Queue :
		public const string AcademicQualificationQueue = "AcademicQualification-queue";
		public const string AcademicQualificationRoutingKey = "academicQualificationsRK";

		//Employment Degree Queue :
		public const string EmploymentDegreeQueue = "employmentDegree-queue";
		public const string EmploymentDegreeRoutingKey = "employmentDegreeRK";

		//Managerial Position Queue : 
		public const string ManagerialPositionQueue = "ManagerialPosition-queue";
		public const string ManagerialPositionRoutingKey = "managerialPositionsRK";

		//Contact Data Queue :
		public const string ContactDataQueue = "ContactData-queue";
		public const string ContactDataRoutingKey = "contactDataRK";

		//Personal Data Queue :
		public const string PersonalDataQueue = "PersonalData-queue";
		public const string PersonalDataRoutingKey = "personalDataRK";

		/*//Specialization Queue :
		public const string SpecializationQueue = "SpecializationData-queue";
		public const string SpecializationRoutingKey = "specializationDataRK";*/

		//Scientific Duty Queue :
		public const string ScientificDutyQueue = "ScientificDutyData-queue";
		public const string ScientificDutyRoutingKey = "scientificDutyDataRK";

		//Training Program Queue :
		public const string TrainingProgramQueue = "TrainingProgramData-queue";
		public const string TrainingProgramRoutingKey = "trainingProgramDataRK";

		//Thesis Supervision Queue :
		public const string ThesisSupervisionQueue = "ThesisSupervisingData-queue";
		public const string ThesisSupervisionRoutingKey = "thesisSupervisingDataRK";

		//Thesis Data Queue :
		public const string ThesisDataQueue = "ThesisData-queue";
		public const string ThesisDataRoutingKey = "thesisDataRK";
	}
}
