using Domain.Entities.FacultyMemberDataModule;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class SocialMediaWithFacultyMemberEmailSpecifications : BaseSpecifications<SocialMediaPlatforms, int>
    {
        public SocialMediaWithFacultyMemberEmailSpecifications(string email) : base(sm => sm.FacultyMember != null && sm.FacultyMember.Email == email)
        {

        }
    }
}
