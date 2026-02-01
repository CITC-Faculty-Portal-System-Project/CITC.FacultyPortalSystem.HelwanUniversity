using Shared.Dtos.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.MissionsModule
{
    public interface IScientificMissionsService
    {
        Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(ScientificMissionSpecificationParamaters paramaters);
        public Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(int id);
        Task<ScientificMissionResponseDto> CreateScientificMissionAsync(ScientificMissionCreateDto scientificMissionCreateDto);
        Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(int id, ScientificMissionUpdateDto mission);
        public Task DeleteScientificMissionAsync(int id);
    }
}
