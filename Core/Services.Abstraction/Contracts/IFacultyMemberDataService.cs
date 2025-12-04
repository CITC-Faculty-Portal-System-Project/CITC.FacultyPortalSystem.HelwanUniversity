using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Abstraction.Contracts
{
    public interface IFacultyMemberDataService
    {
        #region Personal Data
        public Task<PersonalDataResponseDto?> GetPersonalDataAsync();
        public Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(PersonalDataUpdateDto personalDataUpdateDto);
        #endregion

        #region Contact Data
        public Task<ContactDataResponseDto?> GetContactDataAsync();
        public Task<ContactDataResponseDto?> UpdateContactDataAsync(ContactDataUpdateDto contactDataUpdateDto);
        #endregion

        #region Identification Card
        public Task<IdentificationCardDto> GetIdentificationCardAsync();
        public Task<IdentificationCardDto> UpdateIdentificationCardAsync(IdentificationCardDto identificationCardDto);
        #endregion

        #region Social Media
        public Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync();
        public Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(SocialMediaPlatformsDto socialMediaPlatformsDto);
        #endregion
    }
}
