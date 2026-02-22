using FluentValidation;
using Microsoft.Extensions.Localization;
using Shared.Dtos.FacultyMemberDataModule;

namespace Shared.Validations.FacultyMemberDataModule
{
    public class PersonalDataUpdateDTOValidator : BaseValidator<PersonalDataUpdateDto>   
    {
        public PersonalDataUpdateDTOValidator(IStringLocalizerFactory factory) : base(factory)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_localizer["validation.PersonalData.Name.required"])
                .MaximumLength(50)
                .WithMessage(_localizer["validation.PersonalData.Name.maxLength"]);

            RuleFor(x => x.BirthDate)
                .Must(date => !date.HasValue|| date < DateOnly.FromDateTime(DateTime.Now.AddYears(-20)))
                .WithMessage(_localizer["validation.PersonalData.BirthDate.invalid"]);

            RuleFor(x => x.TitleId)
                .NotEmpty()
                .WithMessage(_localizer["validation.PersonalData.Title.required"]);

            RuleFor(x => x.UniversityId)
                .NotEmpty()
                .WithMessage(_localizer["validation.PersonalData.University.required"]);

            RuleFor(x => x.DepartmentId)
                .NotEmpty()
                .WithMessage(_localizer["validation.PersonalData.Department.required"]);

            RuleFor(x => x.AuthorityId)
                .NotEmpty()
                .WithMessage(_localizer["validation.PersonalData.Authority.required"]);

            RuleFor(x => x.FieldId)
                .NotEmpty()
                .WithMessage(_localizer["validation.PersonalData.Field.required"]);

            RuleFor(x => x.MaritalStatusId)
                .NotEmpty()
                .WithMessage(_localizer["validation.PersonalData.MaritalStatus.required"]);

            RuleFor(x => x.BirthPlace)
                .MaximumLength(50)
                .WithMessage(_localizer["validation.PersonalData.BirthPlace.maxLength"]);

            RuleFor(x => x.NameInComposition)
                .NotEmpty()
                .WithMessage(_localizer["validation.PersonalData.NameInComposition.required"])
                .MaximumLength(50)
                .WithMessage(_localizer["validation.PersonalData.Name.maxLength"]);

            RuleFor(x => x.GeneralSpecialization)
                .MaximumLength(250)
                .WithMessage(_localizer["validation.PersonalData.GeneralSpecialization.maxLength"]);

            RuleFor(x => x.AccurateSpecialization)
                .MaximumLength(250)
                .WithMessage(_localizer["validation.PersonalData.AccurateSpecialization.maxLength"]);

            RuleFor(x => x.CompositionTopics)
                .MaximumLength(500)
                .WithMessage(_localizer["validation.PersonalData.CompositionTopics.maxLength"]);
        }
    }
}
