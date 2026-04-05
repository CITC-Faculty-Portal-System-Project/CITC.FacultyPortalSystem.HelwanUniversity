using Domain.Entities.CVGenerationModule;
using Services.Abstraction.Contracts.CVGenerationModule;
using Services.Implementations.CVGenerationModule.Factories;
using Services.Specifications.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Dtos.CVGenerationModule.Contributions;
using Shared.Dtos.CVGenerationModule.Experiences;
using Shared.Dtos.CVGenerationModule.Missions;
using Shared.Dtos.CVGenerationModule.Prizes;
using Shared.Dtos.CVGenerationModule.ProjectsAndCommittees;
using Shared.Dtos.CVGenerationModule.ScientificProgression;
using Shared.Dtos.CVGenerationModule.WritingsAndPatents;
using Shared.Models.CVGenerationModule;

namespace Services.Implementations.CVGenerationModule
{
    public class CVGenerationService(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IAuthenticationService _authenticationService,
        IEnumerable<ICVSectionVisibilityFilter> _visibilityFilters,
        CVTemplatesFactory _cVTemplatesFactory) : ICVGenerationService
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

        public async Task<CVVisibilitySettingResponseDTO> ManageCVVisibilityAsync(CVVisibilityConfig newConfig)
        {
            var currentUser = await GetCurrentUserAsync();

            var repo = _unitOfWork.GetRepository<CVVisibilitySettings, Guid>();

            var settings = await repo.GetAsync(new CVVisibilitySpecifications(currentUser.UserId));

            if (settings == null)
            {
                settings = new CVVisibilitySettings
                {
                    FacultyMemberId = currentUser.UserId
                };

                await repo.AddAsync(settings);
            }

            settings.VisibilityJson = CVVisibilityHelper.Serialize(newConfig);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CVVisibilitySettingResponseDTO>(settings);
        }

