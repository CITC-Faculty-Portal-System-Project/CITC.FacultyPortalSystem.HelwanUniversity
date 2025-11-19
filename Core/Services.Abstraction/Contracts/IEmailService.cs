namespace Services.Abstraction.Contracts
{
    public interface IEmailService
    {
        public Task SendCredentialsAsync(Guid userId, string userName, string password);
        public Task SendOTPAsync(string email);
    }
}
