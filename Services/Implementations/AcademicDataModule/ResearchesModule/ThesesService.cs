using AutoMapper.Execution;
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Helpers.CollectionSyncingHelpers;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
    public class ThesesService
        (IUnitOfWork unitOfWork, IMapper mapper
        , IAuthenticationService authenticationService) : BaseService<Thesis, int>
        (unitOfWork, authenticationService, mapper), IThesesService
    {


        protected override string EntityName => "Theses";

        public async Task<ThesesResponseDTO> AcceptRecommendedThesesSupervison(int thesisId)
        {
            var user = await GetCurrentUserAsync();

            var thesisEntity = await Repo.GetAsync(new RecommendedThesesSupervisionSpecifications(thesisId, user.UserId))
                ?? throw NotFound();

            if (!thesisEntity.ComitteeMembers!.Any(c => c.MemberId.HasValue && c.MemberId.Value == user.UserId))
                throw new UnauthorizedException("You Can't Modify this theis!");

            thesisEntity.ComitteeMembers!
             .SingleOrDefault(c => c.MemberId == user.UserId)!.isConfirmed = true;

            Repo.Update(thesisEntity);

            await unitOfWork.SaveChangesAsync();

            return Mapper.Map<ThesesResponseDTO>(thesisEntity);

        }

        public async Task<ThesesResponseDTO> AddTheses(ThesesDTO theses)
        {
            var researchesRepo = UnitOfWork.GetRepository<Research, int>();
            var personalRepo = UnitOfWork.GetRepository<PersonalData, int>();


            var currentUser = await GetCurrentUserAsync();

            theses.FacultyMemberId = currentUser.UserId;

            if (theses.ComitteeMembers is not null)
                foreach (var member in theses.ComitteeMembers!)
                {
                    var memberEntity = await personalRepo.
                        GetAsync(new PersonalDataWithNameSpecification(member.Name));

                    if(memberEntity is not null && 
                        memberEntity.FacultyMemberId != currentUser.UserId)
                            member.MemberId = memberEntity!.FacultyMemberId;
                }


            var entity = Mapper.Map<Thesis>(theses);

            if (theses.Researches is not null)
                foreach (var research in theses.Researches!)
                {
                    var researchEntity = await researchesRepo.
                        GetAsync(new ResearchSpecifications(research.Id, currentUser.UserId));

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

        public async Task<PaginatedResult<ThesesResponseDTO>> GetAllRecommendedThesesSupervisons(ThesesSpecificationParameters parameters)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntites = await Repo.GetAllAsync(new RecommendedThesesSupervisionSpecifications(parameters, user.UserId))
                        ?? throw NotFound();

            var totalPagesCount = await Repo.CountAsync(new RecommendedThesesSupervisionCountSpecifications(parameters, user.UserId));

            var currentPage = thesesEntites.Count();

            var thesesResponses = Mapper.Map<IEnumerable<ThesesResponseDTO>>(thesesEntites);

            return new PaginatedResult<ThesesResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, thesesResponses);
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

        public async Task<ThesesResponseDTO> GetRecommendedThesesSupervisonById(int id)
        {
            var user = await GetCurrentUserAsync();

            var entity = await Repo.GetAsync(new RecommendedThesesSupervisionSpecifications(id, user.UserId))
                     ?? throw NotFound();

            EnsureOwnership(entity.ComitteeMembers!.SingleOrDefault(cm => cm.MemberId == user.UserId)!.MemberId!.Value 
                , user.UserId, EntityName);

            return Mapper.Map<ThesesResponseDTO>(entity);
        }

        public async Task<ThesesResponseDTO> GetThesesById(int Id)
        {
            var user = await GetCurrentUserAsync();

            var entity = await Repo.GetAsync(new ThesesSpecifications(Id, user.UserId))
                     ??throw NotFound();

            EnsureOwnership(entity.FacultyMemberId, user.UserId , EntityName);

            return Mapper.Map<ThesesResponseDTO>(entity);
        }

        public async Task RejectRecommendedThesesSupervison(int thesisId)
        {
            var user = await GetCurrentUserAsync();

            var thesisEntity = await Repo.GetAsync(new RecommendedThesesSupervisionSpecifications(thesisId, user.UserId))
                ?? throw NotFound();

            if (!thesisEntity.ComitteeMembers!.Any(c => c.MemberId.HasValue && c.MemberId.Value == user.UserId))
                throw new UnauthorizedException("You Can't Modify this thesis!");

            thesisEntity.ComitteeMembers!.SingleOrDefault(c => c.MemberId == user.UserId)!
                .IsDeleted = true;
            
            
            Repo.Update(thesisEntity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<ThesesResponseDTO> UpdateTheses(int id, ThesesUpdateDTO theses)
        {
            var currentUser = await GetCurrentUserAsync();

            var thesesEntity = await Repo.GetAsync(new ThesesSpecifications(id, currentUser.UserId))
                ?? throw NotFound();

            EnsureOwnership(thesesEntity.FacultyMemberId, currentUser.UserId, EntityName);

            
            CollectionSync.Sync<ThesisComittee,
                                ThesesSupervisorDTO,
                                ThesesSupervisorDTO,
                                ThesesSupervisorResponseDTO,
                                int>(
                
                current: thesesEntity.ComitteeMembers!,
                toAdd: theses.SupervisorsToAdd,
                toUpdate: theses.SupervisorsToUpdate,
                toDelete: theses.SupervisorsToDelete,

                childKey: s => s.Id,
                deleteKey: d => d.Id,

                mapAdd: d => Mapper.Map<ThesisComittee>(d),
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
