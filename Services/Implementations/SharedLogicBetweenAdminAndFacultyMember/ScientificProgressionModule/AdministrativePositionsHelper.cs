using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule
{
    public class AdministrativePositionsHelper(
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<AdministrativePositions, int>(unitOfWork, authenticationService, mapper),
          IAdministrativePositionsHelper
    {
        protected override string EntityName => "Administrative Positions";

        public async Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(
            AdministrativePositionsSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var administrativePositions = await Repo.GetAllAsync(
                new AdministrativePositionsSpecifications(parameters, facultyMemberEmail));

            var administrativePositionsResult =
                Mapper.Map<IEnumerable<AdministrativePositionDto>>(administrativePositions);

            var currentPageCount = administrativePositionsResult.Count();

            var totalCount = await Repo.CountAsync(
                new AdministrativePositionsCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<AdministrativePositionDto>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                administrativePositionsResult);
        }

        public async Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(int id)
        {
            var administrativePosition = await Repo.GetAsync(new AdministrativePositionsSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task<AdministrativePositionDto> CreateAdministrativePositionAsync(
            AdministrativePositionCreateDto administrativePositionCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var administrativePosition = Mapper.Map<AdministrativePositions>(administrativePositionCreateDto);
            administrativePosition.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(administrativePosition);
            await SaveChangesAsync();

            return Mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(
            int administrativePositionId,
            AdministrativePositionDto administrativePositionUpdateDto)
        {
            var administrativePosition = await Repo.GetAsync(
                new AdministrativePositionsSpecifications(administrativePositionId))
                ?? throw NotFound();

            Mapper.Map(administrativePositionUpdateDto, administrativePosition);

            Repo.Update(administrativePosition);
            await SaveChangesAsync();

            return Mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task DeleteAdministrativePositionAsync(int administrativePositionId)
        {
            var administrativePosition = await Repo.GetAsync(
                new AdministrativePositionsSpecifications(administrativePositionId))
                ?? throw NotFound();

            administrativePosition.IsDeleted = true;

            Repo.Update(administrativePosition);
            await SaveChangesAsync();
        }
    }
}
