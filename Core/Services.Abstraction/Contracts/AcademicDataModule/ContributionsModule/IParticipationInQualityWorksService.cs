using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule
{
    public interface IParticipationInQualityWorksService
    {
        Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetAllParticipationsInQualityWorksAsync(
         ParticipationInQualityWorksSpecificationParameters parameters,
         string? facultyMemberEmail = null);

        Task<ParticipationInQualityWorksResponseDTO> GetParticipationInQualityWorksByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ParticipationInQualityWorksResponseDTO> CreateParticipationInQualityWorksAsync(
            ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto,
            string? facultyMemberEmail = null);

        Task<ParticipationInQualityWorksResponseDTO> UpdateParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto,
            string? facultyMemberEmail = null);

        Task DeleteParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            string? facultyMemberEmail = null);
    }
}
