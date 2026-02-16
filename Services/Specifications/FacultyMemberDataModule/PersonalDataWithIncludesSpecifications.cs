using Domain.Entities.FacultyMemberDataModule;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.DataFetchingFromExternalService;

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

        public PersonalDataWithIncludesSpecifications(PersonalDataFetchingDTO dTO)
            : base(pd => pd.Gender.ValueAr == dTO.Gender &&
            pd.Title.ValueAr == dTO.Title && pd.MaritalStatus.ValueAr == dTO.SocialStatus
             && pd.BirthDate == dTO.BirthDate && pd.BirthPlace == dTO.BirthPlace
            && pd.NameInComposition == dTO.NameInCompositions && pd.CompositionTopics == dTO.CompositionTopics &&
            pd.Authority.ValueAr == dTO.FacultyName && pd.Field.ValueAr == dTO.FieldOfStudy && pd.Department.ValueAr == dTO.Department
            && pd.GeneralSpecialization == dTO.GeneralSpecialization && pd.AccurateSpecialization == dTO.AccurateSpecialization
            && pd.Name == dTO.Name && pd.FacultyMember.NationalNumber == dTO.NationalNumber)
        {
            AddIncludes(pd => pd.Gender);
            AddIncludes(pd => pd.Title);
            AddIncludes(pd => pd.MaritalStatus);
            AddIncludes(pd => pd.Authority);
            AddIncludes(pd => pd.Field);
            AddIncludes(pd => pd.Department);
            AddIncludes(aq => aq.FacultyMember!);


        }
    }
}
