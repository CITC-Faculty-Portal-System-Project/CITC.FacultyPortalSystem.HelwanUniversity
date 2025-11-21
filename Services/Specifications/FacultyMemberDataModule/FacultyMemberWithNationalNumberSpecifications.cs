using Domain.Entities.FacultyMemberDataModule;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class FacultyMemberWithNationalNumberSpecifications : BaseSpecifications<FacultyMember, Guid>
    {
        public FacultyMemberWithNationalNumberSpecifications(string nationalNumber) : base(fm => fm.NationalNumber == nationalNumber) { }
    }
}
