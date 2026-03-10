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
     IAuthenticationService authenticationService,
     IMapper mapper)
     : BaseService<ScientificMissions, int>(unitOfWork, authenticationService, mapper),
       IScientificMissionsService
    {
        protected override string EntityName => "Scientific Missions";

        public async Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(
            ScientificMissionSpecificationParamaters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var scientificMissions = await Repo.GetAllAsync(
                new ScientificMissionsSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ScientificMissionResponseDto?>>(scientificMissions);

            var totalCount = await Repo.CountAsync(
                new ScientificMissionsCountSpecification(parameters, email));

            return new PaginatedResult<ScientificMissionResponseDto?>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var scientificMission = await Repo.GetAsync(
                new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                scientificMission.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ScientificMissionResponseDto?>(scientificMission);
        }

        public async Task<ScientificMissionResponseDto> CreateScientificMissionAsync(
            ScientificMissionCreateDto scientificMissionCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var scientificMission = Mapper.Map<ScientificMissions>(scientificMissionCreateDto);
            scientificMission.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(scientificMission);
            await SaveChangesAsync();

            return Mapper.Map<ScientificMissionResponseDto>(scientificMission);
        }

        public async Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(
            int id,
            ScientificMissionUpdateDto mission,
            string? facultyMemberEmail = null)
        {
            var scientificMission = await Repo.GetAsync(
                new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                scientificMission.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(mission, scientificMission);

            Repo.Update(scientificMission);
            await SaveChangesAsync();

            return Mapper.Map<ScientificMissionResponseDto>(scientificMission);
        }

        public async Task DeleteScientificMissionAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var scientificMission = await Repo.GetAsync(
                new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                scientificMission.FacultyMemberId,
                facultyMemberEmail);

            scientificMission.IsDeleted = true;

            Repo.Update(scientificMission);
            await SaveChangesAsync();
        }
    }
}
