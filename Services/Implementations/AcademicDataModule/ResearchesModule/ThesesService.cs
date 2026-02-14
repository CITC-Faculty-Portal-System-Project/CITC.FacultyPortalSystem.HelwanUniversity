using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Microsoft.AspNetCore.Http;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Abstraction.Enums;
using Services.Global;
using Services.Helpers.CollectionSyncingHelpers;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.AttachmentsModule;
using Shared.Dtos.HigherStudiesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Net.Mail;
namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
    public class ThesesService
        (IUnitOfWork unitOfWork, IMapper mapper
        , IAuthenticationService authenticationService) : BaseService<Thesis, int>
        (unitOfWork, authenticationService, mapper), IThesesService
    {


        protected override string EntityName => "Theses";


        public async Task<ThesesResponseDTO> AddTheses(ThesesDTO theses)
        {
            var researchesRepo = UnitOfWork.GetRepository<Research, int>();
            
            var currentUser = await GetCurrentUserAsync();

            theses.FacultyMemberId = currentUser.UserId;

            var entity = Mapper.Map<Thesis>(theses);
            
            if(theses.Researches is not null)
                foreach(var research in theses.Researches!)
                {
                    var researchEntity = await researchesRepo.
                        GetAsync(new ResearchSpecifications(research.Id , currentUser.UserId));

                    entity.Researches!.Add(researchEntity!);
                }

            
            await Repo.AddAsync(entity);

            await UnitOfWork.SaveChangesAsync();
            
            return Mapper.Map<ThesesResponseDTO>(entity);
        }

        public async Task DeleteTheses(int Id)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntity = await Repo.GetAsync(new ThesesSpecifications(Id, user.UserId))
                ?? throw NotFound();

            EnsureOwnership(thesesEntity.FacultyMemberId, user.UserId, EntityName);

            thesesEntity!.IsDeleted = true;
            thesesEntity.DeletedAt = DateTime.Now;
            thesesEntity.DeletedBy = user.UserName;

            Repo.Update(thesesEntity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<PaginatedResult<ThesesResponseDTO>> GetAllTheses
            (ThesesSpecificationParameters parameters)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntites = await Repo.GetAllAsync(new ThesesSpecifications(parameters, user.UserId))
                        ?? throw NotFound();

            var totalPagesCount = await Repo.CountAsync(new ThesesCountSpecifications(parameters, user.UserId));

            var currentPage = thesesEntites.Count();

            var thesesResponses = Mapper.Map<IEnumerable<ThesesResponseDTO>>(thesesEntites);

            return new PaginatedResult<ThesesResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, thesesResponses);
        }

        public async Task<ThesesResponseDTO> GetThesesById(int Id)
        {
            var user = await GetCurrentUserAsync();

            var entity = await Repo.GetAsync(new ThesesSpecifications(Id, user.UserId))
                     ??throw NotFound();

            EnsureOwnership(entity.FacultyMemberId, user.UserId , EntityName);

            return Mapper.Map<ThesesResponseDTO>(entity);
        }

        public async Task<ThesesResponseDTO> UpdateTheses(int id, ThesesUpdateDTO theses)
        {
            var currentUser = await GetCurrentUserAsync();

            var thesesEntity = await Repo.GetAsync(new ThesesSpecifications(id, currentUser.UserId))
                ?? throw NotFound();

            EnsureOwnership(thesesEntity.FacultyMemberId, currentUser.UserId, EntityName);

            
            CollectionSync.Sync<Supervisor,
                                ThesesSupervisorDTO,
                                ThesesSupervisorDTO,
                                ThesesSupervisorResponseDTO,
                                int>(
                
                current: thesesEntity.Supervisors!,
                toAdd: theses.SupervisorsToAdd,
                toUpdate: theses.SupervisorsToUpdate,
                toDelete: theses.SupervisorsToDelete,

                childKey: s => s.Id,
                deleteKey: d => d.Id,

                mapAdd: d => Mapper.Map<Supervisor>(d),
                mapUpdate: (dto, entity) => Mapper.Map(dto, entity),

                onDelete: e => e.IsDeleted = true,

                onUpdateNotFound: id => throw new NotFoundException($"Supervisor was not found"),
                onDeleteNotFound: id => throw new NotFoundException($"Supervisor was not found for delete")
            );


            CollectionSync.Sync<Research,
                     ResearchDTO,
                     ResearchDTO,
                     ResearchResponseDTO,
                     int>(
                         current: thesesEntity.Researches!,
                         toAdd: theses.ResearchesToAdd,
                         toUpdate: theses.ResearchesToUpdate,
                         toDelete: theses.ResearchesToDelete,

                         childKey: r => r.Id,
                         deleteKey: d => d.Id,

                         mapAdd: d => Mapper.Map<Research>(d),
                         mapUpdate: (dto, entity) => Mapper.Map(dto, entity),

                         onDelete: e => e.ThesisId = null,

                         onUpdateNotFound: id => throw new NotFoundException($"Research was not found"),
                         onDeleteNotFound: id => throw new NotFoundException($"Research was not found for delete")
                     );
            
            Mapper.Map(theses, thesesEntity);

            Repo.Update(thesesEntity);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<ThesesResponseDTO>(thesesEntity);

        }
    }
}
