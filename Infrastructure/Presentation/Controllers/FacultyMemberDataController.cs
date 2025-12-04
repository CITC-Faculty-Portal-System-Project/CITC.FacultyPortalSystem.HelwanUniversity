using Microsoft.AspNetCore.Authorization;
using Shared.Dtos.FacultyMemberDataModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class FacultyMemberDataController(IServiceManager _serviceManager) : ApiController
    {
        #region Personal Data
        [ProducesResponseType(typeof(PersonalDataResponseDto), StatusCodes.Status200OK)]
        [HttpGet("PersonalData")]
        public async Task<ActionResult<PersonalDataResponseDto>> GetPersonalDataAsync()
            => Ok(await _serviceManager.FacultyMemberDataService.GetPersonalDataAsync());

        [ProducesResponseType(typeof(PersonalDataResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdatePersonalData")]
        public async Task<ActionResult<PersonalDataResponseDto>> UpdatePersonalDataAsync(PersonalDataUpdateDto personalDataUpdateDto)
            => Ok(await _serviceManager.FacultyMemberDataService.UpdatePersonalDataAsync(personalDataUpdateDto));
        #endregion

        #region Contact Data
        [ProducesResponseType(typeof(ContactDataResponseDto), StatusCodes.Status200OK)]
        [HttpGet("ContactData")]
        public async Task<ActionResult<ContactDataResponseDto>> GetContactDataAsync()
            => Ok(await _serviceManager.FacultyMemberDataService.GetContactDataAsync());

        [ProducesResponseType(typeof(ContactDataResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateContactData")]
        public async Task<ActionResult<ContactDataResponseDto>> UpdateContactDataAsync(ContactDataUpdateDto contactDataUpdateDto)
            => Ok(await _serviceManager.FacultyMemberDataService.UpdateContactDataAsync(contactDataUpdateDto));
        #endregion

        #region Identification Card
        [ProducesResponseType(typeof(IdentificationCardDto), StatusCodes.Status200OK)]
        [HttpGet("IdentificationCard")]
        public async Task<ActionResult<IdentificationCardDto>> GetIdentificationCardAsync()
            => Ok(await _serviceManager.FacultyMemberDataService.GetIdentificationCardAsync());

        [ProducesResponseType(typeof(IdentificationCardDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateIdentificationCard")]
        public async Task<ActionResult<IdentificationCardDto>> UpdateIdentificationCardAsync(IdentificationCardDto identificationCardDto)
            => Ok(await _serviceManager.FacultyMemberDataService.UpdateIdentificationCardAsync(identificationCardDto));
        #endregion

        #region Social Media
        [ProducesResponseType(typeof(SocialMediaPlatformsDto), StatusCodes.Status200OK)]
        [HttpGet("SocialMediaPlatforms")]
        public async Task<ActionResult<SocialMediaPlatformsDto>> GetSocialMediaOlatformsAsync()
            => Ok(await _serviceManager.FacultyMemberDataService.GetSocialMediaPlatformsAsync());

        [ProducesResponseType(typeof(SocialMediaPlatformsDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateSocialMediaPlatforms")]
        public async Task<ActionResult<SocialMediaPlatformsDto>> UpdateSocialMediaPlatformsAsync(SocialMediaPlatformsDto socialMediaPlatformsDto)
            => Ok(await _serviceManager.FacultyMemberDataService.UpdateSocialMediaPlatformsAsync(socialMediaPlatformsDto));
        #endregion
    }
}
