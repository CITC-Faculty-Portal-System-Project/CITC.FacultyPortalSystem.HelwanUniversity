using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ParticipationInMagazinesService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService,
        IValidationService validationService)
                : BaseService<ParticipationInMagazines, int>(unitOfWork, authenticationService, mapper, validationService), IParticipationInMagazinesService
    {
        protected override string EntityName => "Participation In Magazines";
        public async Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(ParticipationInMagazinesSpecificationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInMagazines = await Repo.GetAllAsync(new ParticipationInMagazinesSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var participationIndMagazinesResult = Mapper.Map<IEnumerable<ParticipationInMagazinesResponseDto>>(participationInMagazines);

            var currentPageSize = participationInMagazines.Count();

            var totalCount = await Repo.CountAsync(new ParticipationInMagazinesCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<ParticipationInMagazinesResponseDto>(parameters.PageIndex, currentPageSize, totalCount, participationIndMagazinesResult);
        }

        public async Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInMagazine = await Repo.GetAsync(new ParticipationInMagazinesSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(participationInMagazine.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(ParticipationInMagazineCreateDto participationInMagazinesCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInMagazine = Mapper.Map<ParticipationInMagazines>(participationInMagazinesCreateDto);
            participationInMagazine.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(participationInMagazine);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(int participationInMagazineId, ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInMagazine = await Repo.GetAsync(new ParticipationInMagazinesSpecifications(participationInMagazineId))
                ?? throw NotFound();

            EnsureOwnership(participationInMagazine.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(participationInMagazinesUpdateDto, participationInMagazine);

            Repo.Update(participationInMagazine);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task DeleteParticipationInMagazineAsync(int participationInMagazineId)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInMagazine = await Repo.GetAsync(new ParticipationInMagazinesSpecifications(participationInMagazineId))
                ?? throw NotFound();

            EnsureOwnership(participationInMagazine.FacultyMemberId, currentUser.UserId, EntityName);

            participationInMagazine.IsDeleted = true;

            Repo.Update(participationInMagazine);
            await SaveChangesAsync();
        }
    }
}