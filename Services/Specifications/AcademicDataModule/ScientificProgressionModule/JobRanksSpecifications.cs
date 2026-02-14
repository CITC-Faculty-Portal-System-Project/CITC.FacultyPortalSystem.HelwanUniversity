using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Enums.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Specifications.AcademicDataModule.ScientificProgressionModule
{
    internal class JobRanksSpecifications : BaseSpecifications<JobRanks, int>
    {
        public JobRanksSpecifications(JobRanksSpecificationsParameters parameters, string facultyMemberEmail) 
            : base(jr => 
                  !jr.IsDeleted && 
                   jr.FacultyMember!.Email == facultyMemberEmail &&
                  (parameters.JobRankIds == null || !parameters.JobRankIds.Any() ||
                   parameters.JobRankIds.Contains(jr.JobRankId)) &&
                  (string.IsNullOrEmpty(parameters.Search) || 
                   jr.JobRank.ValueAr.Contains(parameters.Search) ||
                   jr.JobRank.ValueEn.Contains(parameters.Search))
            )
        {
            AddIncludes(jr => jr.JobRank);
            switch (parameters.Sort)
            {
                case JobRanksSortingOptions.DateAsc:
                    AddOrderBy(jr => jr.DateOfJobRank);
                    break;
                case JobRanksSortingOptions.DateDesc:
                    AddOrderByDescending(jr => jr.DateOfJobRank);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public JobRanksSpecifications(int id) : base(jr => !jr.IsDeleted && jr.Id == id)
        {
            AddIncludes(jr => jr.JobRank);
        }

        public JobRanksSpecifications(JobRanksFetchingDTO dTO)
            : base(jr => jr.DateOfJobRank == dTO.PromotionDate && 
                  jr.JobRank.ValueAr == dTO.Name || jr.JobRank.ValueEn == dTO.Name && 
                  jr.FacultyMember.NationalNumber == dTO.NationalNumber)
        {

            AddIncludes(jr => jr.JobRank);
            AddIncludes(jr => jr.FacultyMember);
        }


    }
}
