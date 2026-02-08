using Domain.Entities.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.AcademicDataModule.PrizesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Implementations.AcademicDataModule.PrizesModule
{
    public class PrizesAndRewardsService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<PrizesAndRewards, int>(unitOfWork, authenticationService, mapper), IPrizesAndRewardsService
    {
        protected override string EntityName => "Prizes and Rewards";
        public async Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetAllPrizesAndRewardsAsync(PrizesAndRewardsSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var prizesAndRewards = await Repo.GetAllAsync(new PrizesAndRewardsSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var prizesAndRewardsResult = Mapper.Map<IEnumerable<PrizesAndRewardsResponseDTO>>(prizesAndRewards);

            var currentPageCount = prizesAndRewardsResult.Count();

            var totalCount = await Repo.CountAsync(new PrizesAndRewardsCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<PrizesAndRewardsResponseDTO>(parameters.PageIndex, currentPageCount, totalCount, prizesAndRewardsResult);
        }

        public async Task<PrizesAndRewardsResponseDTO> GetPrizeOrRewardByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var prizeOrReward = await Repo.GetAsync(new PrizesAndRewardsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(prizeOrReward.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<PrizesAndRewardsResponseDTO>(prizeOrReward);
        }

        public async Task<PrizesAndRewardsResponseDTO> CreatePrizeOrRewardAsync(PrizesAndRewardsCreateDTO prizesAndRewardsCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var prizeOrReward = Mapper.Map<PrizesAndRewards>(prizesAndRewardsCreateDto);
            prizeOrReward.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(prizeOrReward);
            await SaveChangesAsync();

            return Mapper.Map<PrizesAndRewardsResponseDTO>(prizeOrReward);
        }

        public async Task<PrizesAndRewardsResponseDTO> UpdatePrizeOrRewardAsync(int prizesOrRewardId, PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var prizeOrReward = await Repo.GetAsync(new PrizesAndRewardsSpecifications(prizesOrRewardId))
                ?? throw NotFound();

            EnsureOwnership(prizeOrReward.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(prizesAndRewardsUpdateDto, prizeOrReward);

            Repo.Update(prizeOrReward);
            await SaveChangesAsync();

            return Mapper.Map<PrizesAndRewardsResponseDTO>(prizeOrReward);
        }

        public async Task DeletePrizeOrRewardAsync(int prizesOrRewardId)
        {
            var currentUser = await GetCurrentUserAsync();

            var prizeOrReward = await Repo.GetAsync(new PrizesAndRewardsSpecifications(prizesOrRewardId))
                ?? throw NotFound();

            EnsureOwnership(prizeOrReward.FacultyMemberId, currentUser.UserId, EntityName);

            prizeOrReward.IsDeleted = true;

            Repo.Update(prizeOrReward);
            await SaveChangesAsync();
        }
    }
}