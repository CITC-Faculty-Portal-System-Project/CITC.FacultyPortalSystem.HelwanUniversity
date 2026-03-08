using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberScientificMissionsManagementService
    {
        Task<PaginatedResult<ScientificMissionResponseDto?>> GetFacultyMemberScientificMissionsAsync(
            ScientificMissionSpecificationParamaters parameters,
            string facultyMemberEmail);

        Task<ScientificMissionResponseDto?> GetFacultyMemberScientificMissionByIdAsync(int id);

        Task<ScientificMissionResponseDto> CreateFacultyMemberScientificMissionAsync(
            ScientificMissionCreateDto scientificMissionCreateDto,
            string facultyMemberEmail);

        Task<ScientificMissionResponseDto> UpdateFacultyMemberScientificMissionAsync(
            int id,
            ScientificMissionUpdateDto scientificMissionUpdateDto);

        Task DeleteFacultyMemberScientificMissionAsync(int id);
    }
}
