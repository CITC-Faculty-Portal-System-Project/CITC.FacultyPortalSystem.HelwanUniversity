using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.PrizesModule
{
    public interface IPrizesAndRewardsHelper
    {
        Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetAllPrizesAndRewardsAsync(
            PrizesAndRewardsSpecificationParameters parameters,
            string facultyMemberEmail);

        Task<PrizesAndRewardsResponseDTO> GetPrizeOrRewardByIdAsync(int id);

        Task<PrizesAndRewardsResponseDTO> CreatePrizeOrRewardAsync(
            PrizesAndRewardsCreateDTO prizesAndRewardsCreateDto,
            string facultyMemberEmail);

        Task<PrizesAndRewardsResponseDTO> UpdatePrizeOrRewardAsync(
            int prizesOrRewardId,
            PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDto);

        Task DeletePrizeOrRewardAsync(int prizesOrRewardId);
    }
}
