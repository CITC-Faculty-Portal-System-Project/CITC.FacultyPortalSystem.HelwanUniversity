namespace Services.Specifications.FacultyMemberDataModule
{
    internal class PersonalDataWithFacultyMemberIdSpecifications : BaseSpecifications<PersonalData, int>
    {
        public PersonalDataWithFacultyMemberIdSpecifications(Guid facultyMemberId) : base(pd => pd.FacultyMemberId == facultyMemberId)
        {

        }
 
    }
}
