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
       IAuthenticationService authenticationService,
       IMapper mapper)
       : BaseService<PrizesAndRewards, int>(unitOfWork, authenticationService, mapper),
         IPrizesAndRewardsService
    {
        protected override string EntityName => "Prizes and Rewards";

        public async Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetAllPrizesAndRewardsAsync(
            PrizesAndRewardsSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var prizes = await Repo.GetAllAsync(
                new PrizesAndRewardsSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<PrizesAndRewardsResponseDTO>>(prizes);

            var totalCount = await Repo.CountAsync(
                new PrizesAndRewardsCountSpecifications(parameters, email));

            return new PaginatedResult<PrizesAndRewardsResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<PrizesAndRewardsResponseDTO> GetPrizeOrRewardByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var prize = await Repo.GetAsync(
                new PrizesAndRewardsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                prize.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<PrizesAndRewardsResponseDTO>(prize);
        }

        public async Task<PrizesAndRewardsResponseDTO> CreatePrizeOrRewardAsync(
            PrizesAndRewardsCreateDTO dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var prize = Mapper.Map<PrizesAndRewards>(dto);
            prize.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(prize);
            await SaveChangesAsync();

            return Mapper.Map<PrizesAndRewardsResponseDTO>(prize);
        }

        public async Task<PrizesAndRewardsResponseDTO> UpdatePrizeOrRewardAsync(
            int id,
            PrizesAndRewardsUpdateDTO dto,
            string? facultyMemberEmail = null)
        {
            var prize = await Repo.GetAsync(
                new PrizesAndRewardsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                prize.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, prize);

            Repo.Update(prize);
            await SaveChangesAsync();

            return Mapper.Map<PrizesAndRewardsResponseDTO>(prize);
        }

        public async Task DeletePrizeOrRewardAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var prize = await Repo.GetAsync(
                new PrizesAndRewardsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                prize.FacultyMemberId,
                facultyMemberEmail);

            prize.IsDeleted = true;

            Repo.Update(prize);
            await SaveChangesAsync();
        }
    }
}