using Domain.Entities.FacultyMemberDataModule;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class ContactDataWithFacultyMemberEmailSpecifications : BaseSpecifications<ContactData, int>
    {
        public ContactDataWithFacultyMemberEmailSpecifications(string email) : base(cd => cd.FacultyMember != null && cd.FacultyMember.Email == email)
        {
        }
    }
}
