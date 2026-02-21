using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Shared.Validations
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected readonly IStringLocalizer _localizer;

        protected BaseValidator(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create(
                baseName: "Shared.Localisation.Resources.Messages",
                location: "Shared");
        }
    }
}
