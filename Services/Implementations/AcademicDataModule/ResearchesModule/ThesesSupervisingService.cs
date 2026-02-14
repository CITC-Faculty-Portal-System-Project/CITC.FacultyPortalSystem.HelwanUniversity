using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
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

        public async Task<SupervisingThsesResponseDTO> GetThesesSupervisingById(int id)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntity = await Repo.GetAsync(new ThsesSupervisingSpecifications(id))
                            ?? throw NotFound();

            EnsureOwnership(thesesEntity.FacultyMemberId, user.UserId, EntityName);
            
            return Mapper.Map<SupervisingThsesResponseDTO>(thesesEntity);
 
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
