using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Dtos.IdentityModule;

namespace Shared.Validations.IdentityModule
{
    public class OTPSendDTOValidator : BaseValidator<OTPSendDTO>
    {
        public OTPSendDTOValidator(IStringLocalizerFactory factory) : base(factory)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(_localizer["validation.Email.required"])
                .EmailAddress()
                .WithMessage(_localizer["validation.Email.invalid"]);

            RuleFor(x => x.Otp)
                .NotEmpty()
                .WithMessage(_localizer["validation.OTP.required"])
                .Length(6)
                .WithMessage(_localizer["validation.OTP.length"])
                .Matches(@"^[0-9]+$")
                .WithMessage(_localizer["validation.OTP.digitsOnly"]);
        }
    }
}
