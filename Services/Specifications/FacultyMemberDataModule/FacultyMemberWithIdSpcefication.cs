using System.Linq.Expressions;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class FacultyMemberWithIdSpcefication : BaseSpecifications<FacultyMember, Guid>
    {
        public FacultyMemberWithIdSpcefication
            (Guid id) : base(f => f.Id == id && !f.IsDeleted)
        {
        }
    }
}
