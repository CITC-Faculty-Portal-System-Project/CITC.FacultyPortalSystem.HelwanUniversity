using Domain.Entities.AcademicDataModule.MissionsModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Enums.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Specifications.AcademicDataModule.MissionsModule
{
    internal class TrainingProgramsSpecifications : BaseSpecifications<TrainingPrograms, int>
    {
        public TrainingProgramsSpecifications(TrainingProgramsSpecificationParameters parameters, string facultyMemberEmail) 
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

            switch (parameters.Sort)
            {
                case TrainingProgramsSortingOptions.NameAsc:
                    AddOrderBy(tp => tp.TrainingProgramName);
                    break;
                case TrainingProgramsSortingOptions.NameDesc:
                    AddOrderByDescending(tp => tp.TrainingProgramName);
                    break;
                case TrainingProgramsSortingOptions.DateAsc:
                    AddOrderBy(tp => tp.StartDate);
                    break;
                case TrainingProgramsSortingOptions.DateDesc:
                    AddOrderByDescending(tp => tp.StartDate);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public TrainingProgramsSpecifications(int id) : base(tp => !tp.IsDeleted && tp.Id == id)
        {

        }

        public TrainingProgramsSpecifications(TrainingProgramsFetchingDTO trainingProgramsFetchingDTO) 
            : base(tp => tp.StartDate == trainingProgramsFetchingDTO.StartDate && tp.EndDate == trainingProgramsFetchingDTO.EndDate
            && tp.TrainingProgramName == trainingProgramsFetchingDTO.Name && tp.Description == trainingProgramsFetchingDTO.Description
            && tp.OrganizingAuthority == trainingProgramsFetchingDTO.OrganizerName && tp.Venue == trainingProgramsFetchingDTO.ProgramPlace
            && tp.FacultyMember.NationalNumber == trainingProgramsFetchingDTO.NationalNumber)
        {
            AddIncludes(tp => tp.FacultyMember);
        }
    }
}
