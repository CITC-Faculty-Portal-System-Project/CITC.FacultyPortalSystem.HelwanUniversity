using Domain.Entities.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.PrizesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.PrizesModule
{
    public class ManifestationsOfScientificAppreciationHelper(
          IUnitOfWork unitOfWork,
          IAuthenticationService authenticationService,
          IMapper mapper)
          : BaseService<ManifestationsOfScientificAppreciation, int>(unitOfWork, authenticationService, mapper),
            IManifestationsOfScientificAppreciationHelper
    {
        protected override string EntityName => "Manifestations of Scientific Appreciation";

        public async Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetAllManifestationsOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var manifestations = await Repo.GetAllAsync(
                new ManifestationsOfScientificAppreciationSpecifications(parameters, facultyMemberEmail));

            var manifestationsResult =
                Mapper.Map<IEnumerable<ManifestationsOfScientificAppreciationResponseDTO>>(manifestations);

            var currentPageCount = manifestationsResult.Count();

            var totalCount = await Repo.CountAsync(
                new ManifestationsOfScientificAppreciationCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                manifestationsResult);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> GetManifestationOfScientificAppreciationByIdAsync(int id)
        {
            var manifestation = await Repo.GetAsync(new ManifestationsOfScientificAppreciationSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> CreateManifestationOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationCreateDTO manifestationsOfScientificAppreciationCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var manifestation = Mapper.Map<ManifestationsOfScientificAppreciation>(
                manifestationsOfScientificAppreciationCreateDto);

            manifestation.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(manifestation);
            await SaveChangesAsync();

            return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateManifestationOfScientificAppreciationAsync(
            int manifestationsOfScientificAppreciationId,
            ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDto)
        {
            var manifestation = await Repo.GetAsync(
                new ManifestationsOfScientificAppreciationSpecifications(manifestationsOfScientificAppreciationId))
                ?? throw NotFound();

            Mapper.Map(manifestationsOfScientificAppreciationUpdateDto, manifestation);

            Repo.Update(manifestation);
            await SaveChangesAsync();

            return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
        }

        public async Task DeleteManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId)
        {
            var manifestation = await Repo.GetAsync(
                new ManifestationsOfScientificAppreciationSpecifications(manifestationsOfScientificAppreciationId))
                ?? throw NotFound();

            manifestation.IsDeleted = true;

            Repo.Update(manifestation);
            await SaveChangesAsync();
        }
    }
}
