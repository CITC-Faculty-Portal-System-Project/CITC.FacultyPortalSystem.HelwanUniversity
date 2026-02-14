using Domain.Entities.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.AcademicDataModule.PrizesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Implementations.AcademicDataModule.PrizesModule
{
    public class ManifestationsOfScientificAppreciationService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<ManifestationsOfScientificAppreciation, int>(unitOfWork, authenticationService, mapper), IManifestationsOfScientificAppreciationService
    {
        protected override string EntityName => "Manifestations of Scientific Appreciation";
        public async Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetAllManifestationsOfScientificAppreciationAsync(ManifestationsOfScientificAppreciationSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var manifestationsOfScientificAppreciation = await Repo.GetAllAsync(new ManifestationsOfScientificAppreciationSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var manifestationsOfScientificAppreciationResult = Mapper.Map<IEnumerable<ManifestationsOfScientificAppreciationResponseDTO>>(manifestationsOfScientificAppreciation);

            var currentPageCount = manifestationsOfScientificAppreciationResult.Count();

            var totalCount = await Repo.CountAsync(new ManifestationsOfScientificAppreciationCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>(parameters.PageIndex, currentPageCount, totalCount, manifestationsOfScientificAppreciationResult);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> GetManifestationOfScientificAppreciationByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var manifestationOfScientificAppreciation = await Repo.GetAsync(new ManifestationsOfScientificAppreciationSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(manifestationOfScientificAppreciation.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestationOfScientificAppreciation);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> CreateManifestationOfScientificAppreciationAsync(ManifestationsOfScientificAppreciationCreateDTO ManifestationsOfScientificAppreciationCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var manifestationOfScientificAppreciation = Mapper.Map<ManifestationsOfScientificAppreciation>(ManifestationsOfScientificAppreciationCreateDto);
            manifestationOfScientificAppreciation.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(manifestationOfScientificAppreciation);
            await SaveChangesAsync();

            return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestationOfScientificAppreciation);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId, ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var manifestationOfScientificAppreciation = await Repo.GetAsync(new ManifestationsOfScientificAppreciationSpecifications(manifestationsOfScientificAppreciationId))
                ?? throw NotFound();

            EnsureOwnership(manifestationOfScientificAppreciation.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(manifestationsOfScientificAppreciationUpdateDto, manifestationOfScientificAppreciation);

            Repo.Update(manifestationOfScientificAppreciation);
            await SaveChangesAsync();

            return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestationOfScientificAppreciation);
        }

        public async Task DeleteManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId)
        {
            var currentUser = await GetCurrentUserAsync();

            var manifestationOfScientificAppreciation = await Repo.GetAsync(new ManifestationsOfScientificAppreciationSpecifications(manifestationsOfScientificAppreciationId))
                ?? throw NotFound();

            EnsureOwnership(manifestationOfScientificAppreciation.FacultyMemberId, currentUser.UserId, EntityName);

            manifestationOfScientificAppreciation.IsDeleted = true;

            Repo.Update(manifestationOfScientificAppreciation);
            await SaveChangesAsync();
        }
    }
}