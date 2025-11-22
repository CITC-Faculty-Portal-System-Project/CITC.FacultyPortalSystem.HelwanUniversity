using Domain.Entities.FacultyMemberDataModule;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class FacultyMemberWithEmailSpecifications : BaseSpecifications<FacultyMember, Guid>
    {
        public FacultyMemberWithEmailSpecifications(string email) : base(fm => fm.Email == email)
        {

        }
    }
}
