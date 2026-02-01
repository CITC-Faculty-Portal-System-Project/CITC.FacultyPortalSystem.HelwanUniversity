using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule
{
    public interface IParticipationInMagazinesService
    {
        public Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(ParticipationInMagazinesSpecificationsParameters parameters);
        public Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(int id);
        public Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(ParticipationInMagazineCreateDto participationInMagazinesCreateDto);
        public Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(int participationInMagazineId, ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto);
        public Task DeleteParticipationInMagazineAsync(int participationInMagazineId);
    }
}
