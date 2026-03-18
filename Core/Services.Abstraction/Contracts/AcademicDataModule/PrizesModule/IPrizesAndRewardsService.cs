using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.PrizesModule
{
    public interface IPrizesAndRewardsService
    {
        Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetAllPrizesAndRewardsAsync(
       PrizesAndRewardsSpecificationParameters parameters,
       string? facultyMemberEmail = null);

        Task<PrizesAndRewardsResponseDTO> GetPrizeOrRewardByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<PrizesAndRewardsResponseDTO> CreatePrizeOrRewardAsync(
            PrizesAndRewardsCreateDTO dto,
            string? facultyMemberEmail = null);

        Task<PrizesAndRewardsResponseDTO> UpdatePrizeOrRewardAsync(
            int id,
            PrizesAndRewardsUpdateDTO dto,
            string? facultyMemberEmail = null);

        Task DeletePrizeOrRewardAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
