using Shared.Dtos.DataFetchingFromExternalService;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class ContactDataWithExternalServiceSpecification : BaseSpecifications<ContactData, int>
    {
        public ContactDataWithExternalServiceSpecification(ContactDataFetchingDTO contactDataFetchingDTO)
            : base
            (cd => cd.MainPhoneNumber == contactDataFetchingDTO.MainPhoneNumber &&
            cd.PersonalEmail == contactDataFetchingDTO.PersonalEmail && cd.OfficialEmail == contactDataFetchingDTO.OfficialEmail
            && cd.AlternativeEmail == contactDataFetchingDTO.AlternativeEmail && cd.Address == contactDataFetchingDTO.Address
            && cd.FaxNumber == contactDataFetchingDTO.FaxNumber && cd.HomePhoneNumber == contactDataFetchingDTO.HomePhoneNumber
            && cd.WorkPhoneNumber == contactDataFetchingDTO.WorkPhoneNumber && cd.FacultyMember.NationalNumber == contactDataFetchingDTO.NationalNumber)
        {
            AddIncludes(cd => cd.FacultyMember);
        }
    }
}
