using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.MissionsModule
{
    public interface IScientificMissionsService
    {
        Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(
      ScientificMissionSpecificationParamaters parameters,
      string? facultyMemberEmail = null);

        Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ScientificMissionResponseDto> CreateScientificMissionAsync(
            ScientificMissionCreateDto scientificMissionCreateDto,
            string? facultyMemberEmail = null);

        Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(
            int id,
            ScientificMissionUpdateDto mission,
            string? facultyMemberEmail = null);

        Task DeleteScientificMissionAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
