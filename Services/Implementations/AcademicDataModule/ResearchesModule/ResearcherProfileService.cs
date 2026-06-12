using Domain.Entities.AcademicDataModule.ResearchesModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.Enums.Logging;

namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
	public class ResearcherProfileService(
	   IUnitOfWork unitOfWork,
	   IMapper mapper,
	   IAuthenticationService authenticationService,
	   ILogger<ResearcherProfileService> _logger)
	   : BaseService<ResearcherProfile, int>(unitOfWork, authenticationService, mapper),
		 IResearcherProfileService
	{
		protected override string EntityName => "Researcher Profile";

		public async Task<ResearcherProfileResponseDTO> GetResearcherProfile(Guid? facultyMemberId = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var userOfData = (facultyMemberId is null) ? currentUser : await GetUserByIdAsync(targetFacultyMemberId);
			var researcherLog = new LogEntry
			{
				Category = Category.FacultyMemberResearches.ToString(),
				CategoryAction = CategoryAction.ResearcherProfileActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var profile = await Repo.GetAsync(
				new ResearcherProfileSpceification(targetFacultyMemberId));

			if (profile is not null)
			{
				try
				{
					await EnsureOwnershipIfClientAsync(
							profile.FacultyMemberId,
							facultyMemberId?.ToString());
				}
				catch (UnauthorizedAccessException)
				{
					#region Log
					researcherLog.Timestamp = DateTime.Now;
					researcherLog.Level = "Warning";
					researcherLog.RenderedMessage = $"User unauthorized to access researcher profile.";
					researcherLog.AdditionalData = $"User tried to get a researcher profile that does not belong to them, researcher profile faculty member id: {profile.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
					_logger.LogWarning("{@LogDetails}", researcherLog);
					#endregion
					throw;
				}
			}
			#region Log
			researcherLog.Timestamp = DateTime.Now;
			researcherLog.Level = "Information";
			researcherLog.RenderedMessage = $"Researcher profile data retrieved for user: {userOfData.UserName}.";
			researcherLog.AdditionalData = (facultyMemberId is null) ? $"User retrieved their researcher profile successfully."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} researcher profile.";
			_logger.LogInformation("{@LogDetails}", researcherLog);
			#endregion
			return Mapper.Map<ResearcherProfileResponseDTO>(profile);
		}
	}
}