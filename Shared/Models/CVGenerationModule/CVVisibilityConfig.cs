using Shared.Models.CVGenerationModule.Contributions;
using Shared.Models.CVGenerationModule.Experiences;
using Shared.Models.CVGenerationModule.Missions;
using Shared.Models.CVGenerationModule.Prizes;
using Shared.Models.CVGenerationModule.ProjectsAndCommittees;
using Shared.Models.CVGenerationModule.ScientificProgression;
using Shared.Models.CVGenerationModule.WritingsAndPatents;

namespace Shared.Models.CVGenerationModule
{
    public class CVVisibilityConfig
    {
        public PersonalDataVisibility PersonalData { get; set; } = new();
        public ContactInfoVisibility ContactInfo { get; set; } = new();
        public SocialMediaVisibility SocialMedia { get; set; } = new();

        #region Scientific Progression
        public AcademicQualificationVisibility AcademicQualifications { get; set; } = new();
        public JobRanksVisibility JobRanks { get; set; } = new();
        public AdministrativePositionsVisibility AdministrativePositions { get; set; } = new();
        #endregion

        #region Missions
        public ConferencesAndSeminarsVisibility ConferencesAndSeminars { get; set; } = new();
        public ScientificMissionsVisibility ScientificMissions { get; set; } = new();
        public TrainingProgramsVisibility TrainingPrograms { get; set; } = new();
        #endregion

        #region ProjectsAndCommittees
        public CommitteesAndAssociationsVisibility CommitteesAndAssociations { get; set; } = new();
        public ParticipationInMagazinesVisibility ParticipationInMagazines { get; set; } = new();
        public ReviewingArticlesVisibility ReviewingArticles { get; set; } = new();
        public ProjectsVisibility Projects { get; set; } = new();
        #endregion

        #region Experiences
        public TeachingExperienceVisibility TeachingExperiences { get; set; } = new();
        public GeneralExperienceVisibility GeneralExperiences { get; set; } = new();
        #endregion

        #region WritingsAndPatents
        public ScientificWritingVisibility ScientificWritings { get; set; } = new();
        public PatentVisibility Patents { get; set; } = new();
        #endregion

        #region Prizes
        public PrizesAndRewardsVisibility PrizesAndRewards { get; set; } = new();
        public ManifestationsOfScientificAppreciationVisibility ManifestationsOfScientificAppreciation { get; set; } = new();
        #endregion

        #region Contributions
        public ContributionsToCommunityServiceVisibility ContributionsToCommunityService { get; set; } = new();
        public ContributionsToUniversityVisibility ContributionsToUniversity { get; set; } = new();
        public ParticipationInQualityWorkVisibility ParticipationInQualityWork { get; set; } = new();
        #endregion
    }
}
