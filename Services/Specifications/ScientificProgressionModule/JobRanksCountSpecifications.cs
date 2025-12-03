using Domain.Entities.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Specifications.ScientificProgressionModule
{
    internal class JobRanksCountSpecifications : BaseSpecifications<JobRanks, int>
    {
        public JobRanksCountSpecifications(JobRanksSpecificationsParameters parameters, string facultyMemberEmail)
            : base(jr =>
                  (!jr.IsDeleted &&
                   jr.FacultyMember!.Email == facultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   jr.JobRank.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   jr.JobRank.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
