using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Specifications.ProjectsAndCommitteesModule
{
    internal class ProjectsSpecifications : BaseSpecifications<Projects, int>
    {
        public ProjectsSpecifications(ProjectsSpecifcationsParameters parameters, string facultyMemberId)
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
