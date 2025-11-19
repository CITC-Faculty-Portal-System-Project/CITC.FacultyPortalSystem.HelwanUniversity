using Shared.Dtos.IdentityModule;

namespace Services.Abstraction.Contracts
{
    public interface IRegistrationClientService
    {
        Task<UserRegistrationClientDto?> CheckNationalNumber(string nationalNumber);
    }
}
