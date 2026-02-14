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
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<ScientificWritings, int>(unitOfWork, authenticationService, mapper), IScientificWritingsService
    {
        protected override string EntityName => "Scientific Writings";
        public async Task<PaginatedResult<ScientificWritingsResponseDTO>> GetAllScientificWritingsAsync(ScientificWritingsSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificWriting = await Repo.GetAllAsync(new ScientificWritingsSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var scientificWritingResult = Mapper.Map<IEnumerable<ScientificWritingsResponseDTO>>(scientificWriting);

            var currentPageCount = scientificWritingResult.Count();

            var totalCount = await Repo.CountAsync(new ScientificWritingsCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<ScientificWritingsResponseDTO>(parameters.PageIndex, currentPageCount, totalCount, scientificWritingResult);
        }

        public async Task<ScientificWritingsResponseDTO> GetScientificWritingByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificWriting = await Repo.GetAsync(new ScientificWritingsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(scientificWriting.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
        }

        public async Task<ScientificWritingsResponseDTO> CreateScientificWritingAsync(ScientificWritingsCreateDTO scientificWritingCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificWriting = Mapper.Map<ScientificWritings>(scientificWritingCreateDto);
            scientificWriting.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(scientificWriting);
            await SaveChangesAsync();

            return Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
        }

        public async Task<ScientificWritingsResponseDTO> UpdateScientificWritingAsync(int scientificWritingId, ScientificWritingsUpdateDTO scientificWritingUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificWriting = await Repo.GetAsync(new ScientificWritingsSpecifications(scientificWritingId))
                ?? throw NotFound();

            EnsureOwnership(scientificWriting.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(scientificWritingUpdateDto, scientificWriting);

            Repo.Update(scientificWriting);
            await SaveChangesAsync();

            return Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
        }

        public async Task DeleteScientificWritingAsync(int scientificWritingId)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificWriting = await Repo.GetAsync(new ScientificWritingsSpecifications(scientificWritingId))
                ?? throw NotFound();

            EnsureOwnership(scientificWriting.FacultyMemberId, currentUser.UserId, EntityName);

            scientificWriting.IsDeleted = true;

            Repo.Update(scientificWriting);
            await SaveChangesAsync();
        }
    }
}