using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class ProfilePageSpecification : BaseSpecifications<PersonalData, int>
    {
        public ProfilePageSpecification
            (string email) 
            :base(pd => pd.FacultyMember != null && pd.FacultyMember.Email == email)
        {
            AddIncludeWithChain(q => q
              .Include(pd => pd.FacultyMember!)
                  .ThenInclude(fm => fm.SocialMediaPlatforms)


              .Include(q => q.ProfilePicture)
                .Include(pd => pd.University)
                .Include(pd => pd.Authority)
                .Include(pd => pd.Gender)
                .Include(pd => pd.Title)
                .Include(pd => pd.MaritalStatus)
                .Include(pd => pd.Department)
                .Include(pd => pd.Field)

                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.ResearchContributions!)
                        .ThenInclude(rc => rc.Research)


                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.PrizesAndRewards)

                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.ScientificWritings)

                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.Projects)

                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.GeneralExperiences)

                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.TeachingExperiences)

                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.AcademicQualifications)
                        .ThenInclude(aq => aq.Qualification)

                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.ContributionsToUniversity)

                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.ContributionsToCommunityServices)

                .Include(pd => pd.FacultyMember!)
                    .ThenInclude(fm => fm.ParticipationInQualityWorks)

            );

            EnableSplitQuery();
        }
    }
}
