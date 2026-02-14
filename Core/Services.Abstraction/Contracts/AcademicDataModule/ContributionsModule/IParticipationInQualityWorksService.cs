using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule
{
    public interface IParticipationInQualityWorksService
    {
        public Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetAllParticipationsInQualityWorksAsync(ParticipationInQualityWorksSpecificationParameters parameters);
        public Task<ParticipationInQualityWorksResponseDTO> GetParticipationInQualityWorksByIdAsync(int id);
        public Task<ParticipationInQualityWorksResponseDTO> CreateParticipationInQualityWorksAsync(ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto);
        public Task<ParticipationInQualityWorksResponseDTO> UpdateParticipationInQualityWorksAsync(int participationInQualityWorksId, ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto);
        public Task DeleteParticipationInQualityWorksAsync(int participationInQualityWorksId);
    }
}
