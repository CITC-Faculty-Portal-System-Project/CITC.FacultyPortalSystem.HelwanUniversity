using Domain.Entities.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.PrizesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Implementations.AcademicDataModule.PrizesModule
{
    public class PrizesAndRewardsService(
      IUnitOfWork unitOfWork,
      IMapper mapper,
      IAuthenticationService authenticationService,
      IPrizesAndRewardsHelper prizesAndRewardsHelper)
      : BaseService<PrizesAndRewards, int>(unitOfWork, authenticationService, mapper),
        IPrizesAndRewardsService
    {
        private readonly IPrizesAndRewardsHelper _helper = prizesAndRewardsHelper;

        protected override string EntityName => "Prizes and Rewards";

        public async Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetAllPrizesAndRewardsAsync(
            PrizesAndRewardsSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllPrizesAndRewardsAsync(parameters, currentUser.Email);
        }

        public async Task<PrizesAndRewardsResponseDTO> GetPrizeOrRewardByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var prizeOrReward = await Repo.GetAsync(new PrizesAndRewardsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(prizeOrReward.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetPrizeOrRewardByIdAsync(id);
        }

        public async Task<PrizesAndRewardsResponseDTO> CreatePrizeOrRewardAsync(
            PrizesAndRewardsCreateDTO prizesAndRewardsCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreatePrizeOrRewardAsync(prizesAndRewardsCreateDto, currentUser.Email);
        }

        public async Task<PrizesAndRewardsResponseDTO> UpdatePrizeOrRewardAsync(
            int prizesOrRewardId,
            PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var prizeOrReward = await Repo.GetAsync(new PrizesAndRewardsSpecifications(prizesOrRewardId))
                ?? throw NotFound();

            EnsureOwnership(prizeOrReward.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdatePrizeOrRewardAsync(prizesOrRewardId, prizesAndRewardsUpdateDto);
        }

        public async Task DeletePrizeOrRewardAsync(int prizesOrRewardId)
        {
            var currentUser = await GetCurrentUserAsync();

            var prizeOrReward = await Repo.GetAsync(new PrizesAndRewardsSpecifications(prizesOrRewardId))
                ?? throw NotFound();

            EnsureOwnership(prizeOrReward.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeletePrizeOrRewardAsync(prizesOrRewardId);
        }
    }
}