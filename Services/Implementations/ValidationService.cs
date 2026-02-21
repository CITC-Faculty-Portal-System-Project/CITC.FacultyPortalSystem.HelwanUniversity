using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using ValidationException = Domain.Exceptions.ValidationException;

namespace Services.Implementations
{
    public sealed class ValidationService(IServiceProvider _provider) : IValidationService
    {
        public async Task ValidateAsync<T>(T dto)
        {
            var validator = _provider.GetService<IValidator<T>>();

            if (validator is null)
                return;

            ValidationResult result = await validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .Select(g => new Domain.Models.ValidationError 
                    {
                        Field = g.Key,
                        Errors = g.Select(x => x.ErrorMessage).ToList()
                    }).ToList();

                throw new ValidationException(errors);
            }
        }
    }
}
