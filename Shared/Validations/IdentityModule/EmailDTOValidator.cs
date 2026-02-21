using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Dtos.Auth;

namespace Shared.Validations.IdentityModule
{
    public class EmailDTOValidator : BaseValidator<EmailDTO>
    {
        public EmailDTOValidator(IStringLocalizerFactory factory) : base(factory)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(_localizer["validation.Email.required"])
                .EmailAddress()
                .WithMessage(_localizer["validation.Email.invalid"]);
        }
    }
}
