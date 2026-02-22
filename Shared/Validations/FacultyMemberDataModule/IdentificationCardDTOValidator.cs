using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Dtos.FacultyMemberDataModule;

namespace Shared.Validations.FacultyMemberDataModule
{
    public class IdentificationCardDTOValidator : BaseValidator<IdentificationCardDto>
    {
        public IdentificationCardDTOValidator(IStringLocalizerFactory factory) : base(factory)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.EKB)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);

            RuleFor(x => x.ResearcherGate)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);

            RuleFor(x => x.AcademiaEdu)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);

            RuleFor(x => x.ResearcherId)
                .MaximumLength(100)
                .WithMessage(_localizer["validation.IdentificationCard.ResearcherId.maxLength"]);
        }
    }
}
