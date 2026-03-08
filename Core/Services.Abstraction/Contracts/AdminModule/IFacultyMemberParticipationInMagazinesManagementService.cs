using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberParticipationInMagazinesManagementService
    {
        Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetFacultyMemberParticipationInMagazinesAsync(
            ParticipationInMagazinesSpecificationsParameters parameters,
            string facultyMemberEmail);

        Task<ParticipationInMagazinesResponseDto> GetFacultyMemberParticipationInMagazineByIdAsync(int id);

        Task<ParticipationInMagazinesResponseDto> CreateFacultyMemberParticipationInMagazineAsync(
            ParticipationInMagazineCreateDto participationInMagazinesCreateDto,
            string facultyMemberEmail);

        Task<ParticipationInMagazinesResponseDto> UpdateFacultyMemberParticipationInMagazineAsync(
            int participationInMagazineId,
            ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto);

        Task DeleteFacultyMemberParticipationInMagazineAsync(int participationInMagazineId);
    }
}
