using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Dtos.IdentityModule;

namespace Shared.Validations.IdentityModule
{
    public class ResetPasswordDTOValidator : BaseValidator<ResetPasswordDto>
    {
        public ResetPasswordDTOValidator(IStringLocalizerFactory factory) : base(factory)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(_localizer["validation.Email.required"])
                .EmailAddress()
                .WithMessage(_localizer["validation.Email.invalid"]);

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(_localizer["validation.Password.required"])
                .MinimumLength(8)
                .WithMessage(_localizer["validation.Password.minLength"])
                .Matches(@"[A-Z]").WithMessage(_localizer["validation.Password.uppercase"])
                .Matches(@"[a-z]").WithMessage(_localizer["validation.Password.lowercase"])
                .Matches(@"[0-9]").WithMessage(_localizer["validation.Password.digit"])
                .Matches(@"[""!@$%^&*(){}:;<>,.?/+\-_=|'[\]~\\]").WithMessage(_localizer["validation.Password.specialChar"]);

            RuleFor(x => x.NewPasswordConifrmed)
                .NotEmpty()
                .WithMessage(_localizer["validation.PasswordConfirm.required"])
                .Equal(x => x.NewPassword)
                .WithMessage(_localizer["validation.Password.mismatch"]);
        }
    }
}