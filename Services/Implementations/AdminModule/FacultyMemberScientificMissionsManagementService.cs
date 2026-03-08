using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberScientificMissionsManagementService(IScientificMissionsHelper _helper)
        :IFacultyMemberScientificMissionsManagementService
    {

        public Task<PaginatedResult<ScientificMissionResponseDto?>> GetFacultyMemberScientificMissionsAsync(
            ScientificMissionSpecificationParamaters parameters,
            string facultyMemberEmail)
            => _helper.GetAllScientificMissionsAsync(parameters, facultyMemberEmail);

        public Task<ScientificMissionResponseDto?> GetFacultyMemberScientificMissionByIdAsync(int id)
            => _helper.GetScientificMissionByIdAsync(id);

        public Task<ScientificMissionResponseDto> CreateFacultyMemberScientificMissionAsync(
            ScientificMissionCreateDto scientificMissionCreateDto,
            string facultyMemberEmail)
            => _helper.CreateScientificMissionAsync(scientificMissionCreateDto, facultyMemberEmail);

        public Task<ScientificMissionResponseDto> UpdateFacultyMemberScientificMissionAsync(
            int id,
            ScientificMissionUpdateDto scientificMissionUpdateDto)
            => _helper.UpdateScientificMissionAsync(id, scientificMissionUpdateDto);

        public Task DeleteFacultyMemberScientificMissionAsync(int id)
            => _helper.DeleteScientificMissionAsync(id);
    }
}
