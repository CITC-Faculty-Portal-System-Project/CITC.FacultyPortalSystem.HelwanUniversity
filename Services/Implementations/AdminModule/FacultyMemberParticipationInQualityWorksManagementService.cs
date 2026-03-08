using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberParticipationInQualityWorksManagementService(IParticipationInQualityWorksServiceHelper _helper)
        : IFacultyMemberParticipationInQualityWorksManagementService
    {

        public Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetFacultyMemberParticipationsInQualityWorksAsync(
            ParticipationInQualityWorksSpecificationParameters parameters,
            string facultyMemberEmail)
            => _helper.GetAllParticipationsInQualityWorksAsync(parameters, facultyMemberEmail);

        public Task<ParticipationInQualityWorksResponseDTO> GetFacultyMemberParticipationInQualityWorksByIdAsync(int id)
            => _helper.GetParticipationInQualityWorksByIdAsync(id);

        public Task<ParticipationInQualityWorksResponseDTO> CreateFacultyMemberParticipationInQualityWorksAsync(
            ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto,
            string facultyMemberEmail)
            => _helper.CreateParticipationInQualityWorksAsync(participationInQualityWorksCreateDto, facultyMemberEmail);

        public Task<ParticipationInQualityWorksResponseDTO> UpdateFacultyMemberParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto)
            => _helper.UpdateParticipationInQualityWorksAsync(participationInQualityWorksId, participationInQualityWorksUpdateDto);

        public Task DeleteFacultyMemberParticipationInQualityWorksAsync(int participationInQualityWorksId)
            => _helper.DeleteParticipationInQualityWorksAsync(participationInQualityWorksId);
    }
}
