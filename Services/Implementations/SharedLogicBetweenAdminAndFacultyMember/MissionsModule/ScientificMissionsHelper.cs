using Domain.Entities.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.MissionsModule
{
    public class ScientificMissionsHelper(
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<ScientificMissions, int>(unitOfWork, authenticationService, mapper),
          IScientificMissionsHelper
    {
        protected override string EntityName => "Scientific Missions";

        public async Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(
            ScientificMissionSpecificationParamaters parameters,
            string facultyMemberEmail)
        {
            var scientificMissions = await Repo.GetAllAsync(
                new ScientificMissionsSpecifications(parameters, facultyMemberEmail));

            var scientificMissionsResult =
                Mapper.Map<IEnumerable<ScientificMissionResponseDto>>(scientificMissions);

            var currentPageCount = scientificMissionsResult.Count();

            var totalCount = await Repo.CountAsync(
                new ScientificMissionsCountSpecification(parameters, facultyMemberEmail));

            return new PaginatedResult<ScientificMissionResponseDto?>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                scientificMissionsResult);
        }

        public async Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(int id)
        {
            var scientificMission = await Repo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<ScientificMissionResponseDto>(scientificMission);
        }

        public async Task<ScientificMissionResponseDto> CreateScientificMissionAsync(
            ScientificMissionCreateDto scientificMissionCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var scientificMission = Mapper.Map<ScientificMissions>(scientificMissionCreateDto);
            scientificMission.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(scientificMission);
            await SaveChangesAsync();

            return Mapper.Map<ScientificMissionResponseDto>(scientificMission);
        }

        public async Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(
            int id,
            ScientificMissionUpdateDto scientificMissionUpdateDto)
        {
            var scientificMission = await Repo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            Mapper.Map(scientificMissionUpdateDto, scientificMission);

            Repo.Update(scientificMission);
            await SaveChangesAsync();

            return Mapper.Map<ScientificMissionResponseDto>(scientificMission);
        }

        public async Task DeleteScientificMissionAsync(int id)
        {
            var scientificMission = await Repo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw NotFound();

            scientificMission.IsDeleted = true;

            Repo.Update(scientificMission);
            await SaveChangesAsync();
        }
    }
}
