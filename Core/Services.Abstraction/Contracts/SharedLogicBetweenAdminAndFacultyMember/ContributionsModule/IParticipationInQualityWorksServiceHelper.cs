using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule
{
    public interface IParticipationInQualityWorksServiceHelper
    {
        Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetAllParticipationsInQualityWorksAsync(
          ParticipationInQualityWorksSpecificationParameters parameters,
          string facultyMemberEmail);

        Task<ParticipationInQualityWorksResponseDTO> GetParticipationInQualityWorksByIdAsync(int id);

        Task<ParticipationInQualityWorksResponseDTO> CreateParticipationInQualityWorksAsync(
            ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto,
            string facultyMemberEmail);

        Task<ParticipationInQualityWorksResponseDTO> UpdateParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto);

        Task DeleteParticipationInQualityWorksAsync(int participationInQualityWorksId);
    }
}
