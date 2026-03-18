using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule
{
    public interface IParticipationInMagazinesService
    {
        Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(
         ParticipationInMagazinesSpecificationsParameters parameters,
         string? facultyMemberEmail = null);

        Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(
            ParticipationInMagazineCreateDto dto,
            string? facultyMemberEmail = null);

        Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(
            int id,
            ParticipationInMagazineUpdateDto dto,
            string? facultyMemberEmail = null);

        Task DeleteParticipationInMagazineAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
