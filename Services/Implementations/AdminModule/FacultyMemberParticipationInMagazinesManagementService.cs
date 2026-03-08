using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberParticipationInMagazinesManagementService(IParticipationInMagazinesHelper _helper)
        : IFacultyMemberParticipationInMagazinesManagementService
    {
        public Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetFacultyMemberParticipationInMagazinesAsync(
          ParticipationInMagazinesSpecificationsParameters parameters,
          string facultyMemberEmail)
          => _helper.GetAllParticipationInMagazinesAsync(parameters, facultyMemberEmail);

        public Task<ParticipationInMagazinesResponseDto> GetFacultyMemberParticipationInMagazineByIdAsync(int id)
            => _helper.GetParticipationInMagazineByIdAsync(id);

        public Task<ParticipationInMagazinesResponseDto> CreateFacultyMemberParticipationInMagazineAsync(
            ParticipationInMagazineCreateDto participationInMagazinesCreateDto,
            string facultyMemberEmail)
            => _helper.CreateParticipationInMagazineAsync(participationInMagazinesCreateDto, facultyMemberEmail);

        public Task<ParticipationInMagazinesResponseDto> UpdateFacultyMemberParticipationInMagazineAsync(
            int participationInMagazineId,
            ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto)
            => _helper.UpdateParticipationInMagazineAsync(participationInMagazineId, participationInMagazinesUpdateDto);

        public Task DeleteFacultyMemberParticipationInMagazineAsync(int participationInMagazineId)
            => _helper.DeleteParticipationInMagazineAsync(participationInMagazineId);
    }
}
