using Domain.Contracts;
using Domain.Entities.CVGenerationModule;
using Domain.Entities.FacultyMemberDataModule;
using Microsoft.Extensions.Logging;
using Services.Specifications.CVGenerationModule;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Enums.Logging;

namespace Services.Implementations
{
	public class ProfileDashboardService(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IAuthenticationService _authenticationService,
        ILogger<ProfileDashboardService> _logger) : IProfileDashboardService
    {
        #region Helper Methods
        private async Task<UserResultDto> GetCurrentUserAsync()
        {
            var userEmail = _authenticationService.GetLoggedUserEmail();
            return await _authenticationService.GetCurrentUserAsync(userEmail)
                ?? throw new UnauthorizedAccessException("Unauthorized.");
        }

        protected static void EnsureOwnership(
            Guid entityFacultyMemberId,
            Guid currentUserId,
            string? entityNameOverride = null)
        {
            if (entityFacultyMemberId != currentUserId)
				throw new UnauthorizedAccessException(
                    $"You do not have permission to access this {(entityNameOverride ?? "resource")}."
                );
        }
        #endregion

        public async Task<BioSummaryDTO> UpdateBioSummaryAsync(BioSummaryDTO bioSummaryDTO)
        {
            var bioSummaryLog = new LogEntry
            {
                Category = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.ProfileDashboardDataActions.ToString()
            };

            var currentUser = await GetCurrentUserAsync();

            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

            var personalData = await personalDataRepo.GetAsync(new PersonalDataWithFacultyMemberIdSpecifications(currentUser.UserId));
            if (personalData is null)
            {
                #region Log
                bioSummaryLog.Timestamp = DateTime.Now;
                bioSummaryLog.RenderedMessage = $"Personal data not found for user: {currentUser.UserName}";
                bioSummaryLog.Level = "Warning";
                bioSummaryLog.UserIP = _authenticationService.GetUserIP();
                bioSummaryLog.UserName = currentUser.UserName;
                bioSummaryLog.AdditionalData = $"User tried to get their personal data, but no personal data was found in the database for user with email : {currentUser.Email}";
                _logger.LogWarning("{@LogDetails}", bioSummaryLog);
                #endregion
                throw new NotFoundException($"Personal data not found.");
            } 
            var oldBioSummary = personalData.BioSummary;

            try
            {
                EnsureOwnership(personalData.FacultyMemberId, currentUser.UserId, "bio summary");
            }
            catch (Exception ex)
            {
                #region Log
                var ensureOwnershipLog = new LogEntry
                {
                    Category = Category.FacultyMemberService.ToString(),
                    CategoryAction = CategoryAction.EnsureOwnership.ToString(),
                    Level = "Error",
                    UserIP = _authenticationService.GetUserIP(),
                    UserName = currentUser.UserName,
                    RenderedMessage = "User unauthorized to update profile bio summary.",
                    AdditionalData = $"User tried to update profile bio summary that does not belong to them. profile data faculty member id: {personalData.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
                    ExceptionMessage = ex.Message,
					Exception = ex.ToString(),
					ExceptionDetail = ex.StackTrace,
					Timestamp = DateTime.Now
                };
                _logger.LogError("{@LogDetails}", ensureOwnershipLog);
                #endregion
                throw;
            }

            personalData.BioSummary = bioSummaryDTO.BioSummary;

            personalDataRepo.Update(personalData);

            await _unitOfWork.SaveChangesAsync();

            #region Log
            bioSummaryLog.Timestamp = DateTime.Now;
            bioSummaryLog.Level = "Information";
            bioSummaryLog.UserIP = _authenticationService.GetUserIP();
            bioSummaryLog.UserName = currentUser.UserName;
            bioSummaryLog.RenderedMessage = "User updated thier profile bio summary.";
            bioSummaryLog.AdditionalData = $"User updated thier bio from: {oldBioSummary} to {personalData.BioSummary} successfully";
            _logger.LogInformation("{@LogDetails}", bioSummaryLog);
            #endregion
            return bioSummaryDTO;
        }

        public async Task<SkillsDTO> UpdateSkillAsync(SkillsDTO skillsDTO)
        {
            var skillsLog = new LogEntry
            {
                Category  = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.ProfileDashboardDataActions.ToString()
            };
            var currentUser = await GetCurrentUserAsync();

            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

            var personalData = await personalDataRepo.GetAsync(new PersonalDataWithFacultyMemberIdSpecifications(currentUser.UserId));
			if (personalData is null)
			{
				#region Log
				skillsLog.Timestamp = DateTime.Now;
				skillsLog.RenderedMessage = $"Personal data not found for user: {currentUser.UserName}";
				skillsLog.Level = "Warning";
				skillsLog.UserIP = _authenticationService.GetUserIP();
				skillsLog.UserName = currentUser.UserName;
				skillsLog.AdditionalData = $"User tried to get their personal data, but no personal data was found in the database for user with email : {currentUser.Email}";
				_logger.LogWarning("{@LogDetails}", skillsLog);
				#endregion
				throw new NotFoundException($"Personal data not found.");
			}

            try
            {
                EnsureOwnership(personalData.FacultyMemberId, currentUser.UserId, "skills");
            }
            catch (Exception ex)
            {
				#region Log
				var ensureOwnershipLog = new LogEntry
				{
					Category = Category.FacultyMemberService.ToString(),
					CategoryAction = CategoryAction.EnsureOwnership.ToString(),
					Level = "Error",
					UserIP = _authenticationService.GetUserIP(),
					UserName = currentUser.UserName,
					RenderedMessage = "User unauthorized to update profile skills.",
					AdditionalData = $"User tried to update profile skill that does not belong to them. profile data faculty member id: {personalData.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
					ExceptionMessage = ex.Message,
                    Exception = ex.ToString(),
                    ExceptionDetail = ex.StackTrace,  
					Timestamp = DateTime.Now
				};
				_logger.LogError("{@LogDetails}", ensureOwnershipLog);
				#endregion
				throw;
            }

            var normalizedSkills = skillsDTO.Skills?
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim().Replace(";", ""))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList() ?? new List<string>();

            personalData.Skills = normalizedSkills.Any()
                ? string.Join(";", normalizedSkills)
                : string.Empty;

            personalDataRepo.Update(personalData);
            await _unitOfWork.SaveChangesAsync();
            #region Log
            skillsLog.Timestamp = DateTime.Now;
            skillsLog.Level = "Information";
            skillsLog.UserIP = _authenticationService.GetUserIP();
            skillsLog.UserName = currentUser.UserName;
            skillsLog.RenderedMessage = "User updated thier profile skills";
			skillsLog.AdditionalData = $"User updated thier profile skills to {personalData.Skills} successfully";
            _logger.LogInformation("{@LogDetails}", skillsLog);
			#endregion
			return skillsDTO;
        }

