using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ParticipationInMagazinesService(
      IUnitOfWork unitOfWork,
      IAuthenticationService authenticationService,
      IMapper mapper,
      ILogger<ParticipationInMagazinesService> _logger)
      : BaseService<ParticipationInMagazines, int>(unitOfWork, authenticationService, mapper),
        IParticipationInMagazinesService
    {
        protected override string EntityName => "Participation In Magazines";

        public async Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(
            ParticipationInMagazinesSpecificationsParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            #region Log
            var participationsLog = new LogEntry
            {
                Category = Category.FacultyMemberAcademicData.ToString(),
                CategoryAction = CategoryAction.ParticipationInMagazinesActions.ToString(),
				UserIP = GetUserIP(),
                UserName = currentUser.UserName
			};
            #endregion

            var magazines = await Repo.GetAllAsync(
                new ParticipationInMagazinesSpecifications(parameters, email));
            if(magazines is null)
            {
				#region Log
				participationsLog.RenderedMessage = $"Participations in magazines not found for user: {currentUser.UserName}.";
				participationsLog.Level = "Warning";
				participationsLog.Timestamp = DateTime.Now;
				participationsLog.AdditionalData = $"User tried to get their participations in magazines data, but no participations in magazines data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", participationsLog);
				#endregion
				throw NotFound();
			}
                
            var mapped = Mapper.Map<IEnumerable<ParticipationInMagazinesResponseDto>>(magazines);

            var totalCount = await Repo.CountAsync(
                new ParticipationInMagazinesCountSpecifications(parameters, email));

			#region Log
			participationsLog.RenderedMessage = $"Participations in magazines data retrieved for user: {currentUser.UserName}.";
			participationsLog.Level = "Information";
			participationsLog.Timestamp = DateTime.Now;
			participationsLog.AdditionalData = $"User retrieved their participations in magazines data successfully, total count of participations in magazines data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", participationsLog);
			#endregion

			return new PaginatedResult<ParticipationInMagazinesResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            #region Log
            var currentUser = await GetCurrentUserAsync();
			var participationsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ParticipationInMagazinesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
            #endregion

            var participation = await Repo.GetAsync(
                new ParticipationInMagazinesSpecifications(id));
			if (participation is null)
            {
				#region Log
				participationsLog.Timestamp = DateTime.Now;
				participationsLog.Level = "Warning";
				participationsLog.RenderedMessage = $"Participation in magazine not found for user: {currentUser.UserName}.";
				participationsLog.AdditionalData = $"User tried to get their participation in magazine data with id: {id}, but no participation in magazine data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", participationsLog);
				#endregion
				throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        participation.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				participationsLog.Timestamp = DateTime.Now;
				participationsLog.Level = "Warning";
				participationsLog.RenderedMessage = $"User unauthorized to access participation in magazine data.";
				participationsLog.AdditionalData = $"User tried to get participation in magazine data with id: {id} that does not belong to them, participation in magazine data faculty member id: {participation.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", participationsLog);
				#endregion
				throw;
            }

			#region Log
			participationsLog.Timestamp = DateTime.Now;
			participationsLog.Level = "Information";
			participationsLog.RenderedMessage = $"Participation in magazine data retrieved for user: {currentUser.UserName}.";
			participationsLog.AdditionalData = $"User retrieved their participation in magazine data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", participationsLog);
			#endregion
			return Mapper.Map<ParticipationInMagazinesResponseDto>(participation);
        }

        public async Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(
            ParticipationInMagazineCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            #region Log
            var participationsLog = new LogEntry
            {
                Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ParticipationInMagazinesActions.ToString(),
                UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			FacultyMember facultyMember = null!;
            try
            {
                facultyMember = await GetFacultyMemberByEmailAsync(email);
            }
            catch (NotFoundException)
            {
				#region Log
				participationsLog.Timestamp = DateTime.Now;
				participationsLog.Level = "Warning";
				participationsLog.RenderedMessage = $"Faculty Member not found.";
				participationsLog.AdditionalData = $"User tried to create a participation in magazine for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", participationsLog);
				#endregion
				throw;
            }

            var participation = Mapper.Map<ParticipationInMagazines>(dto);
            participation.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(participation);
            await SaveChangesAsync();

            var response = Mapper.Map<ParticipationInMagazinesResponseDto>(participation);
			#region Log
			participationsLog.Timestamp = DateTime.Now;
			participationsLog.Level = "Information";
			participationsLog.RenderedMessage = $"User: {currentUser.UserName} created a participation in magazine.";
			participationsLog.AdditionalData = $"User created a participation in magazine with id: {response.Id} and magazine name: {response.NameOfMagazine} successfully.";
			_logger.LogInformation("{@LogDetails}", participationsLog);
			#endregion
			return response;
		}

        public async Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(
            int id,
            ParticipationInMagazineUpdateDto dto,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var participationsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ParticipationInMagazinesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
            var participation = await Repo.GetAsync(
                new ParticipationInMagazinesSpecifications(id));
            if(participation is null)
            {
				#region Log
				participationsLog.Timestamp = DateTime.Now;
				participationsLog.Level = "Warning";
				participationsLog.RenderedMessage = $"Participation in magazine not found for user: {currentUser.UserName}.";
				participationsLog.AdditionalData = $"User tried to update their participation in magazine data with id: {id}, but no participation in magazine data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", participationsLog);
				#endregion
				throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        participation.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				participationsLog.Timestamp = DateTime.Now;
				participationsLog.Level = "Warning";
				participationsLog.RenderedMessage = $"User unauthorized to update participation in magazine data.";
				participationsLog.AdditionalData = $"User tried to update participation in magazine data with id: {id} that does not belong to them, participation in magazine data faculty member id: {participation.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", participationsLog);
				#endregion
				throw;
            }

            var oldData = Mapper.Map<ParticipationInMagazinesResponseDto>(participation);
			Mapper.Map(dto, participation);

            Repo.Update(participation);
            await SaveChangesAsync();

            var newData = Mapper.Map<ParticipationInMagazinesResponseDto>(participation);
			#region Log
			participationsLog.Timestamp = DateTime.Now;
			participationsLog.Level = "Information";
			participationsLog.RenderedMessage = $"Participation in magazine data updated for user: {currentUser.UserName}.";
			participationsLog.AdditionalData = $"User updated their participation in magazine data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", participationsLog);
			#endregion
			return newData;
        }

        public async Task DeleteParticipationInMagazineAsync(
            int id,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var participationsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ParticipationInMagazinesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var participation = await Repo.GetAsync(
				new ParticipationInMagazinesSpecifications(id));
			if(participation is null)
			{
				#region Log
				participationsLog.Timestamp = DateTime.Now;
				participationsLog.Level = "Warning";
				participationsLog.RenderedMessage = $"Participation in magazine not found for user: {currentUser.UserName}.";
				participationsLog.AdditionalData = $"User tried to delete their participation in magazine data with id: {id}, but no participation in magazine data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", participationsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						participation.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				participationsLog.Timestamp = DateTime.Now;
				participationsLog.Level = "Warning";
				participationsLog.RenderedMessage = $"User unauthorized to delete participation in magazine data.";
				participationsLog.AdditionalData = $"User tried to delete participation in magazine data with id: {id} that does not belong to them, participation in magazine data faculty member id: {participation.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", participationsLog);
				#endregion
				throw;
			}

            participation.IsDeleted = true;

            Repo.Update(participation);
            await SaveChangesAsync();
			#region Log
			participationsLog.Timestamp = DateTime.Now;
			participationsLog.Level = "Information";
			participationsLog.RenderedMessage = $"Participation in magazine data deleted for user: {currentUser.UserName}.";
			participationsLog.AdditionalData = $"User deleted their participation in magazine data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", participationsLog);
			#endregion
		}
    }
}