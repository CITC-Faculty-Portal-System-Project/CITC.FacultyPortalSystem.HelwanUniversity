using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule
{
    public interface IScientificMissionsHelper
    {
        Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(
          ScientificMissionSpecificationParamaters parameters,
          string facultyMemberEmail);

        Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(int id);

        Task<ScientificMissionResponseDto> CreateScientificMissionAsync(
            ScientificMissionCreateDto scientificMissionCreateDto,
            string facultyMemberEmail);

        Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(
            int id,
            ScientificMissionUpdateDto scientificMissionUpdateDto);

        Task DeleteScientificMissionAsync(int id);
    }
}
