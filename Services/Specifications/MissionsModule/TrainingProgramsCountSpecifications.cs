using Domain.Entities.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Specifications.MissionsModule
{
    internal class TrainingProgramsCountSpecifications : BaseSpecifications<TrainingPrograms, int>
    {
        public TrainingProgramsCountSpecifications(TrainingProgramsSpecificationParameters parameters, string facultyMemberEmail)
            : base(tp =>
                  (!tp.IsDeleted &&
                    tp.FacultyMember!.Email == facultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   tp.TrainingProgramName.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   tp.OrganizingAuthority.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   tp.Venue.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
