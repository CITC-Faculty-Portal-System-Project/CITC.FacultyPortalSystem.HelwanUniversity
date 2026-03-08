using Domain.Entities.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.PrizesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.PrizesModule
{
    public class PrizesAndRewardsHelper(
         IUnitOfWork unitOfWork,
         IAuthenticationService authenticationService,
         IMapper mapper)
         : BaseService<PrizesAndRewards, int>(unitOfWork, authenticationService, mapper),
           IPrizesAndRewardsHelper
    {
        protected override string EntityName => "Prizes and Rewards";

        public async Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetAllPrizesAndRewardsAsync(
            PrizesAndRewardsSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var prizesAndRewards = await Repo.GetAllAsync(
                new PrizesAndRewardsSpecifications(parameters, facultyMemberEmail));

            var prizesAndRewardsResult =
                Mapper.Map<IEnumerable<PrizesAndRewardsResponseDTO>>(prizesAndRewards);

            var currentPageCount = prizesAndRewardsResult.Count();

            var totalCount = await Repo.CountAsync(
                new PrizesAndRewardsCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<PrizesAndRewardsResponseDTO>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                prizesAndRewardsResult);
        }

        public async Task<PrizesAndRewardsResponseDTO> GetPrizeOrRewardByIdAsync(int id)
        {
            var prizeOrReward = await Repo.GetAsync(new PrizesAndRewardsSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<PrizesAndRewardsResponseDTO>(prizeOrReward);
        }

        public async Task<PrizesAndRewardsResponseDTO> CreatePrizeOrRewardAsync(
            PrizesAndRewardsCreateDTO prizesAndRewardsCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var prizeOrReward = Mapper.Map<PrizesAndRewards>(prizesAndRewardsCreateDto);
            prizeOrReward.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(prizeOrReward);
            await SaveChangesAsync();

            return Mapper.Map<PrizesAndRewardsResponseDTO>(prizeOrReward);
        }

        public async Task<PrizesAndRewardsResponseDTO> UpdatePrizeOrRewardAsync(
            int prizesOrRewardId,
            PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDto)
        {
            var prizeOrReward = await Repo.GetAsync(new PrizesAndRewardsSpecifications(prizesOrRewardId))
                ?? throw NotFound();

            Mapper.Map(prizesAndRewardsUpdateDto, prizeOrReward);

            Repo.Update(prizeOrReward);
            await SaveChangesAsync();

            return Mapper.Map<PrizesAndRewardsResponseDTO>(prizeOrReward);
        }

        public async Task DeletePrizeOrRewardAsync(int prizesOrRewardId)
        {
            var prizeOrReward = await Repo.GetAsync(new PrizesAndRewardsSpecifications(prizesOrRewardId))
                ?? throw NotFound();

            prizeOrReward.IsDeleted = true;

            Repo.Update(prizeOrReward);
            await SaveChangesAsync();
        }
    }
}
