using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Microsoft.EntityFrameworkCore;

namespace Services.Specifications.CVGenerationModule
{
    internal class CVSpecifications : BaseSpecifications<PersonalData, int>
    {
        public CVSpecifications
            (string email)
            : base(cv => cv.FacultyMember != null && cv.FacultyMember.Email == email)
        {
            IncludePersonalData();
            IncludeFacultyContact();
            IncludeAcademicQualifications();
            IncludeJobRanks();
            IncludeAdministrativePositions();
            IncludeTrainingPrograms();
            IncludeScientificMissions();
            IncludeConferencesAndSeminars();
            IncludeCommitteesAndAssociations();
            IncludeParticipationInMagazines();
            IncludeReviewingArticles();
            IncludeProjects();
            IncludeGeneralExperiences();
            IncludeTeachingExperiences();
            IncludeScientificWritings();
            IncludePatents();
            IncludePrizesAndRewards();
            IncludeManifestationsOfScientificAppreciations();
            IncludeParticipationInQualityWorks();
            IncludeContributionsToCommunityServices();
            IncludeContributionsToUniversity();

            EnableSplitQuery();
        }

        #region Personal Data
        private void IncludePersonalData()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.University)
                .Include(cv => cv.Title)
                .Include(cv => cv.Authority)
                .Include(cv => cv.Department));
        }
        #endregion

        #region SocialMedia and ContactInfo
        private void IncludeFacultyContact()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.SocialMediaPlatforms)
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ContactData));
        }
        #endregion

        #region Scientific Progression
        private void IncludeAcademicQualifications()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.AcademicQualifications)
                        .ThenInclude(aq => aq.Qualification)

                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.AcademicQualifications)
                        .ThenInclude(aq => aq.Grade)

                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.AcademicQualifications)
                        .ThenInclude(aq => aq.DispatchType));
        }

        private void IncludeJobRanks()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.JobRanks)
                        .ThenInclude(jr => jr.JobRank)
            );
        }

        private void IncludeAdministrativePositions()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.AdministrativePositions)
            );
        }
        #endregion

        #region Missions
        private void IncludeConferencesAndSeminars()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ConferencesAndSeminars)
                    .ThenInclude(cs => cs.RoleOfParticipation)
            );
        }

        private void IncludeScientificMissions()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ScientificMissions)
            );
        }

        private void IncludeTrainingPrograms()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.TrainingPrograms)
            );
        }
        #endregion

        #region ProjectsAndCommittees
        private void IncludeCommitteesAndAssociations()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.CommitteesAndAssociations)
                    .ThenInclude(ca => ca.DegreeOfSubscription)

                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.CommitteesAndAssociations)
                    .ThenInclude(ca => ca.TypeOfCommitteeOrAssociation)
            );
        }

        private void IncludeParticipationInMagazines()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ParticipationInMagazines)
                    .ThenInclude(pm => pm.TypeOfParticipation)
            );
        }

        private void IncludeReviewingArticles()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ReviewingArticles)
            );
        }

        private void IncludeProjects()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.Projects)
                    .ThenInclude(p => p.ParticipationRole)

                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.Projects)
                    .ThenInclude(p => p.TypeOfProject)
            );
        }
        #endregion

        #region Experiences
        private void IncludeTeachingExperiences()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.TeachingExperiences)
            );
        }

        private void IncludeGeneralExperiences()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.GeneralExperiences)
            );
        }
        #endregion

        #region WritingsAndPatents
        private void IncludeScientificWritings()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ScientificWritings)
                    .ThenInclude(sw => sw.AuthorRole)
            );
        }

        private void IncludePatents()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.Patents)
            );
        }
        #endregion

        #region Prizes
        private void IncludePrizesAndRewards()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.PrizesAndRewards)
            );
        }

        private void IncludeManifestationsOfScientificAppreciations()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ManifestationsOfScientificAppreciations)
            );
        }
        #endregion

        #region Contributions
        private void IncludeContributionsToCommunityServices()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ContributionsToCommunityServices)
            );
        }

        private void IncludeContributionsToUniversity()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ContributionsToUniversity)
                    .ThenInclude(cu => cu.TypeOfContribution)
            );
        }

        private void IncludeParticipationInQualityWorks()
        {
            AddIncludeWithChain(x => x
                .Include(cv => cv.FacultyMember!)
                    .ThenInclude(fm => fm.ParticipationInQualityWorks)
            );
        }
        #endregion
    }
}
