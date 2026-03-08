using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.FacultyMemberDataModule;
using Services.Global;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Dtos.IdentityModule;

namespace Services.Implementations
{
    public class FacultyMemberDataService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService,
        IFacultyMemberDataHelper facultyMemberDataHelper)
        : BaseService<FacultyMember, Guid>(unitOfWork, authenticationService, mapper), IFacultyMemberDataService
    {
        private readonly IFacultyMemberDataHelper _facultyMemberDataHelper = facultyMemberDataHelper;

        protected override string EntityName => "Faculty Member";

        #region Personal Data
        public async Task<PersonalDataResponseDto?> GetPersonalDataAsync()
        {
            var currentUser = await GetCurrentUserAsync();
            return await _facultyMemberDataHelper.GetPersonalDataAsync(currentUser.Email);
        }

        public async Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(PersonalDataUpdateDto personalDataUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();
            return await _facultyMemberDataHelper.UpdatePersonalDataAsync(personalDataUpdateDto, currentUser.Email);
        }
        #endregion

        #region Contact Data
        public async Task<ContactDataResponseDto?> GetContactDataAsync()
        {
            var currentUser = await GetCurrentUserAsync();
            return await _facultyMemberDataHelper.GetContactDataAsync(currentUser.Email);
        }

        public async Task<ContactDataResponseDto?> UpdateContactDataAsync(ContactDataUpdateDto contactDataUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();
            return await _facultyMemberDataHelper.UpdateContactDataAsync(contactDataUpdateDto, currentUser.Email);
        }
        #endregion

        #region Identification Card
        public async Task<IdentificationCardDto> GetIdentificationCardAsync()
        {
            var currentUser = await GetCurrentUserAsync();
            return await _facultyMemberDataHelper.GetIdentificationCardAsync(currentUser.Email);
        }

        public async Task<IdentificationCardDto> UpdateIdentificationCardAsync(IdentificationCardDto identificationCardDto)
        {
            var currentUser = await GetCurrentUserAsync();
            return await _facultyMemberDataHelper.UpdateIdentificationCardAsync(identificationCardDto, currentUser.Email);
        }
        #endregion

        #region Social Media
        public async Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync()
        {
            var currentUser = await GetCurrentUserAsync();
            return await _facultyMemberDataHelper.GetSocialMediaPlatformsAsync(currentUser.Email);
        }

        public async Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(SocialMediaPlatformsDto socialMediaPlatformsDto)
        {
            var currentUser = await GetCurrentUserAsync();
            return await _facultyMemberDataHelper.UpdateSocialMediaPlatformsAsync(socialMediaPlatformsDto, currentUser.Email);
        }
        #endregion
    }
}
