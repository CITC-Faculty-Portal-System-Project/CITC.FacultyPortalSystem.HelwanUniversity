using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberParticipationInQualityWorksManagementService
    {
        Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetFacultyMemberParticipationsInQualityWorksAsync(
          ParticipationInQualityWorksSpecificationParameters parameters,
          string facultyMemberEmail);

        Task<ParticipationInQualityWorksResponseDTO> GetFacultyMemberParticipationInQualityWorksByIdAsync(int id);

        Task<ParticipationInQualityWorksResponseDTO> CreateFacultyMemberParticipationInQualityWorksAsync(
            ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto,
            string facultyMemberEmail);

        Task<ParticipationInQualityWorksResponseDTO> UpdateFacultyMemberParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto);

        Task DeleteFacultyMemberParticipationInQualityWorksAsync(int participationInQualityWorksId);
    }
}
