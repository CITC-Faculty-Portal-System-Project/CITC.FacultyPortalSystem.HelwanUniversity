using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.WritingsAndPatentsModule
{
    public class ScientificWritingsService(
       IUnitOfWork unitOfWork,
       IAuthenticationService authenticationService,
       IMapper mapper,
       ILogger<ScientificWritingsService> _logger)
       : BaseService<ScientificWritings, int>(unitOfWork, authenticationService, mapper),
         IScientificWritingsService
    {
        protected override string EntityName => "Scientific Writings";

        public async Task<PaginatedResult<ScientificWritingsResponseDTO>> GetAllScientificWritingsAsync(
            ScientificWritingsSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            #region Log
            var writingsLog = new LogEntry
            {
                Category = Category.FacultyMemberAcademicData.ToString(),
                CategoryAction = CategoryAction.ScientificWritingsActions.ToString(),
                UserIP = GetUserIP(),
                UserName = currentUser.UserName,
			};
            #endregion

            var scientificWritings = await Repo.GetAllAsync(
                new ScientificWritingsSpecifications(parameters, email));
            if(scientificWritings is null)
            {
				#region Log
				writingsLog.RenderedMessage = $"Scientific writings not found for user: {currentUser.UserName}.";
				writingsLog.Level = "Warning";
				writingsLog.Timestamp = DateTime.Now;
				writingsLog.AdditionalData = $"User tried to get their scientific writings data, but no scientific writings data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", writingsLog);
				#endregion
				throw NotFound();
			}

            var mapped = Mapper.Map<IEnumerable<ScientificWritingsResponseDTO>>(scientificWritings);

            var totalCount = await Repo.CountAsync(
                new ScientificWritingsCountSpecifications(parameters, email));

			#region Log
			writingsLog.RenderedMessage = $"Scientific writings data retrieved for user: {currentUser.UserName}.";
			writingsLog.Level = "Information";
			writingsLog.Timestamp = DateTime.Now;
			writingsLog.AdditionalData = $"User retrieved their scientific writings data successfully, total count of scientific writings data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", writingsLog);
			#endregion

			return new PaginatedResult<ScientificWritingsResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ScientificWritingsResponseDTO> GetScientificWritingByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var writingsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ScientificWritingsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var scientificWriting = await Repo.GetAsync(
                new ScientificWritingsSpecifications(id));
            if(scientificWriting is null)
            {
				#region Log
				writingsLog.Timestamp = DateTime.Now;
				writingsLog.Level = "Warning";
				writingsLog.RenderedMessage = $"Scientific writing not found for user: {currentUser.UserName}.";
				writingsLog.AdditionalData = $"User tried to get their scientific writing data with id: {id}, but no scientific writing data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", writingsLog);
				#endregion
				throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        scientificWriting.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				writingsLog.Timestamp = DateTime.Now;
				writingsLog.Level = "Warning";
				writingsLog.RenderedMessage = $"User unauthorized to access scientific writing data.";
				writingsLog.AdditionalData = $"User tried to get scientific writing data with id: {id} that does not belong to them, scientific writing data faculty member id: {scientificWriting.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", writingsLog);
				#endregion
				throw;
            }

			#region Log
			writingsLog.Timestamp = DateTime.Now;
			writingsLog.Level = "Information";
			writingsLog.RenderedMessage = $"Scientific writing data retrieved for user: {currentUser.UserName}.";
			writingsLog.AdditionalData = $"User retrieved their scientific writing data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", writingsLog);
			#endregion
			return Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
        }

        public async Task<ScientificWritingsResponseDTO> CreateScientificWritingAsync(
            ScientificWritingsCreateDTO scientificWritingCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var writingsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ScientificWritingsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
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
				writingsLog.Timestamp = DateTime.Now;
				writingsLog.Level = "Warning";
				writingsLog.RenderedMessage = $"Faculty Member not found.";
				writingsLog.AdditionalData = $"User tried to create a scientific writing for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", writingsLog);
				#endregion
				throw;
            }

            var scientificWriting = Mapper.Map<ScientificWritings>(scientificWritingCreateDto);
            scientificWriting.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(scientificWriting);
            await SaveChangesAsync();

            var response = Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
			#region Log
			writingsLog.Timestamp = DateTime.Now;
			writingsLog.Level = "Information";
			writingsLog.RenderedMessage = $"User: {currentUser.UserName} created a scientific writing.";
			writingsLog.AdditionalData = $"User created a scientific writing with id: {response.Id} and title: {response.Title} successfully.";
			_logger.LogInformation("{@LogDetails}", writingsLog);
			#endregion
			return response;
        }

        public async Task<ScientificWritingsResponseDTO> UpdateScientificWritingAsync(
            int scientificWritingId,
            ScientificWritingsUpdateDTO scientificWritingUpdateDto,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var writingsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ScientificWritingsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
            var scientificWriting = await Repo.GetAsync(
                new ScientificWritingsSpecifications(scientificWritingId));
            if(scientificWriting is null)
            {
				#region Log
				writingsLog.Timestamp = DateTime.Now;
				writingsLog.Level = "Warning";
				writingsLog.RenderedMessage = $"Scientific writing not found for user: {currentUser.UserName}.";
				writingsLog.AdditionalData = $"User tried to update their scientific writing data with id: {scientificWritingId}, but no scientific writing data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", writingsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						scientificWriting.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				writingsLog.Timestamp = DateTime.Now;
				writingsLog.Level = "Warning";
				writingsLog.RenderedMessage = $"User unauthorized to update scientific writing data.";
				writingsLog.AdditionalData = $"User tried to update scientific writing data with id: {scientificWritingId} that does not belong to them, scientific writing data faculty member id: {scientificWriting.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", writingsLog);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
			Mapper.Map(scientificWritingUpdateDto, scientificWriting);

            Repo.Update(scientificWriting);
            await SaveChangesAsync();

			var newData = Mapper.Map<ScientificWritingsResponseDTO>(scientificWriting);
			#region Log
			writingsLog.Timestamp = DateTime.Now;
			writingsLog.Level = "Information";
			writingsLog.RenderedMessage = $"Scientific writing data updated for user: {currentUser.UserName}.";
			writingsLog.AdditionalData = $"User updated their scientific writing data with id: {scientificWritingId} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", writingsLog);
			#endregion
			return newData;
        }

        public async Task DeleteScientificWritingAsync(
            int scientificWritingId,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var writingsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ScientificWritingsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var scientificWriting = await Repo.GetAsync(
				new ScientificWritingsSpecifications(scientificWritingId));
			if(scientificWriting is null)
			{
				#region Log
				writingsLog.Timestamp = DateTime.Now;
				writingsLog.Level = "Warning";
				writingsLog.RenderedMessage = $"Scientific writing not found for user: {currentUser.UserName}.";
				writingsLog.AdditionalData = $"User tried to delete their scientific writing data with id: {scientificWritingId}, but no scientific writing data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", writingsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						scientificWriting.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				writingsLog.Timestamp = DateTime.Now;
				writingsLog.Level = "Warning";
				writingsLog.RenderedMessage = $"User unauthorized to delete scientific writing data.";
				writingsLog.AdditionalData = $"User tried to delete scientific writing data with id: {scientificWritingId} that does not belong to them, scientific writing data faculty member id: {scientificWriting.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", writingsLog);
				#endregion
				throw;
			}

            scientificWriting.IsDeleted = true;

            Repo.Update(scientificWriting);
            await SaveChangesAsync();
			#region Log
			writingsLog.Timestamp = DateTime.Now;
			writingsLog.Level = "Information";
			writingsLog.RenderedMessage = $"Scientific writing data deleted for user: {currentUser.UserName}.";
			writingsLog.AdditionalData = $"User deleted their scientific writing data with id: {scientificWritingId} successfully.";
			_logger.LogInformation("{@LogDetails}", writingsLog);
			#endregion
		}
    }
}