using Domain.Entities.AcademicDataModule.ContributionsModule;
using Shared.Enums.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Specifications.AcademicDataModule.ContributionsModule
{
    internal class ContributionsToCommunityServiceSpecifications : BaseSpecifications<ContributionsToCommunityService, int>
    {
        public ContributionsToCommunityServiceSpecifications(ContributionsToCommunityServiceSpecificationParameters parameters, string facultyMemberEmail)
            : base(ctcs =>
                  !ctcs.IsDeleted &&
                   ctcs.FacultyMember!.Email == facultyMemberEmail &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   ctcs.ContributionTitle.Contains(parameters.Search))
            )
        {
            switch (parameters.Sort)
            {
                case ContributionsToCommunityServiceSortingOptions.nameAsc:
                    AddOrderBy(ctcs => ctcs.ContributionTitle);
                    break;
                case ContributionsToCommunityServiceSortingOptions.nameDesc:
                    AddOrderByDescending(ctcs => ctcs.ContributionTitle);
                    break;
                case ContributionsToCommunityServiceSortingOptions.dateAsc:
                    AddOrderBy(ctcs => ctcs.DateOfContribution);
                    break;
                case ContributionsToCommunityServiceSortingOptions.dateDesc:
                    AddOrderByDescending(ctcs => ctcs.DateOfContribution);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
        }

        public ContributionsToCommunityServiceSpecifications(int id) : base(ctcs => !ctcs.IsDeleted && ctcs.Id == id)
        {
        }
    }
}
