using Domain.Entities.AcademicDataModule.PrizesModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.PrizesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.PrizesModule
{
    public class ManifestationsOfScientificAppreciationService(
      IUnitOfWork unitOfWork,
      IAuthenticationService authenticationService,
      IMapper mapper,
      ILogger<ManifestationsOfScientificAppreciationService> _logger)
      : BaseService<ManifestationsOfScientificAppreciation, int>(unitOfWork, authenticationService, mapper),
        IManifestationsOfScientificAppreciationService
    {
        protected override string EntityName => "Manifestations of Scientific Appreciation";

        public async Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetAllManifestationsOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var manifestationsLog = new LogEntry
            {
                Category = Category.FacultyMemberPrizesAndRewards.ToString(),
                CategoryAction = CategoryAction.ManifestationsOfScientificAppreciationActions.ToString(),
                UserIP = GetUserIP(),
                UserName = currentUser.UserName
			};
            #endregion

            var manifestations = await Repo.GetAllAsync(
                new ManifestationsOfScientificAppreciationSpecifications(parameters, email));

            if(manifestations is null)
            {
				#region Log
				manifestationsLog.RenderedMessage = $"Manifestations of scientific appreciation not found for user: {userOfData.UserName}.";
				manifestationsLog.Level = "Warning";
				manifestationsLog.Timestamp = DateTime.Now;
				manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their manifestations of scientific appreciation data, but no manifestations of scientific appreciation data was found in the database for user with email: {email}."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} manifestations of scientific appreciation data, but no manifestations of scientific appreciation data was found in the database for user: {userOfData.UserName}";
				_logger.LogWarning("{@LogDetails}", manifestationsLog);
				#endregion
				throw NotFound();
			}
                
            var mapped = Mapper.Map<IEnumerable<ManifestationsOfScientificAppreciationResponseDTO>>(manifestations);

            var totalCount = await Repo.CountAsync(
                new ManifestationsOfScientificAppreciationCountSpecifications(parameters, email));

			#region Log
			manifestationsLog.RenderedMessage = $"Manifestations of scientific appreciation data retrieved for user: {userOfData.UserName}.";
			manifestationsLog.Level = "Information";
			manifestationsLog.Timestamp = DateTime.Now;
			manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their manifestations of scientific appreciation data successfully, total count of manifestations of scientific appreciation data retrieved: {totalCount}."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} manifestations of scientific appreciation data successfully, total count of manifestations of scientific appreciation data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", manifestationsLog);
			#endregion

			return new PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> GetManifestationOfScientificAppreciationByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var manifestationsLog = new LogEntry
			{
				Category = Category.FacultyMemberPrizesAndRewards.ToString(),
				CategoryAction = CategoryAction.ManifestationsOfScientificAppreciationActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
            #endregion

            var manifestation = await Repo.GetAsync(
                new ManifestationsOfScientificAppreciationSpecifications(id));
            if(manifestation is null)
            {
				#region Log
				manifestationsLog.Timestamp = DateTime.Now;
				manifestationsLog.Level = "Warning";
				manifestationsLog.RenderedMessage = $"Manifestation of scientific appreciation not found for user: {userOfData.UserName}.";
				manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their manifestation of scientific appreciation data with id: {id}, but no manifestation of scientific appreciation data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} manifestation of scientific appreciation data with id: {id}, but no manifestation of scientific appreciation data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", manifestationsLog);
				#endregion
				throw NotFound();
			};

            try
            {
                await EnsureOwnershipIfClientAsync(
                        manifestation.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (Exception)
            {
				#region Log
				manifestationsLog.Timestamp = DateTime.Now;
				manifestationsLog.Level = "Warning";
				manifestationsLog.RenderedMessage = $"User unauthorized to access manifestation of scientific appreciation data.";
				manifestationsLog.AdditionalData = $"User tried to get manifestation of scientific appreciation data with id: {id} that does not belong to them, manifestation of scientific appreciation data faculty member id: {manifestation.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", manifestationsLog);
				#endregion
				throw;
            }

			#region Log
			manifestationsLog.Timestamp = DateTime.Now;
			manifestationsLog.Level = "Information";
			manifestationsLog.RenderedMessage = $"Manifestation of scientific appreciation data retrieved for user: {userOfData.UserName}.";
			manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their manifestation of scientific appreciation data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} manifestation of scientific appreciation data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", manifestationsLog);
			#endregion
			return Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> CreateManifestationOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationCreateDTO dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var manifestationsLog = new LogEntry
			{
				Category = Category.FacultyMemberPrizesAndRewards.ToString(),
				CategoryAction = CategoryAction.ManifestationsOfScientificAppreciationActions.ToString(),
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
				manifestationsLog.Timestamp = DateTime.Now;
				manifestationsLog.Level = "Warning";
				manifestationsLog.RenderedMessage = $"Faculty Member not found.";
				manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to create a manifestation of scientific appreciation for a faculty member that does not exist in database, no faculty member found with email: {email}."
					: $"Admin: {currentUser.UserName} tried to create a manifestation of scientific appreciation for user: {userOfData.UserName}, but no faculty member found with email: {email}.";
				_logger.LogWarning("{@LogDetails}", manifestationsLog);
				#endregion
				throw;
            }

            var manifestation = Mapper.Map<ManifestationsOfScientificAppreciation>(dto);
            manifestation.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(manifestation);
            await SaveChangesAsync();

            var response = Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
			#region Log
			manifestationsLog.Timestamp = DateTime.Now;
			manifestationsLog.Level = "Information";
			manifestationsLog.RenderedMessage = (facultyMemberEmail is null) ? $"User: {userOfData.UserName} created a manifestation of scientific appreciation."
				: $"Admin: {currentUser.UserName} created a manifestation of scientific appreciation for user: {userOfData.UserName}";
			manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User created a manifestation of scientific appreciation with id: {response.Id} and title: {response.TitleOfAppreciation} successfully."
				: $"Admin: {currentUser.UserName} created a manifestation of scientific appreciation with id: {response.Id} and title: {response.TitleOfAppreciation} for user: {userOfData.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", manifestationsLog);
			#endregion
			return response;
        }

        public async Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateManifestationOfScientificAppreciationAsync(
            int id,
            ManifestationsOfScientificAppreciationUpdateDTO dto,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var manifestationsLog = new LogEntry
			{
				Category = Category.FacultyMemberPrizesAndRewards.ToString(),
				CategoryAction = CategoryAction.ManifestationsOfScientificAppreciationActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
            var manifestation = await Repo.GetAsync(
                new ManifestationsOfScientificAppreciationSpecifications(id));
            if(manifestation is null)
            {
				#region Log
				manifestationsLog.Timestamp = DateTime.Now;
				manifestationsLog.Level = "Warning";
				manifestationsLog.RenderedMessage = $"Manifestation of scientific appreciation not found for user: {userOfData.UserName}.";
				manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to update their manifestation of scientific appreciation data with id: {id}, but no manifestation of scientific appreciation data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to update user: {userOfData.UserName} manifestation of scientific appreciation data with id: {id}, but no manifestation of scientific appreciation data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", manifestationsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						manifestation.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				manifestationsLog.Timestamp = DateTime.Now;
				manifestationsLog.Level = "Warning";
				manifestationsLog.RenderedMessage = $"User unauthorized to update manifestation of scientific appreciation data.";
				manifestationsLog.AdditionalData = $"User tried to update manifestation of scientific appreciation data with id: {id} that does not belong to them, manifestation of scientific appreciation data faculty member id: {manifestation.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", manifestationsLog);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
			Mapper.Map(dto, manifestation);

            Repo.Update(manifestation);
            await SaveChangesAsync();

			var newData = Mapper.Map<ManifestationsOfScientificAppreciationResponseDTO>(manifestation);
			#region Log
			manifestationsLog.Timestamp = DateTime.Now;
			manifestationsLog.Level = "Information";
			manifestationsLog.RenderedMessage = $"Manifestation of scientific appreciation data updated for user: {userOfData.UserName}.";
			manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User updated their manifestation of scientific appreciation data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}."
				: $"Admin: {currentUser.UserName} updated user: {userOfData.UserName} manifestation of scientific appreciation data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", manifestationsLog);
			#endregion
			return newData;

		}

        public async Task DeleteManifestationOfScientificAppreciationAsync(
            int id,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var manifestationsLog = new LogEntry
			{
				Category = Category.FacultyMemberPrizesAndRewards.ToString(),
				CategoryAction = CategoryAction.ManifestationsOfScientificAppreciationActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var manifestation = await Repo.GetAsync(
				new ManifestationsOfScientificAppreciationSpecifications(id));
			if(manifestation is null)
			{
				#region Log
				manifestationsLog.Timestamp = DateTime.Now;
				manifestationsLog.Level = "Warning";
				manifestationsLog.RenderedMessage = $"Manifestation of scientific appreciation not found for user: {userOfData.UserName}.";
				manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to delete their manifestation of scientific appreciation data with id: {id}, but no manifestation of scientific appreciation data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to delete user: {userOfData.UserName} manifestation of scientific appreciation data with id: {id}, but no manifestation of scientific appreciation data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", manifestationsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						manifestation.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				manifestationsLog.Timestamp = DateTime.Now;
				manifestationsLog.Level = "Warning";
				manifestationsLog.RenderedMessage = $"User unauthorized to delete manifestation of scientific appreciation data.";
				manifestationsLog.AdditionalData = $"User tried to delete manifestation of scientific appreciation data with id: {id} that does not belong to them, manifestation of scientific appreciation data faculty member id: {manifestation.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", manifestationsLog);
				#endregion
				throw;
			}

            manifestation.IsDeleted = true;

            Repo.Update(manifestation);
            await SaveChangesAsync();
			#region Log
			manifestationsLog.Timestamp = DateTime.Now;
			manifestationsLog.Level = "Information";
			manifestationsLog.RenderedMessage = $"Manifestation of scientific appreciation data deleted for user: {userOfData.UserName}.";
			manifestationsLog.AdditionalData = (facultyMemberEmail is null) ? $"User deleted their manifestation of scientific appreciation data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} deleted user: {userOfData.UserName} manifestation of scientific appreciation data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", manifestationsLog);
			#endregion
		}
    }
}