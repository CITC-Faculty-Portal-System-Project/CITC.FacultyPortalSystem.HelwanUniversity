using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule
{
    internal class ProjectsSpecifications : BaseSpecifications<Projects, int>
    {
        public ProjectsSpecifications(ProjectsSpecifcationsParameters parameters, string facultyMemberId)
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
            AddIncludes(p => p.ParticipationRole);
            AddIncludes(p => p.TypeOfProject);
            switch (parameters.Sort)
            {
                case ProjectsSortingOptions.NameAsc:
                    AddOrderBy(p => p.NameOfProject);
                    break;
                case ProjectsSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.NameOfProject);
                    break;
                case ProjectsSortingOptions.DateAsc:
                    AddOrderBy(p => p.StartDate);
                    break;
                case ProjectsSortingOptions.DateDesc:
                    AddOrderByDescending(p => p.StartDate);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public ProjectsSpecifications(int id) : base(p => !p.IsDeleted && p.Id == id)
        {

        }
    }
}
