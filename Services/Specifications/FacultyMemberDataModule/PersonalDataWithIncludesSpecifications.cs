using Domain.Entities.FacultyMemberDataModule;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class PersonalDataWithIncludesSpecifications : BaseSpecifications<PersonalData, int>
    {
        // Get PersonalData ==> Criteria ==> FacultyMemberEmail ==> Includes (Lookup Items)
        public PersonalDataWithIncludesSpecifications(string email) : base(pd => pd.FacultyMember != null && pd.FacultyMember.Email == email)
        {
            AddIncludes(pd => pd.Title);
            AddIncludes(pd => pd.Gender);
            AddIncludes(pd => pd.MaritalStatus);
            AddIncludes(pd => pd.University);
            AddIncludes(pd => pd.Department);
            AddIncludes(pd => pd.Authority);
            AddIncludes(pd => pd.Field);

            AddIncludes(pd => pd.FacultyMember!);
        }

        public PersonalDataWithIncludesSpecifications(string email) : base(pd => pd.FacultyMember != null && pd.FacultyMember.Email == email)
        {
            AddIncludes(pd => pd.Title);
            AddIncludes(pd => pd.Gender);
            AddIncludes(pd => pd.MaritalStatus);
            AddIncludes(pd => pd.University);
            AddIncludes(pd => pd.Department);
            AddIncludes(pd => pd.Authority);
            AddIncludes(pd => pd.Field);

            AddIncludes(pd => pd.FacultyMember!);
        }
    }
}
