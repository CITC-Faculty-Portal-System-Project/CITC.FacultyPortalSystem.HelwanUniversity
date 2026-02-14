using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule
{
    internal class ProjectsCountSpecifications : BaseSpecifications<Projects, int>
    {
        public ProjectsCountSpecifications(ProjectsSpecifcationsParameters parameters, string facultyMemberId)
            : base(p =>
                  !p.IsDeleted &&
                    p.FacultyMember!.Email == facultyMemberId &&
                  (parameters.LocalOrInternationals == null || !parameters.LocalOrInternationals.Any() ||
                   parameters.LocalOrInternationals.Select(l => (Domain.Enums.LocalOrInternational)l)
                   .Contains(p.LocalOrInternational)) &&
                  (parameters.ParticipationRoleIds == null || !parameters.ParticipationRoleIds.Any() ||
                   parameters.ParticipationRoleIds.Contains(p.ParticipationRoleId)) &&
                  (parameters.TypeOfProjectIds == null || !parameters.TypeOfProjectIds.Any() ||
                   parameters.TypeOfProjectIds.Contains(p.TypeOfProjectId)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   p.NameOfProject.Contains(parameters.Search) ||
                   p.FinancingAuthority.Contains(parameters.Search))
            )
        {

        }
    }
}