        public async Task<ProfileDashboardResponseDTO> GetProfileDashboardAsync()
        {
            var cvRepo = _unitOfWork.GetRepository<SavedCVPreferences, int>();

            #region Old Code
            //var currentUser = await GetCurrentUserAsync();

            //var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

            //var personalDataTask = personalDataRepo.GetAsync(new PersonalDataWithIncludesSpecifications(currentUser.Email));

            //var researchCountTask = _unitOfWork.GetRepository<Research, int>()
            //    .CountAsync(new ResearchCountSpecifications(currentUser.UserId));

            //var prizesAndRewardsCountTask = _unitOfWork.GetRepository<PrizesAndRewards, int>()
            //    .CountAsync(new PrizesAndRewardsCountSpecifications(currentUser.UserId));

            //var scientificWritingsCountTask = _unitOfWork.GetRepository<ScientificWritings, int>()
            //    .CountAsync(new ScientificWritingsCountSpecifications(currentUser.UserId));

            //var projectsCountTask = _unitOfWork.GetRepository<Projects, int>()
            //    .CountAsync(new ProjectsCountSpecifications(currentUser.UserId));

            //var generalExperiencesTask = _unitOfWork.GetRepository<GeneralExperiences, int>()
            //    .GetAllAsync(new GeneralExperiencesSpecifications(currentUser.UserId));

            //var teachingExperiencesTask = _unitOfWork.GetRepository<TeachingExperiences, int>()
            //    .GetAllAsync(new TeachingExperiencesSpecifications(currentUser.UserId));

            //var academicQualificationsTask = _unitOfWork.GetRepository<AcademicQualifications, int>()
            //    .GetAllAsync(new AcademicQualificationsCountSpecifications(currentUser.UserId));

            //var contributionsToUniversityCountTask = _unitOfWork.GetRepository<ContributionsToUniversity, int>()
            //    .CountAsync(new ContributionsToUniversityCountSpecifications(currentUser.UserId));

            //var ContributionsToCommunityServiceCountTask = _unitOfWork.GetRepository<ContributionsToCommunityService, int>()
            //    .CountAsync(new ContributionsToCommunityServiceCountSpecifications(currentUser.UserId));

            //var ParticipationInQualityWorksCountTask = _unitOfWork.GetRepository<ParticipationInQualityWorks, int>()
            //    .CountAsync(new ParticipationInQualityWorksCountSpecifications(currentUser.UserId));

            //await Task.WhenAll(
            //    personalDataTask,
            //    researchCountTask,
            //    prizesAndRewardsCountTask,
            //    scientificWritingsCountTask,
            //    projectsCountTask,
            //    generalExperiencesTask,
            //    teachingExperiencesTask,
            //    academicQualificationsTask,
            //    contributionsToUniversityCountTask,
            //    ContributionsToCommunityServiceCountTask,
            //    ParticipationInQualityWorksCountTask
            //);

            //var generalExperiences = (await generalExperiencesTask)
            //    .Select(ge => new ExperiencesSummaryDTO
            //    {
            //        Title = ge.ExperienceTitle,
            //        Organization = ge.Authority,
            //        StartDate = ge.StartDate,
            //        EndDate = ge.EndDate
            //    });

            //var teachingExperiences = (await teachingExperiencesTask)
            //    .Select(te => new ExperiencesSummaryDTO
            //    {
            //        Title = te.CourseName,
            //        Organization = te.UniversityOrFaculty,
            //        StartDate = te.StartDate,
            //        EndDate = te.EndDate
            //    });

            //var topExperiences = generalExperiences
            //    .Concat(teachingExperiences)
            //    .OrderByDescending(x => x.StartDate)
            //    .Take(3)
            //    .ToList();

            //var academicQualifications = (await academicQualificationsTask)
            //    .Select(aq => new AcademicQualificationsSummaryDTO
            //    {
            //        Qualification = _mapper.Map<LookupItemDto>(aq.Qualification),
            //        Specialization = aq.Specialization,
            //        UniversityOrFaculty = aq.UniversityOrFaculty,
            //        DateOfObtainingTheQualification = aq.DateOfObtainingTheQualification
            //    });

            //var topAcademicQualifications = academicQualifications
            //    .OrderByDescending(aq => aq.DateOfObtainingTheQualification)
            //    .Take(3)
            //    .ToList();

            //var personalData = await personalDataTask
            //    ?? throw new NotFoundException($"Personal data not found for {currentUser.Email}.");

            //var response = _mapper.Map<ProfileDashboardResponseDTO>(personalData);

            //if (personalData.FacultyMember?.SocialMediaPlatforms != null)
            //{
            //    var sm = personalData.FacultyMember.SocialMediaPlatforms;
            //    response.LinkedIn = sm.LinkedIn;
            //    response.Facebook = sm.Facebook;
            //    response.Instagram = sm.Instagram;
            //    response.YouTube = sm.YouTube;
            //    response.X = sm.X;
            //    response.GoogleScholar = sm.GoogleScholar;
            //    response.Scopus = sm.Scopus;
            //    response.PersonalWebsite = sm.PersonalWebsite;
            //}

            //response.ResearchCount = await researchCountTask;
            //response.PrizesAndRewardsCount = await prizesAndRewardsCountTask;
            //response.ScientificWritingsCount = await scientificWritingsCountTask;
            //response.ProjectsCount = await projectsCountTask;
            //response.ContributionsCount = (await contributionsToUniversityCountTask)
            //                            + (await ContributionsToCommunityServiceCountTask)
            //                            + (await ParticipationInQualityWorksCountTask);

            //response.TopExperiences = topExperiences;
            //response.TopAcademicQualifications = topAcademicQualifications;

            //return response;

            #endregion

            var currentUser = await GetCurrentUserAsync();
            var profileLog = new LogEntry
			{
				Category = Category.FacultyMemberService.ToString(),
				CategoryAction = CategoryAction.ProfileDashboardDataActions.ToString()
			};
			var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

            var personalData = await personalDataRepo.GetAsync(new ProfilePageSpecification(currentUser.Email));
			if (personalData is null)
			{
				#region Log
				profileLog.Timestamp = DateTime.Now;
				profileLog.RenderedMessage = $"Personal data not found for user: {currentUser.UserName}";
				profileLog.Level = "Warning";
				profileLog.UserIP = _authenticationService.GetUserIP();
				profileLog.UserName = currentUser.UserName;
				profileLog.AdditionalData = $"User tried to get their personal data, but no personal data was found in the database for user with email : {currentUser.Email}";
				_logger.LogWarning("{@LogDetails}", profileLog);
				#endregion
				throw new NotFoundException($"Personal data not found.");
			}

            var response = _mapper.Map<ProfileDashboardResponseDTO>(personalData);
            response.PersonalDataId = personalData.Id;
            response.Department = new LookupItemDto();
            response.Department.ValueAr = personalData.Department?.NameAR ?? string.Empty;
            response.Department.ValueEn = personalData.Department?.NameEN ?? string.Empty;


            if (personalData.FacultyMember!.SocialMediaPlatforms != null)
            {
                var sm = personalData.FacultyMember!.SocialMediaPlatforms;
                response.LinkedIn = sm.LinkedIn;
                response.Facebook = sm.Facebook;
                response.Instagram = sm.Instagram;
                response.YouTube = sm.YouTube;
                response.X = sm.X;
                response.PersonalWebsite = sm.PersonalWebsite;
            }

            var cv = await cvRepo.GetAsync(new CVPrefferedTemplateSpecification(currentUser.UserId));
            if (cv is not null)
                response.PrefferdCVTempate = cv.TemplateName;


            response.ResearchCount = personalData.FacultyMember!.ResearchContributions?.Count ?? 0;
            response.PrizesAndRewardsCount = personalData.FacultyMember!.PrizesAndRewards.Count;
            response.ScientificWritingsCount = personalData.FacultyMember!.ScientificWritings.Count;
            response.ProjectsCount = personalData.FacultyMember!.Projects.Count;

            response.ContributionsCount =
                personalData.FacultyMember!.ContributionsToUniversity.Count
                + personalData.FacultyMember!.ContributionsToCommunityServices.Count
                + personalData.FacultyMember!.ParticipationInQualityWorks.Count;

            var generalExperiences = personalData.FacultyMember!.GeneralExperiences
                .Select(ge => new ExperiencesSummaryDTO
                {
                    Title = ge.ExperienceTitle,
                    Organization = ge.Authority,
                    StartDate = ge.StartDate,
                    EndDate = ge.EndDate
                });

            var teachingExperiences = personalData.FacultyMember!.TeachingExperiences
                .Select(te => new ExperiencesSummaryDTO
                {
                    Title = te.CourseName,
                    Organization = te.UniversityOrFaculty,
                    StartDate = te.StartDate,
                    EndDate = te.EndDate
                });

            response.TopExperiences = generalExperiences
                .Concat(teachingExperiences)
                .OrderByDescending(x => x.StartDate)
                .Take(3)
                .ToList();

            response.TopAcademicQualifications = personalData.FacultyMember!.AcademicQualifications
                .OrderByDescending(aq => aq.DateOfObtainingTheQualification)
                .Take(3)
                .Select(aq => new AcademicQualificationsSummaryDTO
                {
                    Qualification = _mapper.Map<LookupItemDto>(aq.Qualification),
                    Specialization = aq.Specialization,
                    UniversityOrFaculty = aq.UniversityOrFaculty,
                    DateOfObtainingTheQualification = aq.DateOfObtainingTheQualification
                })
                .ToList();

            #region Log
            profileLog.Timestamp = DateTime.Now;
			profileLog.Level = "Information";
			profileLog.UserIP = _authenticationService.GetUserIP();
			profileLog.UserName = currentUser.UserName;
			profileLog.RenderedMessage = $"Profile dashboard retrieved for user: {currentUser.UserName}.";
			profileLog.AdditionalData = $"User retrieved their profile dashboard data successfully.";
			_logger.LogInformation("{@LogDetails}", profileLog);
			#endregion
			return response;
        }
    }
}
