using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.FacultyMemberDataModule;
using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberMainDataManagementService(
      IFacultyMemberDataHelper facultyMemberDataHelper)
      : IFacultyMemberMainDataManagementService
    {
        private readonly IFacultyMemberDataHelper _facultyMemberDataHelper = facultyMemberDataHelper;

        public Task<PersonalDataResponseDto?> GetMemberPersonalDataAsync(string facultyMemberEmail)
            => _facultyMemberDataHelper.GetPersonalDataAsync(facultyMemberEmail);

        public Task<PersonalDataResponseDto?> UpdateMemberPersonalDataAsync(
            PersonalDataUpdateDto personalDataUpdateDto,
            string facultyMemberEmail)
            => _facultyMemberDataHelper.UpdatePersonalDataAsync(personalDataUpdateDto, facultyMemberEmail);

        public Task<ContactDataResponseDto?> GetMemberContactDataAsync(string facultyMemberEmail)
            => _facultyMemberDataHelper.GetContactDataAsync(facultyMemberEmail);

        public Task<ContactDataResponseDto?> UpdateMemberContactDataAsync(
            ContactDataUpdateDto contactDataUpdateDto,
            string facultyMemberEmail)
            => _facultyMemberDataHelper.UpdateContactDataAsync(contactDataUpdateDto, facultyMemberEmail);

        public Task<IdentificationCardDto> GetMemberIdentificationCardAsync(string facultyMemberEmail)
            => _facultyMemberDataHelper.GetIdentificationCardAsync(facultyMemberEmail);

        public Task<IdentificationCardDto> UpdateMemberIdentificationCardAsync(
            IdentificationCardDto identificationCardDto,
            string facultyMemberEmail)
            => _facultyMemberDataHelper.UpdateIdentificationCardAsync(identificationCardDto, facultyMemberEmail);

        public Task<SocialMediaPlatformsDto> GetMemberSocialMediaPlatformsAsync(string facultyMemberEmail)
            => _facultyMemberDataHelper.GetSocialMediaPlatformsAsync(facultyMemberEmail);

        public Task<SocialMediaPlatformsDto> UpdateMemberSocialMediaPlatformsAsync(
            SocialMediaPlatformsDto socialMediaPlatformsDto,
            string facultyMemberEmail)
            => _facultyMemberDataHelper.UpdateSocialMediaPlatformsAsync(socialMediaPlatformsDto, facultyMemberEmail);
    }
}