        private async Task<CVResponseDTO> BuildCVAsync(Guid facultyMemberId, string email)
        {
            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

            var personalData = await personalDataRepo.GetAsync(
                new CVSpecifications(email)
            ) ?? throw new NotFoundException($"Personal data not found for {email}.");

            var cvVisibilityRepo = _unitOfWork.GetRepository<CVVisibilitySettings, Guid>();

            var settings = await cvVisibilityRepo.GetAsync( new CVVisibilitySpecifications(facultyMemberId));

            if (settings == null)
            {
                settings = new CVVisibilitySettings
                {
                    FacultyMemberId = facultyMemberId,
                    VisibilityJson = CVVisibilityHelper.Serialize(new CVVisibilityConfig())
                };

                await cvVisibilityRepo.AddAsync(settings);
                await _unitOfWork.SaveChangesAsync();
            }

            var config = CVVisibilityHelper.Deserialize(settings.VisibilityJson);

            var response = _mapper.Map<CVResponseDTO>(personalData);

            if (personalData.FacultyMember!.SocialMediaPlatforms != null)
            {
                var sm = personalData.FacultyMember!.SocialMediaPlatforms;
                response.LinkedIn = sm.LinkedIn;
                response.Facebook = sm.Facebook;
                response.Instagram = sm.Instagram;
                response.YouTube = sm.YouTube;
                response.X = sm.X;
                response.GoogleScholar = sm.GoogleScholar;
                response.Scopus = sm.Scopus;
                response.PersonalWebsite = sm.PersonalWebsite;
            }

            if (personalData.FacultyMember!.ContactData != null)
            {
                var cd = personalData.FacultyMember!.ContactData;
                response.MainPhoneNumber = cd.MainPhoneNumber;
                response.WorkPhoneNumber = cd.WorkPhoneNumber;
                response.OfficialEmail = cd.OfficialEmail;
                response.FaxNumber = cd.FaxNumber;
            }

            response.AcademicQualifications = _mapper.Map<List<CVAcademicQualificationsDTO>>(
                personalData.FacultyMember!.AcademicQualifications
                    .OrderByDescending(aq => aq.DateOfObtainingTheQualification) 
            );

            response.JobRanks = _mapper.Map<List<CVJobRanksDTO>>(
                personalData.FacultyMember!.JobRanks
                    .OrderByDescending(jr => jr.DateOfJobRank) 
            );

            response.AdministrativePositions = _mapper.Map<List<CVAdministrativePositions>>(
                personalData.FacultyMember!.AdministrativePositions
                    .OrderByDescending(ap => ap.StartDate) 
            );

            response.ConferencesAndSeminars = _mapper.Map<List<CVConferencesAndSeminarsDTO>>(
                personalData.FacultyMember!.ConferencesAndSeminars
                    .OrderByDescending(cs => cs.StartDate) 
            );

            response.ScientificMissions = _mapper.Map<List<CVScientificMissionsDTO>>(
                personalData.FacultyMember!.ScientificMissions
                    .OrderByDescending(sm => sm.StartDate) 
            );

            response.TrainingPrograms = _mapper.Map<List<CVTrainingProgramsDTO>>(
                personalData.FacultyMember!.TrainingPrograms
                    .OrderByDescending(tp => tp.StartDate) 
            );

            response.CommitteesAndAssociations = _mapper.Map<List<CVCommitteesAndAssociationsDTO>>(
                personalData.FacultyMember!.CommitteesAndAssociations
                    .OrderByDescending(ca => ca.StartDate) 
            );

            response.ParticipationInMagazines = _mapper.Map<List<CVParticipationInMagazinesDTO>>(
                personalData.FacultyMember!.ParticipationInMagazines
                    .OrderByDescending(pm => pm.Id) 
            );

            response.ReviewingArticles = _mapper.Map<List<CVReviewingArticlesDTO>>(
                personalData.FacultyMember!.ReviewingArticles
                    .OrderByDescending(ra => ra.ReviewingDate) 
            );

            response.Projects = _mapper.Map<List<CVProjectsDTO>>(
                personalData.FacultyMember!.Projects
                    .OrderByDescending(p => p.StartDate) 
            );

            response.GeneralExperiences = _mapper.Map<List<CVGeneralExperienceDTO>>(
                personalData.FacultyMember!.GeneralExperiences
                    .OrderByDescending(ge => ge.StartDate) 
            );

            response.TeachingExperiences = _mapper.Map<List<CVTeachingExperienceDTO>>(
                personalData.FacultyMember!.TeachingExperiences
                    .OrderByDescending(te => te.StartDate) 
            );

            response.ScientificWritings = _mapper.Map<List<CVScientificWritingDTO>>(
                personalData.FacultyMember!.ScientificWritings
                    .OrderByDescending(sw => sw.PublishingDate) 
            );

            response.Patents = _mapper.Map<List<CVPatentDTO>>(
                personalData.FacultyMember!.Patents
                    .OrderByDescending(p => p.AccreditationDate) 
            );

            response.PrizesAndRewards = _mapper.Map<List<CVPrizesAndRewardsDTO>>(
                personalData.FacultyMember!.PrizesAndRewards
                    .OrderByDescending(pr => pr.DateReceived) 
            );

            response.ManifestationsOfScientificAppreciation = _mapper.Map<List<CVManifestationsOfScientificAppreciationDTO>>(
                personalData.FacultyMember!.ManifestationsOfScientificAppreciations
                    .OrderByDescending(msa => msa.DateOfAppreciation) 
            );

            response.ContributionsToCommunityService = _mapper.Map<List<CVContributionsToCommunityServiceDTO>>(
                personalData.FacultyMember!.ContributionsToCommunityServices
                    .OrderByDescending(ccs => ccs.DateOfContribution) 
            );

            response.ContributionsToUniversity = _mapper.Map<List<CVContributionsToUniversityDTO>>(
                personalData.FacultyMember!.ContributionsToUniversity
                    .OrderByDescending(ctu => ctu.DateOfContribution) 
            );

            response.ParticipationInQualityWork = _mapper.Map<List<CVParticipationInQualityWorkDTO>>(
                personalData.FacultyMember!.ParticipationInQualityWorks
                    .OrderByDescending(pqw => pqw.StartDate) 
            );

            foreach (var filter in _visibilityFilters.OrderBy(f => f.GetType().Name))
            {
                filter.Apply(response, config);
            }

            return response;
        }

        public async Task<CVResponseDTO> GetCVAsync()
        {
            var currentUser = await GetCurrentUserAsync();

            return await BuildCVAsync(currentUser.UserId, currentUser.Email);
        }

        public async Task<CVResponseDTO> GetPublicCVAsync(Guid id)
        {
            var facultyRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var faculty = await facultyRepo.GetAsync(new FacultyMemberWithIdSpecifications(id))
                ?? throw new NotFoundException("User not found");

            return await BuildCVAsync(faculty.Id, faculty.Email);
        }

        public async Task<byte[]> GenerateCVPdfAsync(string templateName)
        {
            var cv = await GetCVAsync();
            var template = _cVTemplatesFactory.Resolve(templateName);
            return template.GeneratePdf(cv);
        }

        public async Task<string> PreviewCVAsync(string templateName)
        {
            var cv = await GetCVAsync();
            var template = _cVTemplatesFactory.Resolve(templateName);
            return template.GenerateHtml(cv);
        }
    }
}
