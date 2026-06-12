using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.AcademicDataModule.ScientificProgressionModule
{
    public class JobRanksService(
     IUnitOfWork unitOfWork,
     IAuthenticationService authenticationService,
     IMapper mapper)
     : BaseService<JobRanks, int>(unitOfWork, authenticationService, mapper),
       IJobRanksService
    {
        protected override string EntityName => "Job Ranks";

        public async Task<PaginatedResult<JobRankResponseDto>> GetAllAsync(
            JobRanksSpecificationsParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var jobRanks = await Repo.GetAllAsync(
                new JobRanksSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<JobRankResponseDto>>(jobRanks);

            var totalCount = await Repo.CountAsync(
                new JobRanksCountSpecifications(parameters, email));

            return new PaginatedResult<JobRankResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

		public async Task<JobRankResponseDto> GetByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var rankLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.JobRanksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id));
			if (jobRank is null)
			{
				#region Log
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"Job rank not found for user: {userOfData.UserName}.";
				rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their job rank data with id: {id}, but no job rank data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} job rank data with id: {id}, but no job rank data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw NotFound();
			}

            await EnsureOwnershipIfClientAsync(
                jobRank.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task<JobRankResponseDto> CreateAsync(
            JobRankCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var jobRank = Mapper.Map<JobRanks>(dto);
            jobRank.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(jobRank);
            await SaveChangesAsync();

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

		public async Task<JobRankResponseDto> UpdateAsync(
			int id,
			JobRankUpdateDto dto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var rankLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.JobRanksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id));
			if(jobRank is null)
			{
				#region Log
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"Contribution to community service not found for user: {userOfData.UserName}.";
				rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to update their contribution to community service data with id: {id}, but no contribution to community service data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to update user: {userOfData.UserName} contribution to community service data with id: {id}, but no contribution to community service data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw NotFound();
			}

            await EnsureOwnershipIfClientAsync(
                jobRank.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, jobRank);

            Repo.Update(jobRank);
            await SaveChangesAsync();

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

		public async Task DeleteAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var rankLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.JobRanksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id));
			if(jobRank is null)
			{
				#region Log
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"Job rank not found for user: {userOfData.UserName}.";
				rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to delete their job rank data with id: {id}, but no job rank data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to delete user: {userOfData.UserName} job rank data with id: {id}, but no job rank data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw NotFound();
			}

            await EnsureOwnershipIfClientAsync(
                jobRank.FacultyMemberId,
                facultyMemberEmail);

            jobRank.IsDeleted = true;

			Repo.Update(jobRank);
			await SaveChangesAsync();
			#region Log
			rankLog.Timestamp = DateTime.Now;
			rankLog.Level = "Information";
			rankLog.RenderedMessage = $"Job rank data deleted for user: {userOfData.UserName}.";
			rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User deleted their job rank data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} deleted user: {userOfData.UserName} job rank data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", rankLog);
			#endregion
		}
	}
}