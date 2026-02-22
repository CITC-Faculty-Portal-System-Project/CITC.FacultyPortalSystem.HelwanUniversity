using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Dtos.FacultyMemberDataModule;

namespace Shared.Validations.FacultyMemberDataModule
{
    public class SocialMediaPlatformsDTOValidator : BaseValidator<SocialMediaPlatformsDto>
    {
        public SocialMediaPlatformsDTOValidator(IStringLocalizerFactory factory) : base(factory)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Scopus)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);

            RuleFor(x => x.YouTube)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);

            RuleFor(x => x.LinkedIn)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);

            RuleFor(x => x.Instagram)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);

            RuleFor(x => x.PersonalWebsite)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);

            RuleFor(x => x.Facebook)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);

            RuleFor(x => x.X)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(_localizer["validation.Url.invalid"]);
        }
    }
}
