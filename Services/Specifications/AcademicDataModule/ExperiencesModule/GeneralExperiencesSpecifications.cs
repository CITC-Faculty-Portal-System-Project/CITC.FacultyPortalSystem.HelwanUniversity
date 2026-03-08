using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Shared.Enums.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Specifications.AcademicDataModule.ExperiencesModule
{
    internal class GeneralExperiencesSpecifications : BaseSpecifications<GeneralExperiences, int>
    {
        public GeneralExperiencesSpecifications(GeneralExperiencesSpecificationParameters parameters, string facultyMemberEmail)
            : base(ge =>
                  !ge.IsDeleted &&
                    ge.FacultyMember!.Email == facultyMemberEmail &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ge.ExperienceTitle.Contains(parameters.Search) ||
                   ge.Authority.Contains(parameters.Search) ||
                   ge.CountryOrCity.Contains(parameters.Search))
            )
        {
            switch (parameters.Sort)
            {
                case GeneralExperiencesSortingOptions.StartDateAsc:
                    AddOrderBy(ge => ge.StartDate);
                    break;
                case GeneralExperiencesSortingOptions.StartDateDesc:
                    AddOrderByDescending(ge => ge.StartDate);
                    break;
                case GeneralExperiencesSortingOptions.NameAsc:
                    AddOrderBy(ge => ge.ExperienceTitle);
                    break;
                case GeneralExperiencesSortingOptions.NameDesc:
                    AddOrderByDescending(ge => ge.ExperienceTitle);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
        }
        public GeneralExperiencesSpecifications(int id) : base(aq => !aq.IsDeleted && aq.Id == id)
        {

        }

        public GeneralExperiencesSpecifications(Guid facultyMemberId) : base(ge => !ge.IsDeleted && ge.FacultyMemberId == facultyMemberId)
        {

        }
    }
}
