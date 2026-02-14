using Domain.Entities.AcademicDataModule.ContributionsModule;
using Shared.Enums.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Specifications.AcademicDataModule.ContributionsModule
{
    internal class ContributionsToUniversitySpecifications : BaseSpecifications<ContributionsToUniversity, int>
    {
        public ContributionsToUniversitySpecifications(ContributionsToUniversitySpecificationParameters parameters, string facultyMemberEmail)
            : base(ctu =>
                  !ctu.IsDeleted &&
                   ctu.FacultyMember!.Email == facultyMemberEmail &&
                   (parameters.TypeOfContributionIds == null || !parameters.TypeOfContributionIds.Any() ||
                   parameters.TypeOfContributionIds.Contains(ctu.TypeOfContributionId)) &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   ctu.ContributionTitle.Contains(parameters.Search))
            )
        {
            AddIncludes(ctu => ctu.TypeOfContribution);

            switch (parameters.Sort)
            {
                case ContributionsToUniversitySortingOptions.nameAsc:
                    AddOrderBy(ctu => ctu.ContributionTitle);
                    break;
                case ContributionsToUniversitySortingOptions.nameDesc:
                    AddOrderByDescending(ctu => ctu.ContributionTitle);
                    break;
                case ContributionsToUniversitySortingOptions.dateAsc:
                    AddOrderBy(ctu => ctu.DateOfContribution);
                    break;
                case ContributionsToUniversitySortingOptions.dateDesc:
                    AddOrderByDescending(ctu => ctu.DateOfContribution);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
        }
        public ContributionsToUniversitySpecifications(int id) : base(ctu => !ctu.IsDeleted && ctu.Id == id)
        {
        }
    }
}
