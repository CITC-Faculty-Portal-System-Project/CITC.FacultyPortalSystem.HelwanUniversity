using Domain.Entities.FacultyMemberDataModule;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class FacultyMemberWithEmailSPecifications : BaseSpecifications<FacultyMember, Guid>
    {
        public FacultyMemberWithEmailSPecifications(string email) : base(fm => fm.Email == email)
        {

        }
    }
}
