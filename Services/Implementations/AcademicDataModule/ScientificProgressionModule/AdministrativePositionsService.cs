using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.AcademicDataModule.ScientificProgressionModule
{
    public class AdministrativePositionsService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService,
        IAdministrativePositionsHelper administrativePositionsHelper)
        : BaseService<AdministrativePositions, int>(unitOfWork, authenticationService, mapper),
          IAdministrativePositionsService
    {
        private readonly IAdministrativePositionsHelper _helper = administrativePositionsHelper;

        protected override string EntityName => "Administrative Positions";

        public async Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(
            AdministrativePositionsSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllAdministrativePositionsAsync(parameters, currentUser.Email);
        }

        public async Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var administrativePosition = await Repo.GetAsync(new AdministrativePositionsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(administrativePosition.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetAdministrativePositionByIdAsync(id);
        }

        public async Task<AdministrativePositionDto> CreateAdministrativePositionAsync(
            AdministrativePositionCreateDto administrativePositionCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateAdministrativePositionAsync(
                administrativePositionCreateDto,
                currentUser.Email);
        }

        public async Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(
            int administrativePositionId,
            AdministrativePositionDto administrativePositionUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var administrativePosition = await Repo.GetAsync(
                new AdministrativePositionsSpecifications(administrativePositionId))
                ?? throw NotFound();

            EnsureOwnership(administrativePosition.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateAdministrativePositionAsync(
                administrativePositionId,
                administrativePositionUpdateDto);
        }

        public async Task DeleteAdministrativePositionAsync(int administrativePositionId)
        {
            var currentUser = await GetCurrentUserAsync();

            var administrativePosition = await Repo.GetAsync(
                new AdministrativePositionsSpecifications(administrativePositionId))
                ?? throw NotFound();

            EnsureOwnership(administrativePosition.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteAdministrativePositionAsync(administrativePositionId);
        }
    }
}