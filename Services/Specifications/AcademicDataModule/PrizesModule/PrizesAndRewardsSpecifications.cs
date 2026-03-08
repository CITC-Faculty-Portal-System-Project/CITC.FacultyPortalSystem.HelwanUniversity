using Domain.Entities.AcademicDataModule.PrizesModule;
using Shared.Enums.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Specifications.AcademicDataModule.PrizesModule
{
    internal class PrizesAndRewardsSpecifications : BaseSpecifications<PrizesAndRewards, int>
    {
        public PrizesAndRewardsSpecifications(PrizesAndRewardsSpecificationParameters parameters, string facultyMemberEmail)
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
            AddIncludes(par => par.Prize);
            AddIncludes(par => par.Attachments!);

            switch (parameters.Sort)
            {
                case PrizesAndRewardsSortingOptions.DateAsc:
                    AddOrderBy(par => par.DateReceived);
                    break;
                case PrizesAndRewardsSortingOptions.DateDesc:
                    AddOrderByDescending(par => par.DateReceived);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public PrizesAndRewardsSpecifications(int id) : base(par => !par.IsDeleted && par.Id == id)
        {
            AddIncludes(par => par.Prize);
            AddIncludes(par => par.Attachments!);
        }
    }
}
