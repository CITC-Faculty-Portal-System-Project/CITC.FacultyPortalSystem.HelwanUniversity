using Domain.Entities.AcademicDataModule.ContributionsModule;
using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Domain.Entities.AcademicDataModule.PrizesModule;
using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Services.Specifications.AcademicDataModule.ExperiencesModule;
using Services.Specifications.AcademicDataModule.PrizesModule;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Services.Specifications.AcademicDataModule.WritingsAndPatentsModule;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Implementations
{
    public class ProfileDashboardService(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IAuthenticationService _authenticationService) : IProfileDashboardService
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
            var currentUser = await GetCurrentUserAsync();

            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

            var personalData = await personalDataRepo.GetAsync(new PersonalDataWithFacultyMemberIdSpecifications(currentUser.UserId))
                ?? throw new NotFoundException($"Personal data not found.");

            EnsureOwnership(personalData.FacultyMemberId, currentUser.UserId, "bio summary");

            personalData.BioSummary = bioSummaryDTO.BioSummary;

            personalDataRepo.Update(personalData);

            await _unitOfWork.SaveChangesAsync();

            return bioSummaryDTO;
        }

        public async Task<SkillsDTO> UpdateSkillAsync(SkillsDTO skillsDTO)
        {
            var currentUser = await GetCurrentUserAsync();

            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

            var personalData = await personalDataRepo.GetAsync(new PersonalDataWithFacultyMemberIdSpecifications(currentUser.UserId))
                ?? throw new NotFoundException($"Personal data not found.");

            EnsureOwnership(personalData.FacultyMemberId, currentUser.UserId, "skills");

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

            return skillsDTO;
        }

        public async Task<ProfileDashboardResponseDTO> GetProfileDashboardAsync()
        {
            var currentUser = await GetCurrentUserAsync();

            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

            var personalDataTask = personalDataRepo.GetAsync(new PersonalDataWithIncludesSpecifications(currentUser.Email));
            
            var researchCountTask = _unitOfWork.GetRepository<Research, int>()
                .CountAsync(new ResearchCountSpecifications(currentUser.UserId));

            var prizesAndRewardsCountTask = _unitOfWork.GetRepository<PrizesAndRewards, int>()
                .CountAsync(new PrizesAndRewardsCountSpecifications(currentUser.UserId));

            var scientificWritingsCountTask = _unitOfWork.GetRepository<ScientificWritings, int>()
                .CountAsync(new ScientificWritingsCountSpecifications(currentUser.UserId));

            var projectsCountTask = _unitOfWork.GetRepository<Projects, int>()
                .CountAsync(new ProjectsCountSpecifications(currentUser.UserId));

            var generalExperiencesTask = _unitOfWork.GetRepository<GeneralExperiences, int>()
                .GetAllAsync(new GeneralExperiencesSpecifications(currentUser.UserId));

            var teachingExperiencesTask = _unitOfWork.GetRepository<TeachingExperiences, int>()
                .GetAllAsync(new TeachingExperiencesSpecifications(currentUser.UserId));

            var academicQualificationsTask = _unitOfWork.GetRepository<AcademicQualifications, int>()
                .GetAllAsync(new AcademicQualificationsCountSpecifications(currentUser.UserId));

            var contributionsToUniversityCountTask = _unitOfWork.GetRepository<ContributionsToUniversity, int>()
                .CountAsync(new ContributionsToUniversityCountSpecifications(currentUser.UserId));

            var ContributionsToCommunityServiceCountTask = _unitOfWork.GetRepository<ContributionsToCommunityService, int>()
                .CountAsync(new ContributionsToCommunityServiceCountSpecifications(currentUser.UserId));

            var ParticipationInQualityWorksCountTask = _unitOfWork.GetRepository<ParticipationInQualityWorks, int>()
                .CountAsync(new ParticipationInQualityWorksCountSpecifications(currentUser.UserId));

            await Task.WhenAll(
                personalDataTask,
                researchCountTask,
                prizesAndRewardsCountTask,
                scientificWritingsCountTask,
                projectsCountTask,
                generalExperiencesTask,
                teachingExperiencesTask,
                academicQualificationsTask,
                contributionsToUniversityCountTask,
                ContributionsToCommunityServiceCountTask,
                ParticipationInQualityWorksCountTask
            );

            var generalExperiences = (await generalExperiencesTask)
                .Select(ge => new ExperiencesSummaryDTO
                {
                    Title = ge.ExperienceTitle,
                    Organization = ge.Authority,
                    StartDate = ge.StartDate,
                    EndDate = ge.EndDate
                });

            var teachingExperiences = (await teachingExperiencesTask)
                .Select(te => new ExperiencesSummaryDTO
                {
                    Title = te.CourseName,
                    Organization = te.UniversityOrFaculty,
                    StartDate = te.StartDate,
                    EndDate = te.EndDate
                });

            var topExperiences = generalExperiences
                .Concat(teachingExperiences)
                .OrderByDescending(x => x.StartDate)
                .Take(3)
                .ToList();

            var academicQualifications = (await academicQualificationsTask)
                .Select(aq => new AcademicQualificationsSummaryDTO
                {
                    Qualification = _mapper.Map<LookupItemDto>(aq.Qualification),
                    Specialization = aq.Specialization,
                    UniversityOrFaculty = aq.UniversityOrFaculty,
                    DateOfObtainingTheQualification = aq.DateOfObtainingTheQualification
                });

            var topAcademicQualifications = academicQualifications
                .OrderByDescending(aq => aq.DateOfObtainingTheQualification)
                .Take(3)
                .ToList();

            var personalData = await personalDataTask
                ?? throw new NotFoundException($"Personal data not found for {currentUser.Email}.");

            var response = _mapper.Map<ProfileDashboardResponseDTO>(personalData);

            if (personalData.FacultyMember?.SocialMediaPlatforms != null)
            {
                var sm = personalData.FacultyMember.SocialMediaPlatforms;
                response.LinkedIn = sm.LinkedIn;
                response.Facebook = sm.Facebook;
                response.Instagram = sm.Instagram;
                response.YouTube = sm.YouTube;
                response.X = sm.X;
                response.GoogleScholar = sm.GoogleScholar;
                response.Scopus = sm.Scopus;
                response.PersonalWebsite = sm.PersonalWebsite;
            }

            response.ResearchCount = await researchCountTask;
            response.PrizesAndRewardsCount = await prizesAndRewardsCountTask;
            response.ScientificWritingsCount = await scientificWritingsCountTask;
            response.ProjectsCount = await projectsCountTask;
            response.ContributionsCount = (await contributionsToUniversityCountTask)
                                        + (await ContributionsToCommunityServiceCountTask)
                                        + (await ParticipationInQualityWorksCountTask);

            response.TopExperiences = topExperiences;
            response.TopAcademicQualifications = topAcademicQualifications;

            return response;
        }
    }
}
