using AutoMapper.Execution;
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Helpers.CollectionSyncingHelpers;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.ResearchesModule;
namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
    public class ThesesService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService,
        ILogger<ThesesService> _logger)
        : BaseService<Thesis, int>(unitOfWork, authenticationService, mapper),
          IThesesService
    {
        protected override string EntityName => "Theses";

        #region Helpers

        private async Task<List<Supervising>> BuildSupervisingsAsync(
            ThesesDTO theses,
            Guid currentUserId,
            string studentName,
            string studentSpecialization,
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
                supervisingDto.StudentName = studentName;
                supervisingDto.Specialization = studentSpecialization;
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

        public async Task<ThesesResponseDTO> AddTheses(
            ThesesDTO theses,
            Guid? facultyMemberId = null)
        {
            var researchesRepo = UnitOfWork.GetRepository<Research, int>();
            var personalRepo = UnitOfWork.GetRepository<PersonalData, int>();

            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            #region Log
            var thesesLog = new LogEntry
            {
                Category = Category.FacultyMemberAcademicData.ToString(),
                CategoryAction = CategoryAction.ThesesActions.ToString(),
                UserIP = GetUserIP(),
                UserName = currentUser.UserName
			};
            #endregion

            if (facultyMemberId is null)
            {
                try
                {
                    EnsureOwnership(targetFacultyMemberId, currentUser.UserId, EntityName);
                }
                catch (UnauthorizedAccessException)
                {
                    #region Log
                    thesesLog.Timestamp = DateTime.Now;
					thesesLog.Level = "Warning";
					thesesLog.RenderedMessage = $"User unauthorized to add a thesis.";
                    thesesLog.AdditionalData = $"User tried to add a thesis for faculty member with id: {targetFacultyMemberId}, Logged in user id: {currentUser.UserId}.";
					_logger.LogWarning("{@LogDetails}", thesesLog);
					#endregion
					throw;
                }
			}

            var currentStudent = await personalRepo.GetAsync(
                new PersonalDataWithFacultyMemberIdSpecifications(targetFacultyMemberId));

            theses.FacultyMemberId = targetFacultyMemberId;

            var supervisings = await BuildSupervisingsAsync(
                theses,
                targetFacultyMemberId,
                currentStudent?.NameInComposition ?? currentStudent?.NameEn ?? currentStudent?.NameAr ?? currentUser.UserName,
                currentStudent?.GeneralSpecialization ?? currentStudent?.AccurateSpecialization ?? "-",
                personalRepo);

            var entity = Mapper.Map<Thesis>(theses);

            await AddResearchesAsync(
                entity,
                theses.Researches,
                targetFacultyMemberId,
                researchesRepo);

            if (supervisings.Count != 0)
                foreach (var supervising in supervisings)
                    entity.Supervisings!.Add(supervising);

            await Repo.AddAsync(entity);
            await SaveChangesAsync();

            var response = Mapper.Map<ThesesResponseDTO>(entity);
			#region Log
			thesesLog.Timestamp = DateTime.Now;
			thesesLog.Level = "Information";
			thesesLog.RenderedMessage = $"User: {currentUser.UserName} added a thesis.";
            thesesLog.AdditionalData = $"User added a thesis with id: {response.Id} and title: {response.Title} successfully.";
			_logger.LogInformation("{@LogDetails}", thesesLog);
			#endregion
			return response;
        }

        public async Task DeleteTheses(
            int id,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var thesesLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ThesesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var thesesEntity = await Repo.GetAsync(
                new ThesesSpecifications(id, targetFacultyMemberId));
            if(thesesEntity is null)
            {
				#region Log
				thesesLog.Timestamp = DateTime.Now;
				thesesLog.Level = "Warning";
				thesesLog.RenderedMessage = $"Thesis not found for user: {currentUser.UserName}.";
				thesesLog.AdditionalData = $"User tried to delete their thesis with id: {id}, but no thesis with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", thesesLog);
				#endregion
				throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        thesesEntity.FacultyMemberId,
                        facultyMemberId?.ToString());
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				thesesLog.Timestamp = DateTime.Now;
				thesesLog.Level = "Warning";
				thesesLog.RenderedMessage = $"User unauthorized to delete a thesis.";
				thesesLog.AdditionalData = $"User tried to delete a thesis with id: {id} for faculty member with id: {targetFacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", thesesLog);
				#endregion
				throw;
            }

            thesesEntity.IsDeleted = true;
            thesesEntity.DeletedAt = DateTime.Now;
            thesesEntity.DeletedBy = currentUser.UserName;

            Repo.Update(thesesEntity);
            await SaveChangesAsync();
            #region Log
            thesesLog.Timestamp = DateTime.Now;
			thesesLog.Level = "Information";
			thesesLog.RenderedMessage = $"Thesis data deleted for user: {currentUser.UserName}.";
			thesesLog.AdditionalData = $"User deleted a thesis with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", thesesLog);
			#endregion
		}

        public async Task<PaginatedResult<ThesesResponseDTO>> GetAllTheses(
            ThesesSpecificationParameters parameters,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var thesesLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ThesesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var thesesEntities = await Repo.GetAllAsync(
                new ThesesSpecifications(parameters, targetFacultyMemberId));
            if(thesesEntities is null)
            {
				#region Log
				thesesLog.RenderedMessage = $"Theses not found for user: {currentUser.UserName}.";
				thesesLog.Level = "Warning";
				thesesLog.Timestamp = DateTime.Now;
				thesesLog.AdditionalData = $"User tried to get their theses, but no theses was found in the database for user with id : {targetFacultyMemberId}.";
				_logger.LogWarning("{@LogDetails}", thesesLog);
				#endregion
				throw NotFound();
			}

            var totalCount = await Repo.CountAsync(
                new ThesesCountSpecifications(parameters, targetFacultyMemberId));

            var mapped = Mapper.Map<IEnumerable<ThesesResponseDTO>>(thesesEntities);
			#region Log
			thesesLog.RenderedMessage = $"Theses retrieved for user: {currentUser.UserName}.";
			thesesLog.Level = "Information";
			thesesLog.Timestamp = DateTime.Now;
			thesesLog.AdditionalData = $"User retrieved their theses successfully, total count of theses retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", thesesLog);
			#endregion
			return new PaginatedResult<ThesesResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ThesesResponseDTO> GetThesesById(
            int id,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            #region Log

            #endregion

            var entity = await Repo.GetAsync(
                new ThesesSpecifications(id, targetFacultyMemberId));
            if(entity is null)
            {
                #region Log

                #endregion
                throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        entity.FacultyMemberId,
                        facultyMemberId?.ToString());
            }
            catch (UnauthorizedAccessException)
            {
                #region Log

                #endregion
                throw;
            }

            #region Log

            #endregion
            return Mapper.Map<ThesesResponseDTO>(entity);
        }

        public async Task<ThesesResponseDTO> UpdateTheses(
            int id,
            ThesesUpdateDTO theses,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var personalRepo = UnitOfWork.GetRepository<PersonalData, int>();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var thesesEntity = await Repo.GetAsync(
                new ThesesSpecifications(id, targetFacultyMemberId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                thesesEntity.FacultyMemberId,
                facultyMemberId?.ToString());

            var currentStudent = await personalRepo.GetAsync(
                new PersonalDataWithFacultyMemberIdSpecifications(targetFacultyMemberId));

            var supervisingsToAdd = await BuildSupervisingsAsync(
                Mapper.Map<ThesesDTO>(theses),
                targetFacultyMemberId,
                currentStudent?.NameInComposition ?? currentStudent?.NameAr ?? currentUser.UserName,
                currentStudent?.GeneralSpecialization ?? currentStudent?.AccurateSpecialization ?? "-",
                personalRepo);

            if (supervisingsToAdd.Count > 0)
                foreach (var supervising in supervisingsToAdd)
                {
                    if (thesesEntity.Supervisings!.Any(s => s.FacultyMemberId == supervising.FacultyMemberId))
                        continue;

                    thesesEntity!.Supervisings!.Add(supervising);
                }

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
                    if (e!.Theses!.Supervisings!
                        .Any(tc => tc.FacultyMemberId == e.MemberId && tc.isConfirmed == true))
                        throw new ForbiddenException("Confirmed comitee member supervising can't be deleted");

                    e.IsDeleted = true;
                },
                onUpdateNotFound: id => throw new NotFoundException("Supervisor was not found"),
                onDeleteNotFound: id => throw new NotFoundException("Supervisor was not found for delete")
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
                onUpdateNotFound: id => throw new NotFoundException("Research was not found"),
                onDeleteNotFound: id => throw new NotFoundException("Research was not found for delete")
            );

            Mapper.Map(theses, thesesEntity);

            Repo.Update(thesesEntity);
            await SaveChangesAsync();

            return Mapper.Map<ThesesResponseDTO>(thesesEntity);
        }
    }
}
