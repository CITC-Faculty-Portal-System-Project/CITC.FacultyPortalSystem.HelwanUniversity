using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Specifications.AcademicDataModule.ExperiencesModule
{
    internal class GeneralExperiencesCountSpecifications : BaseSpecifications<GeneralExperiences, int>
    {
        public GeneralExperiencesCountSpecifications(GeneralExperiencesSpecificationParameters parameters, string facultyMemberEmail)
            : base(ge =>
                  !ge.IsDeleted &&
                    ge.FacultyMember!.Email == facultyMemberEmail &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ge.ExperienceTitle.Contains(parameters.Search) ||
                   ge.Authority.Contains(parameters.Search) ||
                   ge.CountryOrCity.Contains(parameters.Search))
            )
        {

        }
    }
}
