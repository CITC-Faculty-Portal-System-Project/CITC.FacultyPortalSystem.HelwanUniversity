using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.PrizesModule
{
    public interface IPrizesAndRewardsService
    {
        public Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetAllPrizesAndRewardsAsync(PrizesAndRewardsSpecificationParameters parameters);
        public Task<PrizesAndRewardsResponseDTO> GetPrizeOrRewardByIdAsync(int id);
        public Task<PrizesAndRewardsResponseDTO> CreatePrizeOrRewardAsync(PrizesAndRewardsCreateDTO prizesAndRewardsCreateDto);
        public Task<PrizesAndRewardsResponseDTO> UpdatePrizeOrRewardAsync(int prizesOrRewardId, PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDto);
        public Task DeletePrizeOrRewardAsync(int prizesOrRewardId);
    }
}
