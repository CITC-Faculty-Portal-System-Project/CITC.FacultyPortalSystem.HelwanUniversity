namespace Services.Abstraction.Contracts
{
	public interface INationalNumberPubClient
	{
		public Task PublishUserNationalNumberAsync(string nationalNumber);
	}
}
