using Domain.Entities.AcademicDataModule.MissionsModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.MissionsModule
{
    public class ScientificMissionsService(
     IUnitOfWork unitOfWork,
     IAuthenticationService authenticationService,
     IMapper mapper,
     ILogger<ScientificMissionsService> _logger)
     : BaseService<ScientificMissions, int>(unitOfWork, authenticationService, mapper),
       IScientificMissionsService
    {
        protected override string EntityName => "Scientific Missions";

        public async Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(
            ScientificMissionSpecificationParamaters parameters,
              string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            #region Log
            var scientificMissionsLog = new LogEntry
            {
                Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ScientificMissionsActions.ToString(),
				UserIP = GetUserIP(),
                UserName = currentUser.UserName
			};
            #endregion

            var scientificMissions = await Repo.GetAllAsync(
                new ScientificMissionsSpecifications(parameters, email));

            if(scientificMissions is null)
            {
				#region Log
				scientificMissionsLog.RenderedMessage = $"Scientific Missions not found for user: {currentUser.UserName}.";
				scientificMissionsLog.Level = "Warning";
				scientificMissionsLog.Timestamp = DateTime.Now;
				scientificMissionsLog.AdditionalData = $"User tried to get their scientific missions data, but no scientific missions data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", scientificMissionsLog);
				#endregion
				throw NotFound();
			}

            var mapped = Mapper.Map<IEnumerable<ScientificMissionResponseDto?>>(scientificMissions);

            var totalCount = await Repo.CountAsync(
                new ScientificMissionsCountSpecification(parameters, email));

			#region Log
			scientificMissionsLog.RenderedMessage = $"Scientific missions data retrieved for user: {currentUser.UserName}.";
			scientificMissionsLog.Level = "Information";
			scientificMissionsLog.Timestamp = DateTime.Now;
			scientificMissionsLog.AdditionalData = $"User retrieved their scientific missions data successfully, total count of scientific missions data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", scientificMissionsLog);
			#endregion

			return new PaginatedResult<ScientificMissionResponseDto?>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            #region Log
            var currentUser = await GetCurrentUserAsync();
			var scientificMissionLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ScientificMissionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var scientificMission = await Repo.GetAsync(
                new ScientificMissionsSpecifications(id));
            if (scientificMission is null)
            {
				#region Log
				scientificMissionLog.Timestamp = DateTime.Now;
				scientificMissionLog.Level = "Warning";
				scientificMissionLog.RenderedMessage = $"Scientific mission not found for user: {currentUser.UserName}.";
				scientificMissionLog.AdditionalData = $"User tried to get their scientific mission data with id: {id}, but no Scientific mission data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", scientificMissionLog);
				#endregion
				throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        scientificMission.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				scientificMissionLog.Timestamp = DateTime.Now;
				scientificMissionLog.Level = "Warning";
				scientificMissionLog.RenderedMessage = $"User unauthorized to access scientific mission data.";
				scientificMissionLog.AdditionalData = $"User tried to get scientific mission data with id: {id} that does not belong to them, scientific mission data faculty member id: {scientificMission.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", scientificMissionLog);
				#endregion
				throw;
            }

			#region Log
			scientificMissionLog.Timestamp = DateTime.Now;
			scientificMissionLog.Level = "Information";
			scientificMissionLog.RenderedMessage = $"Scientific mission data retrieved for user: {currentUser.UserName}.";
			scientificMissionLog.AdditionalData = $"User retrieved their scientific mission data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", scientificMissionLog);
			#endregion
			return Mapper.Map<ScientificMissionResponseDto?>(scientificMission);
        }

        public async Task<ScientificMissionResponseDto> CreateScientificMissionAsync(
            ScientificMissionCreateDto scientificMissionCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            #region Log
            var scientificMissionCreationLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ScientificMissionsActions.ToString(),
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
				scientificMissionCreationLog.Timestamp = DateTime.Now;
				scientificMissionCreationLog.Level = "Warning";
				scientificMissionCreationLog.RenderedMessage = $"Faculty Member not found.";
				scientificMissionCreationLog.AdditionalData = $"User tried to create a scientific mission for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", scientificMissionCreationLog);
				#endregion
				throw;
            }

            var scientificMission = Mapper.Map<ScientificMissions>(scientificMissionCreateDto);
            scientificMission.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(scientificMission);
            await SaveChangesAsync();

            var response = Mapper.Map<ScientificMissionResponseDto>(scientificMission);
			#region Log
			scientificMissionCreationLog.Timestamp = DateTime.Now;
			scientificMissionCreationLog.Level = "Information";
			scientificMissionCreationLog.RenderedMessage = $"User: {currentUser.UserName} created a scientific mission.";
			scientificMissionCreationLog.AdditionalData = $"User created a scientific mission with id: {response.Id} and Name: {response.MissionName} successfully.";
			_logger.LogInformation("{@LogDetails}", scientificMissionCreationLog);
			#endregion
			return response;
		}

        public async Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(
            int id,
            ScientificMissionUpdateDto mission,
            string? facultyMemberEmail = null)
        {
			#region Log
            var currentUser = await GetCurrentUserAsync();
			var scientificMissionUpdateLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ScientificMissionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
            var scientificMission = await Repo.GetAsync(
                new ScientificMissionsSpecifications(id));
            if(scientificMission is null)
            {
				#region Log
				scientificMissionUpdateLog.Timestamp = DateTime.Now;
				scientificMissionUpdateLog.Level = "Warning";
				scientificMissionUpdateLog.RenderedMessage = $"Scientific mission not found for user: {currentUser.UserName}.";
				scientificMissionUpdateLog.AdditionalData = $"User tried to update their scientific mission data with id: {id}, but no scientific mission data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", scientificMissionUpdateLog);
				#endregion
				throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        scientificMission.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				scientificMissionUpdateLog.Timestamp = DateTime.Now;
				scientificMissionUpdateLog.Level = "Warning";
				scientificMissionUpdateLog.RenderedMessage = $"User unauthorized to update scientific mission data.";
				scientificMissionUpdateLog.AdditionalData = $"User tried to update scientific mission data with id: {id} that does not belong to them, scientific mission data faculty member id: {scientificMission.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", scientificMissionUpdateLog);
				#endregion
				throw;
            }

            var oldData = Mapper.Map<ScientificMissionResponseDto>(scientificMission);
			Mapper.Map(mission, scientificMission);

            Repo.Update(scientificMission);
            await SaveChangesAsync();

            var newData = Mapper.Map<ScientificMissionResponseDto>(scientificMission);
			#region Log
			scientificMissionUpdateLog.Timestamp = DateTime.Now;
			scientificMissionUpdateLog.Level = "Information";
			scientificMissionUpdateLog.RenderedMessage = $"Scientific mission data updated for user: {currentUser.UserName}.";
			scientificMissionUpdateLog.AdditionalData = $"User updated their scientific mission data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", scientificMissionUpdateLog);
			#endregion
			return newData;
		}

        public async Task DeleteScientificMissionAsync(
            int id,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var scientificMissionDeletionLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ScientificMissionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var scientificMission = await Repo.GetAsync(
				new ScientificMissionsSpecifications(id));
			if(scientificMission is null)
			{
				#region Log
				scientificMissionDeletionLog.Timestamp = DateTime.Now;
				scientificMissionDeletionLog.Level = "Warning";
				scientificMissionDeletionLog.RenderedMessage = $"Scientific mission not found for user: {currentUser.UserName}.";
				scientificMissionDeletionLog.AdditionalData = $"User tried to delete their scientific mission data with id: {id}, but no scientific mission data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", scientificMissionDeletionLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						scientificMission.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				scientificMissionDeletionLog.Timestamp = DateTime.Now;
				scientificMissionDeletionLog.Level = "Warning";
				scientificMissionDeletionLog.RenderedMessage = $"User unauthorized to delete scientific mission data.";
				scientificMissionDeletionLog.AdditionalData = $"User tried to delete scientific mission data with id: {id} that does not belong to them, scientific mission data faculty member id: {scientificMission.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", scientificMissionDeletionLog);
				#endregion
				throw;
			}

            scientificMission.IsDeleted = true;

            Repo.Update(scientificMission);
            await SaveChangesAsync();
			#region Log
			scientificMissionDeletionLog.Timestamp = DateTime.Now;
			scientificMissionDeletionLog.Level = "Information";
			scientificMissionDeletionLog.RenderedMessage = $"Scientific mission data deleted for user: {currentUser.UserName}.";
			scientificMissionDeletionLog.AdditionalData = $"User deleted their scientific mission data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", scientificMissionDeletionLog);
			#endregion
		}
    }
}
