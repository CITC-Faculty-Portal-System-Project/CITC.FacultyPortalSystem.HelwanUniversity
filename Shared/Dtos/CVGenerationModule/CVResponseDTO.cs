using Shared.Dtos.CVGenerationModule.Contributions;
using Shared.Dtos.CVGenerationModule.Experiences;
using Shared.Dtos.CVGenerationModule.Missions;
using Shared.Dtos.CVGenerationModule.Prizes;
using Shared.Dtos.CVGenerationModule.ProjectsAndCommittees;
using Shared.Dtos.CVGenerationModule.ScientificProgression;
using Shared.Dtos.CVGenerationModule.WritingsAndPatents;
using Shared.Dtos.FacultyMemberDataModule;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.CVGenerationModule
{
    public record CVResponseDTO
    {
        public int PersonalDataId { get; set; }
        #region Personal Data
        //public ? ProfilePictureId { get; set; }
        public LookupItemDto? Title { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public LookupItemDto? University { get; set; } 
        public LookupItemDto? Authority { get; set; } 
        public LookupItemDto? Department { get; set; } 
        public DateOnly? BirthDate { get; set; }
        public string? BioSummary { get; set; }
        public List<string>? Skills { get; set; }
        public Guid? ProfilePictureId { get; set; }
        #endregion

        #region SocialMedia Links
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }
        public string? PersonalWebsite { get; set; }
        public string? GoogleScholar { get; set; }
        public string? Scopus { get; set; }
        public string? Facebook { get; set; }
        public string? X { get; set; }
        public string? YouTube { get; set; }
        #endregion

        #region Contact Data
        public string? MainPhoneNumber { get; set; } = string.Empty;
        public string? WorkPhoneNumber { get; set; }
        public string? OfficialEmail { get; set; } = string.Empty;
        public string? FaxNumber { get; set; }
        #endregion

        #region Scientific Progression 
        public List<CVAcademicQualificationsDTO> AcademicQualifications { get; set; } = new();
        public List<CVJobRanksDTO> JobRanks { get; set; } = new();
        public List<CVAdministrativePositions> AdministrativePositions { get; set; } = new();
        #endregion

        #region Missions 
        public List<CVConferencesAndSeminarsDTO> ConferencesAndSeminars { get; set; } = new();
        public List<CVScientificMissionsDTO> ScientificMissions { get; set; } = new();
        public List<CVTrainingProgramsDTO> TrainingPrograms { get; set; } = new();
        #endregion 

        #region ProjectsAndCommittees 
        public List<CVCommitteesAndAssociationsDTO> CommitteesAndAssociations { get; set; } = new();
        public List<CVParticipationInMagazinesDTO> ParticipationInMagazines { get; set; } = new();
        public List<CVReviewingArticlesDTO> ReviewingArticles { get; set; } = new();
        public List<CVProjectsDTO> Projects { get; set; } = new();
        #endregion

        #region Experiences 
        public List<CVGeneralExperienceDTO> GeneralExperiences { get; set; } = new();
        public List<CVTeachingExperienceDTO> TeachingExperiences { get; set; } = new();
        #endregion

        #region WritingsAndPatents 
        public List<CVScientificWritingDTO> ScientificWritings { get; set; } = new();
        public List<CVPatentDTO> Patents { get; set; } = new();
        #endregion

        #region Prizes
        public List<CVPrizesAndRewardsDTO> PrizesAndRewards { get; set; } = new();
        public List<CVManifestationsOfScientificAppreciationDTO> ManifestationsOfScientificAppreciation { get; set; } = new();
        #endregion

        #region Contributions
        public List<CVContributionsToCommunityServiceDTO> ContributionsToCommunityService { get; set; } = new();
        public List<CVContributionsToUniversityDTO> ContributionsToUniversity { get; set; } = new();
        public List<CVParticipationInQualityWorkDTO> ParticipationInQualityWork { get; set; } = new();
        #endregion
    }
}
