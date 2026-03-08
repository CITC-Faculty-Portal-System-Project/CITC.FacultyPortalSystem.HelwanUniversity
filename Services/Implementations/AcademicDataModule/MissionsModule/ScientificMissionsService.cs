using Domain.Entities.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.AcademicDataModule.MissionsModule
{
    public class ScientificMissionsService(
       IUnitOfWork unitOfWork,
       IMapper mapper,
       IAuthenticationService authenticationService,
       IScientificMissionsHelper scientificMissionsHelper)
       : BaseService<ScientificMissions, int>(unitOfWork, authenticationService, mapper),
         IScientificMissionsService
    {
        private readonly IScientificMissionsHelper _helper = scientificMissionsHelper;

        protected override string EntityName => "Scientific Missions";

        public async Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(
            ScientificMissionSpecificationParamaters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllScientificMissionsAsync(parameters, currentUser.Email);
        }

        public async Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = await Repo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(scientificMission.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetScientificMissionByIdAsync(id);
        }

        public async Task<ScientificMissionResponseDto> CreateScientificMissionAsync(
            ScientificMissionCreateDto scientificMissionCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateScientificMissionAsync(scientificMissionCreateDto, currentUser.Email);
        }

        public async Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(
            int id,
            ScientificMissionUpdateDto scientificMissionUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = await Repo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(scientificMission.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateScientificMissionAsync(id, scientificMissionUpdateDto);
        }

        public async Task DeleteScientificMissionAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = await Repo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(scientificMission.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteScientificMissionAsync(id);
        }
    }
}
