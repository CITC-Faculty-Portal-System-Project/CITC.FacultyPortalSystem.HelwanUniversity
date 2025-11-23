using Shared.Dtos.FacultyMemberDataModule;

namespace Presentation.Controllers
{
    public class FacultyMemberDataController(IServiceManager _serviceManager) : ApiController
    {
        #region Personal Data
        [ProducesResponseType(typeof(PersonalDataResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("PersonalData")]
        public async Task<ActionResult<PersonalDataResponseDto>> GetPersonalDataAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberDataService.GetPersonalDataAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(PersonalDataResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FetchPersonalData")]
        public async Task<ActionResult<PersonalDataResponseDto>> FetchPersonalDataAsync(PersonalDataCreateDto personalDataCreateDto)
            => Ok(await _serviceManager.FacultyMemberDataService.FetchPersonalDataAsync(personalDataCreateDto));

        [ProducesResponseType(typeof(PersonalDataResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdatePersonalData")]
        public async Task<ActionResult<PersonalDataResponseDto>> UpdatePersonalDataAsync([FromQuery] string facultyMemberEmail, PersonalDataUpdateDto personalDataUpdateDto)
            => Ok(await _serviceManager.FacultyMemberDataService.UpdatePersonalDataAsync(facultyMemberEmail, personalDataUpdateDto));
        #endregion

        #region Contact Data
        [ProducesResponseType(typeof(ContactDataResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ContactData")]
        public async Task<ActionResult<ContactDataResponseDto>> GetContactDataAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberDataService.GetContactDataAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(ContactDataResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FetchContactData")]
        public async Task<ActionResult<ContactDataResponseDto>> FetchContactDataAsync([FromQuery] string nationalNumber, ContactDataCreateDto contactDataCreateDto)
            => Ok(await _serviceManager.FacultyMemberDataService.FetchContactDataAsync(nationalNumber, contactDataCreateDto));

        [ProducesResponseType(typeof(ContactDataResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateContactData")]
        public async Task<ActionResult<ContactDataResponseDto>> UpdateContactDataAsync([FromQuery] string facultyMemberEmail, ContactDataUpdateDto contactDataUpdateDto)
            => Ok(await _serviceManager.FacultyMemberDataService.UpdateContactDataAsync(facultyMemberEmail, contactDataUpdateDto));
        #endregion

        #region Identification Card
        [ProducesResponseType(typeof(IdentificationCardDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("IdentificationCard")]
        public async Task<ActionResult<IdentificationCardDto>> GetIdentificationCardAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberDataService.GetIdentificationCardAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(IdentificationCardDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateIdentificationCard")]
        public async Task<ActionResult<IdentificationCardDto>> UpdateIdentificationCardAsync([FromQuery] string facultyMemberEmail, IdentificationCardDto identificationCardDto)
            => Ok(await _serviceManager.FacultyMemberDataService.UpdateIdentificationCardAsync(facultyMemberEmail, identificationCardDto));
        #endregion

        #region Social Media
        [ProducesResponseType(typeof(SocialMediaPlatformsDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("SocialMediaPlatforms")]
        public async Task<ActionResult<SocialMediaPlatformsDto>> GetSocialMediaOlatformsAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberDataService.GetSocialMediaPlatformsAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(SocialMediaPlatformsDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateSocialMediaPlatforms")]
        public async Task<ActionResult<SocialMediaPlatformsDto>> UpdateSocialMediaPlatformsAsync([FromQuery] string facultyMemberEmail, SocialMediaPlatformsDto socialMediaPlatformsDto)
            => Ok(await _serviceManager.FacultyMemberDataService.UpdateSocialMediaPlatformsAsync(facultyMemberEmail, socialMediaPlatformsDto));
        #endregion
    }
}
