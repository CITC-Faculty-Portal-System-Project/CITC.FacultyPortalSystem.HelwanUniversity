using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Abstraction.Contracts
{
    public interface IFacultyMemberDataService
    {
        Task<PersonalDataResponseDto?> GetPersonalDataAsync(string? facultyMemberEmail = null);
        Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(
            PersonalDataUpdateDto personalDataUpdateDto,
            string? facultyMemberEmail = null);

        Task<ContactDataResponseDto?> GetContactDataAsync(string? facultyMemberEmail = null);
        Task<ContactDataResponseDto?> UpdateContactDataAsync(
            ContactDataUpdateDto contactDataUpdateDto,
            string? facultyMemberEmail = null);

        Task<IdentificationCardDto> GetIdentificationCardAsync(string? facultyMemberEmail = null);
        Task<IdentificationCardDto> UpdateIdentificationCardAsync(
            IdentificationCardDto identificationCardDto,
            string? facultyMemberEmail = null);

        Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync(string? facultyMemberEmail = null);
        Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(
            SocialMediaPlatformsDto socialMediaPlatformsDto,
            string? facultyMemberEmail = null);
    }
}
