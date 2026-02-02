using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Specifications.ProjectsAndCommitteesModule
{
    internal class ProjectsCountSpecifications : BaseSpecifications<Projects, int>
    {
        public ProjectsCountSpecifications(ProjectsSpecifcationsParameters parameters, string facultyMemberId)
            : base(p =>
                  (!p.IsDeleted &&
                    p.FacultyMember!.Email == facultyMemberId) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   p.NameOfProject.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   p.TypeOfProject.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   p.TypeOfProject.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   p.ParticipationRole.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   p.ParticipationRole.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   p.FinancingAuthority.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
