using Domain.Entities.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.PrizesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Implementations.AcademicDataModule.PrizesModule
{
    public class ManifestationsOfScientificAppreciationService(
      IUnitOfWork unitOfWork,
      IMapper mapper,
      IAuthenticationService authenticationService,
      IManifestationsOfScientificAppreciationHelper manifestationsOfScientificAppreciationHelper)
      : BaseService<ManifestationsOfScientificAppreciation, int>(unitOfWork, authenticationService, mapper),
        IManifestationsOfScientificAppreciationService
    {
        private readonly IManifestationsOfScientificAppreciationHelper _helper =
            manifestationsOfScientificAppreciationHelper;

        protected override string EntityName => "Manifestations of Scientific Appreciation";

        public async Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetAllManifestationsOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllManifestationsOfScientificAppreciationAsync(parameters, currentUser.Email);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> GetManifestationOfScientificAppreciationByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var manifestation = await Repo.GetAsync(new ManifestationsOfScientificAppreciationSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(manifestation.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetManifestationOfScientificAppreciationByIdAsync(id);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> CreateManifestationOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationCreateDTO manifestationsOfScientificAppreciationCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateManifestationOfScientificAppreciationAsync(
                manifestationsOfScientificAppreciationCreateDto,
                currentUser.Email);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateManifestationOfScientificAppreciationAsync(
            int manifestationsOfScientificAppreciationId,
            ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var manifestation = await Repo.GetAsync(
                new ManifestationsOfScientificAppreciationSpecifications(manifestationsOfScientificAppreciationId))
                ?? throw NotFound();

            EnsureOwnership(manifestation.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateManifestationOfScientificAppreciationAsync(
                manifestationsOfScientificAppreciationId,
                manifestationsOfScientificAppreciationUpdateDto);
        }

        public async Task DeleteManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId)
        {
            var currentUser = await GetCurrentUserAsync();

            var manifestation = await Repo.GetAsync(
                new ManifestationsOfScientificAppreciationSpecifications(manifestationsOfScientificAppreciationId))
                ?? throw NotFound();

            EnsureOwnership(manifestation.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteManifestationOfScientificAppreciationAsync(manifestationsOfScientificAppreciationId);
        }
    }
}