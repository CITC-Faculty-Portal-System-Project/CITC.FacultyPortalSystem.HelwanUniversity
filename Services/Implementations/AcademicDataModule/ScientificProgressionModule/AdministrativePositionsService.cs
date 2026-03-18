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
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<AdministrativePositions, int>(unitOfWork, authenticationService, mapper),
          IAdministrativePositionsService
    {
        protected override string EntityName => "Administrative Positions";

        public async Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(
            AdministrativePositionsSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var positions = await Repo.GetAllAsync(
                new AdministrativePositionsSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<AdministrativePositionDto>>(positions);

            var totalCount = await Repo.CountAsync(
                new AdministrativePositionsCountSpecifications(parameters, email));

            return new PaginatedResult<AdministrativePositionDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var position = await Repo.GetAsync(new AdministrativePositionsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                position.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<AdministrativePositionDto>(position);
        }

        public async Task<AdministrativePositionDto> CreateAdministrativePositionAsync(
            AdministrativePositionCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var position = Mapper.Map<AdministrativePositions>(dto);
            position.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(position);
            await SaveChangesAsync();

            return Mapper.Map<AdministrativePositionDto>(position);
        }

        public async Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(
            int id,
            AdministrativePositionDto dto,
            string? facultyMemberEmail = null)
        {
            var position = await Repo.GetAsync(new AdministrativePositionsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                position.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, position);

            Repo.Update(position);
            await SaveChangesAsync();

            return Mapper.Map<AdministrativePositionDto>(position);
        }

        public async Task DeleteAdministrativePositionAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var position = await Repo.GetAsync(new AdministrativePositionsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                position.FacultyMemberId,
                facultyMemberEmail);

            position.IsDeleted = true;

            Repo.Update(position);
            await SaveChangesAsync();
        }
    }
}