using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.FacultyMemberDataModule
{
    public interface IFacultyMemberDataHelper
    {
        Task<PersonalDataResponseDto?> GetPersonalDataAsync(string facultyMemberEmail);
        Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(PersonalDataUpdateDto dto, string facultyMemberEmail);

        Task<ContactDataResponseDto?> GetContactDataAsync(string facultyMemberEmail);
        Task<ContactDataResponseDto?> UpdateContactDataAsync(ContactDataUpdateDto dto, string facultyMemberEmail);

        Task<IdentificationCardDto> GetIdentificationCardAsync(string facultyMemberEmail);
        Task<IdentificationCardDto> UpdateIdentificationCardAsync(IdentificationCardDto dto, string facultyMemberEmail);

        Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync(string facultyMemberEmail);
        Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(SocialMediaPlatformsDto dto, string facultyMemberEmail);
    }
}

