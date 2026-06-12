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
	public class ProjectsService(
	   IUnitOfWork unitOfWork,
	   IAuthenticationService authenticationService,
	   IMapper mapper,
	   ILogger<ProjectsService> _logger)
	   : BaseService<Projects, int>(unitOfWork, authenticationService, mapper),
		 IProjectsService
	{
		protected override string EntityName => "Projects";

		public async Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(
			ProjectsSpecifcationsParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var projectsLog = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.ProjectsServiceActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var projects = await Repo.GetAllAsync(
				new ProjectsSpecifications(parameters, email));

			if (projects is null)
			{
				#region Log
				projectsLog.RenderedMessage = $"Projects not found for user: {currentUser.UserName}.";
				projectsLog.Level = "Warning";
				projectsLog.Timestamp = DateTime.Now;
				projectsLog.AdditionalData = $"User tried to get their projects data, but no projects data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", projectsLog);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<ProjectsResponseDto>>(projects);

			var totalCount = await Repo.CountAsync(
				new ProjectsCountSpecifications(parameters, email));

			#region Log
			projectsLog.RenderedMessage = $"Projects data retrieved for user: {currentUser.UserName}.";
			projectsLog.Level = "Information";
			projectsLog.Timestamp = DateTime.Now;
			projectsLog.AdditionalData = $"User retrieved their projects data successfully, total count of projects data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", projectsLog);
			#endregion

			return new PaginatedResult<ProjectsResponseDto>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

        public async Task<ProjectsResponseDto> GetProjectByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            #region Log
            var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var projectsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ProjectsServiceActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var project = await Repo.GetAsync(
				new ProjectsSpecifications(id));
			if (project is null)
			{
				#region Log
				projectsLog.Timestamp = DateTime.Now;
				projectsLog.Level = "Warning";
				projectsLog.RenderedMessage = $"Project not found for user: {currentUser.UserName}.";
				projectsLog.AdditionalData = $"User tried to get their project data with id: {id}, but no project data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", projectsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						project.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				projectsLog.Timestamp = DateTime.Now;
				projectsLog.Level = "Warning";
				projectsLog.RenderedMessage = $"User unauthorized to access project data.";
				projectsLog.AdditionalData = $"User tried to get project data with id: {id} that does not belong to them, project data faculty member id: {project.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", projectsLog);
				#endregion
				throw;
			}

			#region Log
			projectsLog.Timestamp = DateTime.Now;
			projectsLog.Level = "Information";
			projectsLog.RenderedMessage = $"Project data retrieved for user: {currentUser.UserName}.";
			projectsLog.AdditionalData = $"User retrieved their project data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", projectsLog);
			#endregion
			return Mapper.Map<ProjectsResponseDto>(project);
		}

		public async Task<ProjectsResponseDto> CreateProjectAsync(
			ProjectCreateDto dto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var projectsLog = new LogEntry
            {
                Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.ProjectsServiceActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			FacultyMember facultyMember;
			try
			{
				facultyMember = await GetFacultyMemberByEmailAsync(email);
			}
			catch (NotFoundException)
			{
				#region Log
				projectsLog.Timestamp = DateTime.Now;
				projectsLog.Level = "Warning";
				projectsLog.RenderedMessage = $"Faculty Member not found.";
				projectsLog.AdditionalData = $"User tried to create a project for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", projectsLog);
				#endregion
				throw;
			}

			var project = Mapper.Map<Projects>(dto);
			project.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(project);
			await SaveChangesAsync();

			var response = Mapper.Map<ProjectsResponseDto>(project);
			#region Log
			projectsLog.Timestamp = DateTime.Now;
			projectsLog.Level = "Information";
			projectsLog.RenderedMessage = $"User: {currentUser.UserName} created a project.";
			projectsLog.AdditionalData = $"User created a project with id: {response.Id} and name: {response.NameOfProject} successfully.";
			_logger.LogInformation("{@LogDetails}", projectsLog);
			#endregion
			return response;
		}

		public async Task<ProjectsResponseDto> UpdateProjectAsync(
			int id,
			ProjectUpdateDto dto,
			string? facultyMemberEmail = null)
		{
			#region Log
            var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var projectsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ProjectsServiceActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var project = await Repo.GetAsync(
				new ProjectsSpecifications(id));
			if (project is null)
			{
				#region Log
				projectsLog.Timestamp = DateTime.Now;
				projectsLog.Level = "Warning";
				projectsLog.RenderedMessage = $"Project not found for user: {currentUser.UserName}.";
				projectsLog.AdditionalData = $"User tried to update their project data with id: {id}, but no project data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", projectsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						project.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				projectsLog.Timestamp = DateTime.Now;
				projectsLog.Level = "Warning";
				projectsLog.RenderedMessage = $"User unauthorized to update project data.";
				projectsLog.AdditionalData = $"User tried to update project data with id: {id} that does not belong to them, project data faculty member id: {project.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", projectsLog);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<ProjectsResponseDto>(project);
			Mapper.Map(dto, project);

			Repo.Update(project);
			await SaveChangesAsync();

			var newData = Mapper.Map<ProjectsResponseDto>(project);
			#region Log
			projectsLog.Timestamp = DateTime.Now;
			projectsLog.Level = "Information";
			projectsLog.RenderedMessage = $"Project data updated for user: {currentUser.UserName}.";
			projectsLog.AdditionalData = $"User updated their project data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", projectsLog);
			#endregion
			return Mapper.Map<ProjectsResponseDto>(project);
		}

        public async Task DeleteProjectAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            #region Log
            var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var projectsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ProjectsServiceActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var project = await Repo.GetAsync(
				new ProjectsSpecifications(id));
			if (project is null)
			{
				#region Log
				projectsLog.Timestamp = DateTime.Now;
				projectsLog.Level = "Warning";
				projectsLog.RenderedMessage = $"Project not found for user: {currentUser.UserName}.";
				projectsLog.AdditionalData = $"User tried to delete their project data with id: {id}, but no project data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", projectsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						project.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				projectsLog.Timestamp = DateTime.Now;
				projectsLog.Level = "Warning";
				projectsLog.RenderedMessage = $"User unauthorized to delete project data.";
				projectsLog.AdditionalData = $"User tried to delete project data with id: {id} that does not belong to them, project data faculty member id: {project.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", projectsLog);
				#endregion
				throw;
			}

			project.IsDeleted = true;

			Repo.Update(project);
			await SaveChangesAsync();
			#region Log
			projectsLog.Timestamp = DateTime.Now;
			projectsLog.Level = "Information";
			projectsLog.RenderedMessage = $"Project data deleted for user: {currentUser.UserName}.";
			projectsLog.AdditionalData = $"User deleted their project data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", projectsLog);
			#endregion
		}
	}
}