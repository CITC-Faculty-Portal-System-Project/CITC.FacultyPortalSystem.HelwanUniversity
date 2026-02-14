using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Shared.Enums.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Specifications.AcademicDataModule.ExperiencesModule
{
    internal class TeachingExperiencesSpecifications : BaseSpecifications<TeachingExperiences, int>
    {
        public TeachingExperiencesSpecifications(TeachingExperiencesSpecificationParameters parameters, string facultyMemberEmail)
            : base(te =>
                  !te.IsDeleted &&
                    te.FacultyMember!.Email == facultyMemberEmail &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   te.CourseName.Contains(parameters.Search) ||
                   (te.AcademicLevel != null && te.AcademicLevel.Contains(parameters.Search)) ||
                   (te.UniversityOrFaculty != null && te.UniversityOrFaculty.Contains(parameters.Search)))
            )
        {
            switch (parameters.Sort)
            {
                case TeachingExperiencesSortingOptions.StartDateAsc:
                    AddOrderBy(te => te.StartDate);
                    break;
                case TeachingExperiencesSortingOptions.StartDateDesc:
                    AddOrderByDescending(te => te.StartDate);
                    break;
                case TeachingExperiencesSortingOptions.NameAsc:
                    AddOrderBy(te => te.CourseName);
                    break;
                case TeachingExperiencesSortingOptions.NameDesc:
                    AddOrderByDescending(te => te.CourseName);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
        }
        public TeachingExperiencesSpecifications(int id) : base(te => !te.IsDeleted && te.Id == id)
        {

        }
    }
}
