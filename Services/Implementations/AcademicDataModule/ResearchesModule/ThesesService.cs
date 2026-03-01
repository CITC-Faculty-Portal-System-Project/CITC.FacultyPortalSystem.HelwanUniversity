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



        #region Helpers


        private async Task<List<Supervising>> BuildSupervisingsAsync(
        ThesesDTO theses,
        Guid currentUserId,
        PersonalData currentStudent,
        IGenericRepository<PersonalData, int> personalRepo)
        {
            var result = new List<Supervising>();

            if (theses.ComitteeMembers is null || theses.ComitteeMembers.Count == 0)
                return result;

            foreach (var member in theses.ComitteeMembers)
            {
                var memberEntity = await personalRepo.GetAsync(
                    new PersonalDataWithNameSpecification(member.Name));

                if (memberEntity is not null && memberEntity.FacultyMemberId != currentUserId)
                    member.MemberId = memberEntity.FacultyMemberId;

                if (!member.MemberId.HasValue)
                    continue;

                var supervisingDto = Mapper.Map<SupervisingThesesAddDTO>(theses);

                supervisingDto.FacultyMemberId = member.MemberId.Value;
                supervisingDto.StudentName = currentStudent.Name ?? "-";
                supervisingDto.Specialization = currentStudent.GeneralSpecialization ?? "-";
                supervisingDto.FacultyMemberRole =
                    (Shared.Enums.ResearchesModule.FacultyMemberRoleInSupervisingThesis)member.Role;

                result.Add(Mapper.Map<Supervising>(supervisingDto));
            }

            return result;
        }

        private async Task AddResearchesAsync(
            Thesis entity,
            IEnumerable<ResearchResponseDTO>? researches,
            Guid currentUserId,
            IGenericRepository<Research, int> researchesRepo)
        {
            if (researches is null)
                return;

            entity.Researches ??= new List<Research>();

            foreach (var r in researches)
            {
                var researchEntity = await researchesRepo.GetAsync(
                    new ResearchSpecifications(r.Id, currentUserId));

                if (researchEntity is not null)
                    entity.Researches.Add(researchEntity);
            }
        }


        #endregion

        protected override string EntityName => "Theses";

        public async Task<ThesesResponseDTO> AddTheses(ThesesDTO theses)
        {
            var researchesRepo = UnitOfWork.GetRepository<Research, int>();
            var personalRepo = UnitOfWork.GetRepository<PersonalData, int>();

            var currentUser = await GetCurrentUserAsync();

            var currentStudent = await personalRepo.GetAsync(
                new PersonalDataWithFacultyMemberIdSpecifications(currentUser.UserId)) ??
                 throw new NotFoundException("Student Wasn't found!");

            theses.FacultyMemberId = currentUser.UserId;

            var supervisings = await BuildSupervisingsAsync(
                theses,
                currentUser.UserId,
                currentStudent,
                personalRepo);

            var entity = Mapper.Map<Thesis>(theses);

            await AddResearchesAsync(
                entity,
                theses.Researches,
                currentUser.UserId,
                researchesRepo);


            if (supervisings.Count != 0)
                foreach (var supervising in supervisings)
                    entity!.Supervisings!.Add(supervising);


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
                
                mapUpdate: (dto, entity) =>
                {
                    if (entity!.Theses!.Supervisings!
                    .Any(tc => tc.FacultyMemberId == entity.MemberId && tc.isConfirmed == true))
                        throw new ForbiddenException("Confirmed comitee member supervising can't be updated");

                    Mapper.Map(dto, entity);
                },

                onDelete: e =>
                {
                    if (e!.Theses!.Supervisings!.Any(tc => tc.FacultyMemberId == e.MemberId && tc.isConfirmed == true))
                        throw new ForbiddenException("Confirmed comitee member supervising can't be deleted");

                    e.IsDeleted = true;
                },

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
