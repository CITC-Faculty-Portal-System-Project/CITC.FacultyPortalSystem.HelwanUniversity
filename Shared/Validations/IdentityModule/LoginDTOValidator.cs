using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Dtos.IdentityModule;

namespace Shared.Validations.IdentityModule
{
    public class LoginDTOValidator : BaseValidator<LoginDto>
    {
        public LoginDTOValidator(IStringLocalizerFactory factory) : base(factory)
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage(_localizer["validation.Login.Username.required"]);
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(_localizer["validation.Password.required"]);
        }
    }
}
