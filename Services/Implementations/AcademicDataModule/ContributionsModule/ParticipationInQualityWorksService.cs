using Domain.Entities.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.AcademicDataModule.ContributionsModule
{
    public class ParticipationInQualityWorksService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<ParticipationInQualityWorks, int>(unitOfWork, authenticationService, mapper), IParticipationInQualityWorksService
    {
        protected override string EntityName => "Participation In Quality Works";
        public async Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetAllParticipationsInQualityWorksAsync(ParticipationInQualityWorksSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var ParticipationsInQualityWorks = await Repo.GetAllAsync(new ParticipationInQualityWorksSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var participationResult = Mapper.Map<IEnumerable<ParticipationInQualityWorksResponseDTO>>(ParticipationsInQualityWorks);

            var currentPageCount = participationResult.Count();

            var totalCount = await Repo.CountAsync(new ParticipationInQualityWorksCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<ParticipationInQualityWorksResponseDTO>(parameters.PageIndex, currentPageCount, totalCount, participationResult);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> GetParticipationInQualityWorksByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInQualityWorks = await Repo.GetAsync(new ParticipationInQualityWorksSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(participationInQualityWorks.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participationInQualityWorks);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> CreateParticipationInQualityWorksAsync(ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInQualityWorks = Mapper.Map<ParticipationInQualityWorks>(participationInQualityWorksCreateDto);
            participationInQualityWorks.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(participationInQualityWorks);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participationInQualityWorks);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> UpdateParticipationInQualityWorksAsync(int participationInQualityWorksId, ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInQualityWorks = await Repo.GetAsync(new ParticipationInQualityWorksSpecifications(participationInQualityWorksId))
                ?? throw NotFound();

            EnsureOwnership(participationInQualityWorks.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(participationInQualityWorksUpdateDto, participationInQualityWorks);

            Repo.Update(participationInQualityWorks);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participationInQualityWorks);
        }

        public async Task DeleteParticipationInQualityWorksAsync(int participationInQualityWorksId)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInQualityWorks = await Repo.GetAsync(new ParticipationInQualityWorksSpecifications(participationInQualityWorksId))
                ?? throw NotFound();

            EnsureOwnership(participationInQualityWorks.FacultyMemberId, currentUser.UserId, EntityName);

            participationInQualityWorks.IsDeleted = true;

            Repo.Update(participationInQualityWorks);
            await SaveChangesAsync();
        }
    }
}