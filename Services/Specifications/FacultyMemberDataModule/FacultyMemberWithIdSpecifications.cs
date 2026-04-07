using Domain.Entities.FacultyMemberDataModule;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class FacultyMemberWithIdSpecifications : BaseSpecifications<FacultyMember, Guid>
    {
        public FacultyMemberWithIdSpecifications(Guid id) : base(fm => fm.Id == id)
        {

        }
    }
}
