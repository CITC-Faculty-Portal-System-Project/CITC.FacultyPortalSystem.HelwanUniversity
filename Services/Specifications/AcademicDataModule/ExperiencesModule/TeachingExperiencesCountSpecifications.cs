using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Specifications.AcademicDataModule.ExperiencesModule
{
    internal class TeachingExperiencesCountSpecifications : BaseSpecifications<TeachingExperiences, int>
    {
        public TeachingExperiencesCountSpecifications(TeachingExperiencesSpecificationParameters parameters, string facultyMemberEmail)
            : base(te =>
                  !te.IsDeleted &&
                    te.FacultyMember!.Email == facultyMemberEmail &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   te.CourseName.Contains(parameters.Search) ||
                   (te.AcademicLevel != null && te.AcademicLevel.Contains(parameters.Search)) ||
                   (te.UniversityOrFaculty != null && te.UniversityOrFaculty.Contains(parameters.Search)))
            )
        {

        }
    }
}
