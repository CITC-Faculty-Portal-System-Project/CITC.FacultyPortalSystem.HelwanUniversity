using FluentValidation;
using FluentValidation.Results;

namespace Services.Abstraction.Contracts
{
    public interface IValidationService
    {
        Task ValidateAsync<T>(T instance);
    }
}
