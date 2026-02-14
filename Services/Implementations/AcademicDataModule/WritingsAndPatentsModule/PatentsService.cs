using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Implementations.AcademicDataModule.WritingsAndPatentsModule
{
    public class PatentsService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<Patents, int>(unitOfWork, authenticationService, mapper), IPatentsService
    {
        protected override string EntityName => "Patents";
        public async Task<PaginatedResult<PatentsResponseDTO>> GetAllPatentsAsync(PatentsSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var patents = await Repo.GetAllAsync(new PatentsSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var patentsResult = Mapper.Map<IEnumerable<PatentsResponseDTO>>(patents);

            var currentPageCount = patentsResult.Count();

            var totalCount = await Repo.CountAsync(new PatentsCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<PatentsResponseDTO>(parameters.PageIndex, currentPageCount, totalCount, patentsResult);
        }

        public async Task<PatentsResponseDTO> GetPatentByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var patent = await Repo.GetAsync(new PatentsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(patent.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<PatentsResponseDTO>(patent);
        }

        public async Task<PatentsResponseDTO> CreatePatentAsync(PatentsCreateDTO patentCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var patent = Mapper.Map<Patents>(patentCreateDto);
            patent.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(patent);
            await SaveChangesAsync();

            return Mapper.Map<PatentsResponseDTO>(patent);
        }

        public async Task<PatentsResponseDTO> UpdatePatentAsync(int patentId, PatentsUpdateDTO patentUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var patent = await Repo.GetAsync(new PatentsSpecifications(patentId))
                ?? throw NotFound();

            EnsureOwnership(patent.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(patentUpdateDto, patent);

            Repo.Update(patent);
            await SaveChangesAsync();

            return Mapper.Map<PatentsResponseDTO>(patent);
        }

        public async Task DeletePatentAsync(int patentId)
        {
            var currentUser = await GetCurrentUserAsync();

            var patent = await Repo.GetAsync(new PatentsSpecifications(patentId))
                ?? throw NotFound();

            EnsureOwnership(patent.FacultyMemberId, currentUser.UserId, EntityName);

            patent.IsDeleted = true;

            Repo.Update(patent);
            await SaveChangesAsync();
        }
    }
}