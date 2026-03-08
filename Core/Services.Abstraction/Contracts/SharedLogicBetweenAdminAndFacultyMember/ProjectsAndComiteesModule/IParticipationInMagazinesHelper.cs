using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule
{
    public interface IParticipationInMagazinesHelper
    {
        Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(
            ParticipationInMagazinesSpecificationsParameters parameters,
            string facultyMemberEmail);

        Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(int id);

        Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(
            ParticipationInMagazineCreateDto participationInMagazinesCreateDto,
            string facultyMemberEmail);

        Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(
            int participationInMagazineId,
            ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto);

        Task DeleteParticipationInMagazineAsync(int participationInMagazineId);
    }
}
