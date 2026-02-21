using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Dtos.IdentityModule;

namespace Shared.Validations.IdentityModule
{
    public class RegisterDTOValidator : BaseValidator<RegisterDto>
    {
        public RegisterDTOValidator(IStringLocalizerFactory factory) : base(factory)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.NationalNumber)
                .NotEmpty()
                .WithMessage(_localizer["validation.NationalNumber.required"])
                .Matches(@"^([23])([0-9]{13})$")
                .WithMessage(_localizer["validation.NationalNumber.invalid"]);
        }
    }
}
