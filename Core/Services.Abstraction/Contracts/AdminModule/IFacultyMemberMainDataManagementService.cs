using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberMainDataManagementService
    {
        #region Personal Data

        Task<PersonalDataResponseDto?> GetMemberPersonalDataAsync(string facultyMemberEmail);

        Task<PersonalDataResponseDto?> UpdateMemberPersonalDataAsync(
            PersonalDataUpdateDto personalDataUpdateDto,
            string facultyMemberEmail);

        #endregion

        #region Contact Data

        Task<ContactDataResponseDto?> GetMemberContactDataAsync(string facultyMemberEmail);

        Task<ContactDataResponseDto?> UpdateMemberContactDataAsync(
            ContactDataUpdateDto contactDataUpdateDto,
            string facultyMemberEmail);

        #endregion

        #region Identification Card

        Task<IdentificationCardDto> GetMemberIdentificationCardAsync(string facultyMemberEmail);

        Task<IdentificationCardDto> UpdateMemberIdentificationCardAsync(
            IdentificationCardDto identificationCardDto,
            string facultyMemberEmail);

        #endregion

        #region Social Media

        Task<SocialMediaPlatformsDto> GetMemberSocialMediaPlatformsAsync(string facultyMemberEmail);

        Task<SocialMediaPlatformsDto> UpdateMemberSocialMediaPlatformsAsync(
            SocialMediaPlatformsDto socialMediaPlatformsDto,
            string facultyMemberEmail);

        #endregion
    }
}
