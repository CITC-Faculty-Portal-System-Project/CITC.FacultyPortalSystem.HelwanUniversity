using Domain.Entities.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Specifications.AcademicDataModule.ContributionsModule
{
    internal class ContributionsToUniversityCountSpecifications : BaseSpecifications<ContributionsToUniversity, int>
    {
        public ContributionsToUniversityCountSpecifications(ContributionsToUniversitySpecificationParameters parameters, string facultyMemberEmail)
            : base(ctu =>
                  !ctu.IsDeleted &&
                   ctu.FacultyMember!.Email == facultyMemberEmail &&
                   (parameters.TypeOfContributionIds == null || !parameters.TypeOfContributionIds.Any() ||
                   parameters.TypeOfContributionIds.Contains(ctu.TypeOfContributionId)) &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   ctu.ContributionTitle.Contains(parameters.Search))
            )
        {
        }

        public ContributionsToUniversityCountSpecifications(Guid facultyMemberId)
            : base(ctu =>
                  !ctu.IsDeleted &&
                   ctu.FacultyMemberId == facultyMemberId
            )
        {
        }
    }
}
