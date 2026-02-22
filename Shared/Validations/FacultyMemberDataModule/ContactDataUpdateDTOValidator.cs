using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Dtos.FacultyMemberDataModule;

namespace Shared.Validations.FacultyMemberDataModule
{
    public class ContactDataUpdateDTOValidator :  BaseValidator<ContactDataUpdateDto>
    {
        public ContactDataUpdateDTOValidator(IStringLocalizerFactory factory) : base(factory)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.WorkPhoneNumber)
                .MaximumLength(20)
                .WithMessage(_localizer["validation.ContactData.WorkPhoneNumber.maxLength"])
                .Matches(@"^[0-9+ ]*$")
                .WithMessage(_localizer["validation.ContactData.Phone.invalid"]);

            RuleFor(x => x.HomePhoneNumber)
                .MaximumLength(20)
                .WithMessage(_localizer["validation.ContactData.HomePhoneNumber.maxLength"])
                .Matches(@"^[0-9+ ]*$")
                .WithMessage(_localizer["validation.ContactData.Phone.invalid"]);

            RuleFor(x => x.PersonalEmail)
                .EmailAddress()
                .WithMessage(_localizer["validation.Email.invalid"])
                .MaximumLength(150)
                .WithMessage(_localizer["validation.ContactData.Email.maxLength"]);

            RuleFor(x => x.AlternativeEmail)
                .EmailAddress()
                .WithMessage(_localizer["validation.Email.invalid"])
                .MaximumLength(150)
                .WithMessage(_localizer["validation.ContactData.Email.maxLength"]);

            RuleFor(x => x.FaxNumber)
                .MaximumLength(20)
                .WithMessage(_localizer["validation.ContactData.FaxNumber.maxLength"]);

            RuleFor(x => x.Address)
                .MaximumLength(75)
                .WithMessage(_localizer["validation.ContactData.Address.maxLength"]);
        }
    }
}
