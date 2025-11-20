using Domain.Entities.MissionsModule;
using Domain.Entities.ProjectsAndCommitteesModule;
using Domain.Entities.ScientificProgressionModule;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.FacultyMemberDataModule
{
    public class FacultyMember : BaseEntity<Guid>
    {
        public string NationalNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        #region Navigations

        #region FacultyMemberModule
        public PersonalData? PersonalData { get; set; }
        public ContactData? ContactData { get; set; }
        public IdentificationCard? IdentificationCard { get; set; }
        public SocialMediaPlatforms? SocialMediaPlatforms { get; set; }
        #endregion

        #region ScientificProgressionModule
        public ICollection<AcademicQualifications> AcademicQualifications { get; set; } = new HashSet<AcademicQualifications>();
        public ICollection<JobRanks> JobRanks { get; set; } = new HashSet<JobRanks>();
        public ICollection<AdministrativePositions> AdministrativePositions { get; set; } = new HashSet<AdministrativePositions>();

        #endregion

        #region MissionModule
        public ICollection<ConferencesAndSeminars> ConferencesAndSeminars { get; set; } = new HashSet<ConferencesAndSeminars>();
        public ICollection<ScientificMissions> ScientificMissions { get; set; } = new HashSet<ScientificMissions>();
        public ICollection<TrainingPrograms> TrainingPrograms { get; set; } = new HashSet<TrainingPrograms>();
        #endregion

        #region ProjectsAndCommitteesModule
        public ICollection<CommitteesAndAssociations> CommitteesAndAssociations { get; set; } = new HashSet<CommitteesAndAssociations>();
        public ICollection<ReviewingArticles> ReviewingArticles { get; set; } = new HashSet<ReviewingArticles>();
        public ICollection<ParticipationInMagazines> ParticipationInMagazines { get; set; } = new HashSet<ParticipationInMagazines>();
        public ICollection<Projects> Projects { get; set; } = new HashSet<Projects>();
        #endregion

        #endregion
    }
}
