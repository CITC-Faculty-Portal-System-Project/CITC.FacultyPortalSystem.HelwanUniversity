using Domain.Entities.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.AcademicDataModule.MissionsModule
{
    public class ScientificMissionsService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<ScientificMissions, int>(unitOfWork, authenticationService, mapper), IScientificMissionsService
    {
        protected override string EntityName => "Scientific Missions";
        public async Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(ScientificMissionSpecificationParamaters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMissions = await Repo.GetAllAsync(new ScientificMissionsSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var scientificMissionsResult = Mapper.Map<IEnumerable<ScientificMissionResponseDto>>(scientificMissions);

            var currentPageCount = scientificMissions.Count();

            var totalCount = await Repo.CountAsync(new ScientificMissionsCountSpecification(parameters, currentUser.Email));

            return new PaginatedResult<ScientificMissionResponseDto?>(parameters.PageIndex, currentPageCount, totalCount, scientificMissionsResult);

        }

        public async Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = await Repo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(scientificMission.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ScientificMissionResponseDto>(scientificMission);
        }

        public async Task<ScientificMissionResponseDto> CreateScientificMissionAsync(ScientificMissionCreateDto scientificMissionCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = Mapper.Map<ScientificMissions>(scientificMissionCreateDto);
            scientificMission.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(scientificMission);
            await SaveChangesAsync();

            return Mapper.Map<ScientificMissionResponseDto>(scientificMission);

        }

        public async Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(int id, ScientificMissionUpdateDto scientificMissionUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = await Repo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(scientificMission.FacultyMemberId, currentUser.UserId, EntityName);

            scientificMission = Mapper.Map(scientificMissionUpdateDto, scientificMission);

            Repo.Update(scientificMission);
            await SaveChangesAsync();

            return Mapper.Map<ScientificMissionResponseDto>(scientificMission);
        }

        public async Task DeleteScientificMissionAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = await Repo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(scientificMission.FacultyMemberId, currentUser.UserId, EntityName);

            scientificMission.IsDeleted = true;

            Repo.Update(scientificMission);
            await SaveChangesAsync();
        }
    }
}
