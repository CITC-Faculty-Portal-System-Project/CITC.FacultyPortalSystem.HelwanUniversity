using Domain.Entities.FacultyMemberDataModule;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class IdentificationCardWithFacultyMemberEmailSpecifications : BaseSpecifications<IdentificationCard, int>
    {
        public IdentificationCardWithFacultyMemberEmailSpecifications(string email) : base(ic => ic.FacultyMember != null && ic.FacultyMember.Email == email)
        {

        }
    }
}
