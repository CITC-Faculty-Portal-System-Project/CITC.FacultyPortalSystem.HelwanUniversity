using Domain.Entities.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Specifications.MissionsModule
{
    internal class TrainingProgramsCountSpecifications : BaseSpecifications<TrainingPrograms, int>
    {
        public TrainingProgramsCountSpecifications(TrainingProgramsSpecificationParameters parameters)
            : base(tp =>
                  (!tp.IsDeleted &&
                    tp.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   tp.TrainingProgramName.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   tp.OrganizingAuthority.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   tp.Venue.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
