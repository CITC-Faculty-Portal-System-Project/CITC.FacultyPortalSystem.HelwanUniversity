using Domain.Entities.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Specifications.AcademicDataModule.PrizesModule
{
    internal class PrizesAndRewardsCountSpecifications : BaseSpecifications<PrizesAndRewards, int>
    {
        public PrizesAndRewardsCountSpecifications(PrizesAndRewardsSpecificationParameters parameters, string facultyMemberEmail)
            : base(par =>
                  !par.IsDeleted &&
                    par.FacultyMember!.Email == facultyMemberEmail &&
                  (parameters.PrizeIds == null || !parameters.PrizeIds.Any() ||
                   parameters.PrizeIds.Contains(par.PrizeId)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   par.Prize.ValueAr.Contains(parameters.Search) ||
                   par.Prize.ValueEn.Contains(parameters.Search) ||
                   par.AwardingAuthority.Contains(parameters.Search))
            )
        {

        }
        public PrizesAndRewardsCountSpecifications(Guid facultyMemberId)
            : base(par =>
                  !par.IsDeleted &&
                    par.FacultyMemberId == facultyMemberId
            )
        {
        }
    }
}
