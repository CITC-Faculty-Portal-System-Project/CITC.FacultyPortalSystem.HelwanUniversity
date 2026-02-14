using Domain.Entities.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Specifications.AcademicDataModule.ContributionsModule
{
    internal class ContributionsToCommunityServiceCountSpecifications : BaseSpecifications<ContributionsToCommunityService, int>
    {
        public ContributionsToCommunityServiceCountSpecifications(ContributionsToCommunityServiceSpecificationParameters parameters, string facultyMemberEmail)
            : base(ctcs =>
                  !ctcs.IsDeleted &&
                   ctcs.FacultyMember!.Email == facultyMemberEmail &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   ctcs.ContributionTitle.Contains(parameters.Search))
            )
        {

        }
    }
}
