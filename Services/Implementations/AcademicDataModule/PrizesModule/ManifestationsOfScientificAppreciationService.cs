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
      IAuthenticationService authenticationService,
      IMapper mapper)
      : BaseService<ManifestationsOfScientificAppreciation, int>(unitOfWork, authenticationService, mapper),
        IManifestationsOfScientificAppreciationService
    {
        protected override string EntityName => "Manifestations of Scientific Appreciation";

        public async Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetAllManifestationsOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var manifestations = await Repo.GetAllAsync(
                new ManifestationsOfScientificAppreciationSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ManifestationsOfScientificAppreciationResponseDTO>>(manifestations);

            var totalCount = await Repo.CountAsync(
                new ManifestationsOfScientificAppreciationCountSpecifications(parameters, email));

            return new PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> GetManifestationOfScientificAppreciationByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var manifestation = await Repo.GetAsync(
                new ManifestationsOfScientificAppreciationSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                manifestation.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> CreateManifestationOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationCreateDTO dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var manifestation = Mapper.Map<ManifestationsOfScientificAppreciation>(dto);
            manifestation.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(manifestation);
            await SaveChangesAsync();

            return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateManifestationOfScientificAppreciationAsync(
            int id,
            ManifestationsOfScientificAppreciationUpdateDTO dto,
            string? facultyMemberEmail = null)
        {
            var manifestation = await Repo.GetAsync(
                new ManifestationsOfScientificAppreciationSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                manifestation.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, manifestation);

            Repo.Update(manifestation);
            await SaveChangesAsync();

            return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
        }

        public async Task DeleteManifestationOfScientificAppreciationAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var manifestation = await Repo.GetAsync(
                new ManifestationsOfScientificAppreciationSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                manifestation.FacultyMemberId,
                facultyMemberEmail);

            manifestation.IsDeleted = true;

            Repo.Update(manifestation);
            await SaveChangesAsync();
        }
    }
}