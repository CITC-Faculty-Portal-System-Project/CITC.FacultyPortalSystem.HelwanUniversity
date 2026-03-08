using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberPrizesAndRewardsManagementService(IPrizesAndRewardsHelper _helper)
        :IFacultyMemberPrizesAndRewardsManagementService
    {
        public Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetFacultyMemberPrizesAndRewardsAsync(
         PrizesAndRewardsSpecificationParameters parameters,
         string facultyMemberEmail)
         => _helper.GetAllPrizesAndRewardsAsync(parameters, facultyMemberEmail);

        public Task<PrizesAndRewardsResponseDTO> GetFacultyMemberPrizeOrRewardByIdAsync(int id)
            => _helper.GetPrizeOrRewardByIdAsync(id);

        public Task<PrizesAndRewardsResponseDTO> CreateFacultyMemberPrizeOrRewardAsync(
            PrizesAndRewardsCreateDTO prizesAndRewardsCreateDto,
            string facultyMemberEmail)
            => _helper.CreatePrizeOrRewardAsync(prizesAndRewardsCreateDto, facultyMemberEmail);

        public Task<PrizesAndRewardsResponseDTO> UpdateFacultyMemberPrizeOrRewardAsync(
            int prizesOrRewardId,
            PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDto)
            => _helper.UpdatePrizeOrRewardAsync(prizesOrRewardId, prizesAndRewardsUpdateDto);

        public Task DeleteFacultyMemberPrizeOrRewardAsync(int prizesOrRewardId)
            => _helper.DeletePrizeOrRewardAsync(prizesOrRewardId);
    }
}
