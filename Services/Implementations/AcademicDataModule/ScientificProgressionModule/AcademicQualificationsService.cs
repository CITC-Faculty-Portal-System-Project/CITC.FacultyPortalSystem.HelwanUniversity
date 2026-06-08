using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.ScientificProgressionModule
{
	public class AcademicQualificationsService(
     IUnitOfWork unitOfWork,
     IAuthenticationService authenticationService,
     IMapper mapper,
     ILogger<AcademicQualificationsService> _logger)
     : BaseService<AcademicQualifications, int>(unitOfWork, authenticationService, mapper),
       IAcademicQualificationsService
    {
        protected override string EntityName => "Academic Qualifications";

        public async Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(
            AcademicQualificationsSpecificationParamters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var qualificationLog = new LogEntry
            {
                Category = Category.FacultyMemberScientificProgression.ToString(),
                CategoryAction = CategoryAction.AcademicQualificationsActions.ToString(),
                UserIP = GetUserIP(),
                UserName = currentUser.UserName
			};
            #endregion

            var qualifications = await Repo.GetAllAsync(
                new AcademicQualificationsSpecifications(parameters, email));
            if(qualifications is null)
            {
				#region Log
				qualificationLog.RenderedMessage = $"Academic qualifications not found for user: {userOfData.UserName}.";
				qualificationLog.Level = "Warning";
				qualificationLog.Timestamp = DateTime.Now;
				qualificationLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their academic qualifications data, but no academic qualifications data was found in the database for user with email: {email}."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} academic qualifications data, but no academic qualifications data was found in the database for user: {userOfData.UserName}";
				_logger.LogWarning("{@LogDetails}", qualificationLog);
				#endregion
				throw NotFound();
			}

            var mapped = Mapper.Map<IEnumerable<AcademicQualificationResponseDto>>(qualifications);

            var totalCount = await Repo.CountAsync(
                new AcademicQualificationsCountSpecifications(parameters, email));

			#region Log
			qualificationLog.RenderedMessage = $"Academic qualifications data retrieved for user: {userOfData.UserName}.";
			qualificationLog.Level = "Information";
			qualificationLog.Timestamp = DateTime.Now;
			qualificationLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their academic qualifications data successfully, total count of academic qualifications data retrieved: {totalCount}."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} academic qualifications data successfully, total count of academic qualifications data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", qualificationLog);
			#endregion

			return new PaginatedResult<AcademicQualificationResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            #region Log
            var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var qualificationLog = new LogEntry
            {
                Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.AcademicQualificationsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var qualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(id));
            if (qualification is null)
            {
				#region Log
				qualificationLog.Timestamp = DateTime.Now;
				qualificationLog.Level = "Warning";
				qualificationLog.RenderedMessage = $"Academic qualification not found for user: {userOfData.UserName}.";
				qualificationLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their academic qualification data with id: {id}, but no academic qualification data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} academic qualification data with id: {id}, but no academic qualification data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", qualificationLog);
				#endregion
				throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        qualification.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				qualificationLog.Timestamp = DateTime.Now;
				qualificationLog.Level = "Warning";
				qualificationLog.RenderedMessage = $"User unauthorized to access academic qualification data.";
				qualificationLog.AdditionalData = $"User tried to get academic qualification data with id: {id} that does not belong to them, academic qualification data faculty member id: {qualification.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", qualificationLog);
				#endregion
				throw;
            }

			#region Log
			qualificationLog.Timestamp = DateTime.Now;
			qualificationLog.Level = "Information";
			qualificationLog.RenderedMessage = $"Academic qualification data retrieved for user: {userOfData.UserName}.";
			qualificationLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their academic qualification data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} academic qualification data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", qualificationLog);
			#endregion
			return Mapper.Map<AcademicQualificationResponseDto>(qualification);
        }

        public async Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(
            AcademicQualificationCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var qualificationLog = new LogEntry
            {
                Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.AcademicQualificationsActions.ToString(),
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
				qualificationLog.Timestamp = DateTime.Now;
				qualificationLog.Level = "Warning";
				qualificationLog.RenderedMessage = $"Faculty Member not found.";
				qualificationLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to create an academic qualification for a faculty member that does not exist in database, no faculty member found with email: {email}."
					: $"Admin: {currentUser.UserName} tried to create a academic qualification for user: {userOfData.UserName}, but no faculty member found with email: {email}.";
				_logger.LogWarning("{@LogDetails}", qualificationLog);
				#endregion
				throw;
            }

            var qualification = Mapper.Map<AcademicQualifications>(dto);
            qualification.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(qualification);
            await SaveChangesAsync();

            var response = Mapper.Map<AcademicQualificationResponseDto>(qualification);
			#region Log
			qualificationLog.Timestamp = DateTime.Now;
			qualificationLog.Level = "Information";
			qualificationLog.RenderedMessage = (facultyMemberEmail is null) ? $"User: {userOfData.UserName} created an academic qualification."
				: $"Admin: {currentUser.UserName} created a academic qualification for user: {userOfData.UserName}";
			qualificationLog.AdditionalData = (facultyMemberEmail is null) ? $"User created an academic qualification with id: {response.Id} successfully."
				: $"Admin: {currentUser.UserName} created a academic qualification with id: {response.Id} for user: {userOfData.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", qualificationLog);
			#endregion
			return Mapper.Map<AcademicQualificationResponseDto>(qualification);
        }

        public async Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(
            int id,
            AcademicQualificationsUpdateDto dto,
            string? facultyMemberEmail = null)
        {
			#region Log
            var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var qualificationLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.AcademicQualificationsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
            var qualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(id));
            if (qualification is null)
            {
				#region Log
				qualificationLog.Timestamp = DateTime.Now;
				qualificationLog.Level = "Warning";
				qualificationLog.RenderedMessage = $"Academic qualification not found for user: {userOfData.UserName}.";
				qualificationLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to update their academic qualification data with id: {id}, but no academic qualification data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to update user: {userOfData.UserName} academic qualification data with id: {id}, but no academic qualification data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", qualificationLog);
				#endregion
				throw NotFound();
            }

            try
            {
                await EnsureOwnershipIfClientAsync(
                        qualification.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				qualificationLog.Timestamp = DateTime.Now;
				qualificationLog.Level = "Warning";
				qualificationLog.RenderedMessage = $"User unauthorized to update academic qualification data.";
				qualificationLog.AdditionalData = $"User tried to update academic qualification data with id: {id} that does not belong to them, academic qualification data faculty member id: {qualification.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", qualificationLog);
				#endregion
				throw;
            }

            var oldData = Mapper.Map<AcademicQualificationResponseDto>(qualification);
			Mapper.Map(dto, qualification);

            Repo.Update(qualification);
            await SaveChangesAsync();

            var newData = Mapper.Map<AcademicQualificationResponseDto>(qualification);
			#region Log
			qualificationLog.Timestamp = DateTime.Now;
			qualificationLog.Level = "Information";
			qualificationLog.RenderedMessage = $"Academic qualification data updated for user: {currentUser.UserName}.";
			qualificationLog.AdditionalData = $"User updated their academic qualification data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", qualificationLog);
			#endregion
			return Mapper.Map<AcademicQualificationResponseDto>(qualification);
        }

        public async Task DeleteAcademicQualificationAsync(
            int id,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var qualificationLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.AcademicQualificationsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var qualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(id));
            if(qualification is null)
            {
				#region Log
				qualificationLog.Timestamp = DateTime.Now;
				qualificationLog.Level = "Warning";
				qualificationLog.RenderedMessage = $"Academic qualification not found for user: {userOfData.UserName}.";
				qualificationLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to delete their academic qualification data with id: {id}, but no academic qualification data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to delete user: {userOfData.UserName} academic qualification data with id: {id}, but no academic qualification data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", qualificationLog);
				#endregion
				throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        qualification.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				qualificationLog.Timestamp = DateTime.Now;
				qualificationLog.Level = "Warning";
				qualificationLog.RenderedMessage = $"User unauthorized to delete academic qualification data.";
				qualificationLog.AdditionalData = $"User tried to delete academic qualification data with id: {id} that does not belong to them, academic qualification data faculty member id: {qualification.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", qualificationLog);
				#endregion
				throw;
            }

            qualification.IsDeleted = true;

            Repo.Update(qualification);
            await SaveChangesAsync();
			#region Log
			qualificationLog.Timestamp = DateTime.Now;
			qualificationLog.Level = "Information";
			qualificationLog.RenderedMessage = $"Academic qualification data deleted for user: {userOfData.UserName}.";
			qualificationLog.AdditionalData = (facultyMemberEmail is null) ? $"User deleted their academic qualification data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} deleted user: {userOfData.UserName} academic qualification data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", qualificationLog);
			#endregion
		}
    }
}