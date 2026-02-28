using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Helpers.CollectionSyncingHelpers;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
    public class ThesesSupervisingService(IUnitOfWork unitOfWork
        , IAuthenticationService authenticationService
        , IMapper mapper)

        : BaseService<Supervising, int>(unitOfWork, authenticationService, mapper) , IThesesSupervisingService
    {
        protected override string EntityName => "Theses Supervising";

        public async Task<SupervisingThsesResponseDTO> AcceptRecommendedThesesSupervison(int thesisId)
        {
            var user = await GetCurrentUserAsync();

            var thesisSupervisingEntity = await Repo.GetAsync(new RecommendedThesesSupervisionSpecifications(thesisId, user.UserId))
                ?? throw NotFound();

            EnsureOwnership(thesisSupervisingEntity.FacultyMemberId, user.UserId, EntityName);
      
            thesisSupervisingEntity.isConfirmed = true; 

            Repo.Update(thesisSupervisingEntity);

            await unitOfWork.SaveChangesAsync();

            return Mapper.Map<SupervisingThsesResponseDTO>(thesisSupervisingEntity);

        }

        public async Task<SupervisingThesesAddDTO> AddThesesSupervising(SupervisingThesesAddDTO thesesDTO)
        {
            var user = await GetCurrentUserAsync();

            thesesDTO.FacultyMemberId = user.UserId;
            
            var thesesSupervisingEntity = Mapper.Map<Supervising>(thesesDTO);

            await Repo.AddAsync(thesesSupervisingEntity);

            await UnitOfWork.SaveChangesAsync();

            return thesesDTO;
        }

        public async Task DeleteThesesSupervising(int id)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntity = await Repo.GetAsync(new ThsesSupervisingSpecifications(id))
                            ?? throw NotFound();

            EnsureOwnership(thesesEntity.FacultyMemberId, user.UserId, EntityName);

            thesesEntity.IsDeleted = true;
            thesesEntity.DeletedAt = DateTime.UtcNow;
            thesesEntity.DeletedBy = user.UserName;

            Repo.Update(thesesEntity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<PaginatedResult<SupervisingThsesResponseDTO>> GetAllRecommendedThesesSupervisons(ThesesSupervisingSpecificationParameters parameters)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntites = await Repo.GetAllAsync(new RecommendedThesesSupervisionSpecifications(parameters, user.UserId))
                        ?? throw NotFound();

            var totalPagesCount = await Repo.CountAsync(new RecommendedThesesSupervisionCountSpecifications(parameters, user.UserId));

            var currentPage = thesesEntites.Count();

            var thesesResponses = Mapper.Map<IEnumerable<SupervisingThsesResponseDTO>>(thesesEntites);

            return new PaginatedResult<SupervisingThsesResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, thesesResponses);
        }

        public async Task<PaginatedResult<SupervisingThsesResponseDTO>> GetAllSupervisings(ThesesSupervisingSpecificationParameters supervisingSpecificationParameters)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntity = await Repo.GetAllAsync
                (new ThsesSupervisingSpecifications(supervisingSpecificationParameters , user.UserId));

            var totalPages = await Repo.CountAsync(new ThsesSupervisingCountSpecifications(supervisingSpecificationParameters, user.UserId));

            var currentPage = thesesEntity.Count();


            return new PaginatedResult<SupervisingThsesResponseDTO>
                (supervisingSpecificationParameters.PageIndex
                , currentPage, totalPages
                , Mapper.Map<IEnumerable<SupervisingThsesResponseDTO>>(thesesEntity));
        }

        public async Task<SupervisingThsesResponseDTO> GetRecommendedThesesSupervisonById(int id)
        {
            var user = await GetCurrentUserAsync();

            var entity = await Repo.GetAsync(new RecommendedThesesSupervisionSpecifications(id, user.UserId))
                     ?? throw NotFound();

            EnsureOwnership(entity.FacultyMemberId, user.UserId, EntityName);

            return Mapper.Map<SupervisingThsesResponseDTO>(entity);
        }

        public async Task<SupervisingThsesResponseDTO> GetThesesSupervisingById(int id)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntity = await Repo.GetAsync(new ThsesSupervisingSpecifications(id))
                            ?? throw NotFound();

            EnsureOwnership(thesesEntity.FacultyMemberId, user.UserId, EntityName);
            
            return Mapper.Map<SupervisingThsesResponseDTO>(thesesEntity);
 
        }

        public async Task RejectRecommendedThesesSupervison(int thesisId)
        {
            var user = await GetCurrentUserAsync();

            var supervisingEntity = await Repo.GetAsync(new RecommendedThesesSupervisionSpecifications(thesisId, user.UserId))
                ?? throw NotFound();

            EnsureOwnership(supervisingEntity.FacultyMemberId, user.UserId, EntityName);

            supervisingEntity.IsDeleted = true;
            supervisingEntity.DeletedAt = DateTime.Now;
            supervisingEntity.DeletedBy = user.UserName;
            supervisingEntity!.Thesis!.ComitteeMembers!
                .SingleOrDefault(cm => cm.MemberId == user.UserId)!
                .IsDeleted = true;
        
            Repo.Update(supervisingEntity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<SupervisingThsesResponseDTO> UpdateThesesSupervising(int id, SupervisingThesesUpdateDTO supervisingThesesUpdateDTO)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntity = await Repo.GetAsync(new ThsesSupervisingSpecifications(id))
                            ?? throw NotFound();

            EnsureOwnership(thesesEntity.FacultyMemberId, user.UserId, EntityName);

            Mapper.Map(supervisingThesesUpdateDTO, thesesEntity);
            
            thesesEntity.UpdatedAt = DateTime.UtcNow;
            thesesEntity.UpdatedBy = user.UserName;

            Repo.Update(thesesEntity);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<SupervisingThsesResponseDTO>(thesesEntity);
        }
    }
}
