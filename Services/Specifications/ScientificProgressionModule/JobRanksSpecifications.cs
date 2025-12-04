using Domain.Entities.ScientificProgressionModule;
using Shared.Enums.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;
namespace Services.Specifications.ScientificProgressionModule
{
    internal class JobRanksSpecifications : BaseSpecifications<JobRanks, int>
    {
        public JobRanksSpecifications(JobRanksSpecificationsParameters parameters, string facultyMemberEmail) 
            : base(jr => 
                  (!jr.IsDeleted && 
                   jr.FacultyMember!.Email == facultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) || 
                   jr.JobRank.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   jr.JobRank.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
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
    }
}
