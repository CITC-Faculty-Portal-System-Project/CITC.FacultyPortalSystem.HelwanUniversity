using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Abstraction.Contracts
{
    public interface IFacultyMemberDataService
    {
        #region Personal Data
        public Task<PersonalDataResponseDto?> GetPersonalDataAsync(string facultyMemberEmail);
        public Task<PersonalDataResponseDto?> FetchPersonalDataAsync(PersonalDataCreateDto personalDataCreateDto);
        public Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(string facultyMemberEmail, PersonalDataUpdateDto personalDataUpdateDto);
        #endregion

        #region Contact Data
        public Task<ContactDataResponseDto?> GetContactDataAsync(string facultyMemberEmail);
        public Task<ContactDataResponseDto?> FetchContactDataAsync(string nationalNumber, ContactDataCreateDto contactDataCreateDto);
        public Task<ContactDataResponseDto?> UpdateContactDataAsync(string facultyMemberEmail, ContactDataUpdateDto contactDataUpdateDto);
        #endregion

        #region Identification Card
        public Task<IdentificationCardDto> GetIdentificationCardAsync(string facultyMemberEmail);
        public Task<IdentificationCardDto> UpdateIdentificationCardAsync(string facultyMemberEmail, IdentificationCardDto identificationCardDto);
        #endregion

        #region Social Media
        public Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync(string facultyMemberEmail);
        public Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(string facultyMemberEmail, SocialMediaPlatformsDto socialMediaPlatformsDto);
        #endregion
    }
}
