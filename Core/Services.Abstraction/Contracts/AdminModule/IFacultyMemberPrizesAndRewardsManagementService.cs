using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberPrizesAndRewardsManagementService
    {
        Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetFacultyMemberPrizesAndRewardsAsync(
         PrizesAndRewardsSpecificationParameters parameters,
         string facultyMemberEmail);

        Task<PrizesAndRewardsResponseDTO> GetFacultyMemberPrizeOrRewardByIdAsync(int id);

        Task<PrizesAndRewardsResponseDTO> CreateFacultyMemberPrizeOrRewardAsync(
            PrizesAndRewardsCreateDTO prizesAndRewardsCreateDto,
            string facultyMemberEmail);

        Task<PrizesAndRewardsResponseDTO> UpdateFacultyMemberPrizeOrRewardAsync(
            int prizesOrRewardId,
            PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDto);

        Task DeleteFacultyMemberPrizeOrRewardAsync(int prizesOrRewardId);
    }
}
