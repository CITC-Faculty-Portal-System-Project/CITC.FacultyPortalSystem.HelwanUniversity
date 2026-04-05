using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Implementations.AcademicDataModule.WritingsAndPatentsModule
{
    public class ScientificWritingsService(
       IUnitOfWork unitOfWork,
       IAuthenticationService authenticationService,
       IMapper mapper)
       : BaseService<ScientificWritings, int>(unitOfWork, authenticationService, mapper),
         IScientificWritingsService
    {
        protected override string EntityName => "Scientific Writings";

        public async Task<PaginatedResult<ScientificWritingsResponseDTO>> GetAllScientificWritingsAsync(
            ScientificWritingsSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var scientificWritings = await Repo.GetAllAsync(
                new ScientificWritingsSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ScientificWritingsResponseDTO>>(scientificWritings);

            var totalCount = await Repo.CountAsync(
                new ScientificWritingsCountSpecifications(parameters, email));

            return new PaginatedResult<ScientificWritingsResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ScientificWritingsResponseDTO> GetScientificWritingByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var scientificWriting = await Repo.GetAsync(
                new ScientificWritingsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                scientificWriting.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
        }

        public async Task<ScientificWritingsResponseDTO> CreateScientificWritingAsync(
            ScientificWritingsCreateDTO scientificWritingCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var scientificWriting = Mapper.Map<ScientificWritings>(scientificWritingCreateDto);
            scientificWriting.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(scientificWriting);
            await SaveChangesAsync();

            return Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
        }

        public async Task<ScientificWritingsResponseDTO> UpdateScientificWritingAsync(
            int scientificWritingId,
            ScientificWritingsUpdateDTO scientificWritingUpdateDto,
            string? facultyMemberEmail = null)
        {
            var scientificWriting = await Repo.GetAsync(
                new ScientificWritingsSpecifications(scientificWritingId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                scientificWriting.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(scientificWritingUpdateDto, scientificWriting);

            Repo.Update(scientificWriting);
            await SaveChangesAsync();

            return Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
        }

        public async Task DeleteScientificWritingAsync(
            int scientificWritingId,
            string? facultyMemberEmail = null)
        {
            var scientificWriting = await Repo.GetAsync(
                new ScientificWritingsSpecifications(scientificWritingId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                scientificWriting.FacultyMemberId,
                facultyMemberEmail);

            scientificWriting.IsDeleted = true;

            Repo.Update(scientificWriting);
            await SaveChangesAsync();
        }
    }
}