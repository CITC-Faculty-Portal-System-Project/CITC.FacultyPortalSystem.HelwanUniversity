using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Specifications.AcademicDataModule.ScientificProgressionModule
{
    internal class JobRanksCountSpecifications : BaseSpecifications<JobRanks, int>
    {
        public JobRanksCountSpecifications(JobRanksSpecificationsParameters parameters, string facultyMemberEmail)
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

        }
    }
}
