using Domain.Entities.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Specifications.AcademicDataModule.MissionsModule
{
    internal class TrainingProgramsCountSpecifications : BaseSpecifications<TrainingPrograms, int>
    {
        public TrainingProgramsCountSpecifications(TrainingProgramsSpecificationParameters parameters, string facultyMemberEmail)
            : base(tp =>
                  !tp.IsDeleted &&
                    tp.FacultyMember!.Email == facultyMemberEmail &&
                  (parameters.TrainingProgramTypes == null || !parameters.TrainingProgramTypes.Any() ||
                   parameters.TrainingProgramTypes.Select(t => (Domain.Enums.TrainingProgramType)t)
                   .Contains(tp.Type)) &&
                  (parameters.TrainingProgramParticipationTypes == null || !parameters.TrainingProgramParticipationTypes.Any() ||
                   parameters.TrainingProgramParticipationTypes.Select(t => (Domain.Enums.TrainingProgramParticipationType)t)
                   .Contains(tp.ParticipationType)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   tp.TrainingProgramName.Contains(parameters.Search) ||
                   tp.OrganizingAuthority.Contains(parameters.Search) ||
                   tp.Venue.Contains(parameters.Search))
            )
        {

        }
    }
}
