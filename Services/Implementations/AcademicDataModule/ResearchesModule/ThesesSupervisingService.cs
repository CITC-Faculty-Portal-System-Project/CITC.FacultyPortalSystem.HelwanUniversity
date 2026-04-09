using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Helpers.CollectionSyncingHelpers;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
    public class ThesesSupervisingService(
      IUnitOfWork unitOfWork,
      IAuthenticationService authenticationService,
      IMapper mapper)
      : BaseService<Supervising, int>(unitOfWork, authenticationService, mapper),
        IThesesSupervisingService
    {
        protected override string EntityName => "Theses Supervising";

        public async Task<SupervisingThesesAddDTO> AddThesesSupervising(
            SupervisingThesesAddDTO thesesDTO,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            if (facultyMemberId is null)
                EnsureOwnership(targetFacultyMemberId, currentUser.UserId, EntityName);

            thesesDTO.FacultyMemberId = targetFacultyMemberId;

            var thesesSupervisingEntity = Mapper.Map<Supervising>(thesesDTO);
            thesesSupervisingEntity.isConfirmed = true;

            await Repo.AddAsync(thesesSupervisingEntity);
            await SaveChangesAsync();

            return thesesDTO;
        }

        public async Task DeleteThesesSupervising(
            int id,
            Guid? facultyMemberId = null)
        {
            var thesesEntity = await Repo.GetAsync(
                new ThsesSupervisingSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                thesesEntity.FacultyMemberId,
                facultyMemberId?.ToString());

            var currentUser = await GetCurrentUserAsync();

            thesesEntity.IsDeleted = true;
            thesesEntity.DeletedAt = DateTime.UtcNow;
            thesesEntity.DeletedBy = currentUser.UserName;

            Repo.Update(thesesEntity);
            await SaveChangesAsync();
        }

       
        public async Task<PaginatedResult<SupervisingThsesResponseDTO>> GetAllSupervisings(
            ThesesSupervisingSpecificationParameters supervisingSpecificationParameters,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var thesesEntities = await Repo.GetAllAsync(
                new ThsesSupervisingSpecifications(supervisingSpecificationParameters, targetFacultyMemberId))
                ?? throw NotFound();

            var totalCount = await Repo.CountAsync(
                new ThsesSupervisingCountSpecifications(supervisingSpecificationParameters, targetFacultyMemberId));

            var mapped = Mapper.Map<IEnumerable<SupervisingThsesResponseDTO>>(thesesEntities);

            return new PaginatedResult<SupervisingThsesResponseDTO>(
                supervisingSpecificationParameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<SupervisingThsesResponseDTO> GetRecommendedThesesSupervisonById(
            int id,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var entity = await Repo.GetAsync(
                new RecommendedThesesSupervisionSpecifications(id, targetFacultyMemberId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                entity.FacultyMemberId,
                facultyMemberId?.ToString());

            return Mapper.Map<SupervisingThsesResponseDTO>(entity);
        }

        public async Task<SupervisingThsesResponseDTO> GetThesesSupervisingById(
            int id,
            Guid? facultyMemberId = null)
        {
            var thesesEntity = await Repo.GetAsync(
                new ThsesSupervisingSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                thesesEntity.FacultyMemberId,
                facultyMemberId?.ToString());

            return Mapper.Map<SupervisingThsesResponseDTO>(thesesEntity);
        }

        
        public async Task<SupervisingThsesResponseDTO> UpdateThesesSupervising(
            int id,
            SupervisingThesesUpdateDTO supervisingThesesUpdateDTO,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();

            var thesesEntity = await Repo.GetAsync(
                new ThsesSupervisingSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                thesesEntity.FacultyMemberId,
                facultyMemberId?.ToString());

            Mapper.Map(supervisingThesesUpdateDTO, thesesEntity);

            thesesEntity.UpdatedAt = DateTime.UtcNow;
            thesesEntity.UpdatedBy = currentUser.UserName;

            Repo.Update(thesesEntity);
            await SaveChangesAsync();

            return Mapper.Map<SupervisingThsesResponseDTO>(thesesEntity);
        }
    }
}
