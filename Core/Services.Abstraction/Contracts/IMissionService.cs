using Shared;
using Shared.Dtos.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
namespace Services.Abstraction.Contracts
{
    public interface IMissionService
    {
        Task<MissionResponseDto> AddAsync(MissionAddDto mission);
        Task<MissionEditResponseDto?> EditAsync(int id, MissionEditDto mission);
        Task<PaginatedResult<MissionResponseDto?>> GetAllMissionsAsync(MissionSpecificationParamaters paramaters);
        public Task<bool> DeleteMissionAsync(int id , string reason = "لا يوجد");
        public Task<MissionResponseDto?> GetMissionByIdAsync(int id);

    }
}
