using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
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
        IValidationService validationService)
                : BaseService<AdministrativePositions, int>(unitOfWork, authenticationService, mapper, validationService), IAdministrativePositionsService
    {
        protected override string EntityName => "AdminIstrative Positions";
        public async Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(AdministrativePositionsSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var administrativePositions = await Repo.GetAllAsync(new AdministrativePositionsSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var administrativePositionsResult = Mapper.Map<IEnumerable<AdministrativePositionDto>>(administrativePositions);

            var currentPageCount = administrativePositionsResult.Count();

            var totalCount = await Repo.CountAsync(new AdministrativePositionsCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<AdministrativePositionDto>(parameters.PageIndex, currentPageCount, totalCount, administrativePositionsResult);
        }

        public async Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var administrativePosition = await Repo.GetAsync(new AdministrativePositionsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(administrativePosition.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task<AdministrativePositionDto> CreateAdministrativePositionAsync(AdministrativePositionCreateDto administrativePositionCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var administrativePosition = Mapper.Map<AdministrativePositions>(administrativePositionCreateDto);
            administrativePosition.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(administrativePosition);
            await SaveChangesAsync();

            return Mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(int administrativePositionId, AdministrativePositionDto administrativePositionUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var administrativePosition = await Repo.GetAsync(new AdministrativePositionsSpecifications(administrativePositionId))
                ?? throw NotFound();

            EnsureOwnership(administrativePosition.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(administrativePositionUpdateDto, administrativePosition);

            Repo.Update(administrativePosition);
            await SaveChangesAsync();

            return Mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task DeleteAdministrativePositionAsync(int administrativePositionId)
        {
            var currentUser = await GetCurrentUserAsync();

            var administrativePosition = await Repo.GetAsync(new AdministrativePositionsSpecifications(administrativePositionId))
                ?? throw NotFound();

            EnsureOwnership(administrativePosition.FacultyMemberId, currentUser.UserId, EntityName);

            administrativePosition.IsDeleted = true;

            Repo.Update(administrativePosition);
            await SaveChangesAsync();
        }
    }
}