namespace Shared.Enums.Logging
{
	public enum CategoryAction
	{
		CheckNationalNumber = 1,
		UserRegistration = 2,
		SendEmail = 3,
		UserLogin = 4,
		SendCredentialsByEmail = 5,
		SendOTP = 6,
		ResetPassword = 7,
		Initialize = 8,
		GetConnection = 9,
		Dispose = 10,
		DeclareQueueAndExchange = 11,
		PublishMessage = 12,
		ConsumeMessages = 13,
		MessageHandling = 14,
		BackgroundExecution = 15,
		StopBackgroundExecution = 16,
	}
}
