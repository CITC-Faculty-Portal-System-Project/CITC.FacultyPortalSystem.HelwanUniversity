using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Dtos.AttachmentsModule;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.IdentityModule;
using Shared.SpecificationParameters.ResearchesModule;
namespace Presentation.Controllers.AdminModule
{

    public class AdminController(IServiceManager _serviceManager) : ApiController
    {

        #region UsersManagement

        [Authorize(Policy = "Permission:UserAccount.Read")]
        [ProducesResponseType(typeof(PaginatedResult<PermissionResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Permissions")]
        public async Task<ActionResult<PaginatedResult<PermissionResponseDTO>>> GetAllPermissions
                   ([FromQuery] PermissionSpecificationParameters parameters)
             => Ok(await _serviceManager.UserManagementService.GetAllSystemPermissionsAsync(parameters));


        [Authorize(Policy = "Permission:UserAccount.Read")]
        [ProducesResponseType(typeof(PaginatedResult<UserShowForAdminResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Users")]
        public async Task<ActionResult<PaginatedResult<UserShowForAdminResponseDTO>>> GetAllUsers
                     ([FromQuery] UserSpecificationParameters parameters)
               => Ok(await _serviceManager.UserManagementService.GetAllUsersAsync(parameters));

        [ProducesResponseType(typeof(UserIdentifiersResposnseDTO), StatusCodes.Status200OK)]
        [Authorize(Roles = "SupportAdmin,ManagementAdmin")]
        [HttpGet("UserIdenitifiers")]
        public async Task<ActionResult<UserIdentifiersResposnseDTO>> GetUserIdentitfiers(string username)
     => Ok(await _serviceManager.UserManagementService.GetUserEmailAndIdByUsername(username));


        [Authorize(Policy = "Permission:UserAccount.Read")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("User/{id}")]
        public async Task<ActionResult<PaginatedResult<UserShowForAdminResponseDTO>>> GetUserById
                 (Guid id)
           => Ok(await _serviceManager.UserManagementService.GetUserByIdAsync(id));


        [Authorize(Policy = "Permission:UserAccount.Create")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("User")]
        public async Task<ActionResult<UserShowForAdminResponseDTO>> AddUser
              (UserAddDTO user)
                => Ok(await _serviceManager.UserManagementService.AddUserAsync(user));


        [Authorize(Policy = "Permission:UserAccount.Update")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UserCredeintals/{id}")]
        public async Task<ActionResult<UserShowForAdminResponseDTO>> EditUser
        (UserEditDTO user, Guid id)
        => Ok(await _serviceManager.UserManagementService.EditUserCredeintalsAsync(user, id));


        [Authorize(Policy = "Permission:UserAccount.Update")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UserGrantPermissions/{id}")]
        public async Task<ActionResult<UserShowForAdminResponseDTO>> AssignPermissionToUser
        (IList<PermissionResponseDTO> permissions, Guid id)
        => Ok(await _serviceManager.UserManagementService.AssignPermissionsToUserAsync(permissions, id));

        [Authorize(Policy = "Permission:UserAccount.Update")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpDelete("UserRevokePermissions/{id}")]
        public async Task<ActionResult<UserShowForAdminResponseDTO>> RevokePermissionFromUser
           (IList<PermissionResponseDTO> permissions, Guid id)
           => Ok(await _serviceManager.UserManagementService.RevokePermissionsFromUserAsync(permissions, id));

        #endregion

        #region FacultyMembersDataManagement

        #region MainDataModule

        #region Personal Data

        [ProducesResponseType(typeof(PersonalDataResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/PersonalData")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        public async Task<ActionResult<PersonalDataResponseDto>> GetFacultyMemberPersonalDataAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberDataService
                .GetPersonalDataAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(PersonalDataResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/PersonalData")]
        [Authorize(Policy = "Permission:FacultyMemberData.Update")]
        public async Task<ActionResult<PersonalDataResponseDto>> UpdateFacultyMemberPersonalDataAsync(
            [FromQuery] string facultyMemberEmail,
            PersonalDataUpdateDto personalDataUpdateDto)
            => Ok(await _serviceManager.FacultyMemberDataService
                .UpdatePersonalDataAsync(personalDataUpdateDto, facultyMemberEmail));

        #endregion

        #region Contact Data

        [ProducesResponseType(typeof(ContactDataResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ContactData")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        public async Task<ActionResult<ContactDataResponseDto>> GetFacultyMemberContactDataAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberDataService
                .GetContactDataAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(ContactDataResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ContactData")]
        [Authorize(Policy = "Permission:FacultyMemberData.Update")]
        public async Task<ActionResult<ContactDataResponseDto>> UpdateFacultyMemberContactDataAsync(
            [FromQuery] string facultyMemberEmail,
            ContactDataUpdateDto contactDataUpdateDto)
            => Ok(await _serviceManager.FacultyMemberDataService
                .UpdateContactDataAsync(contactDataUpdateDto, facultyMemberEmail));

        #endregion

        #region Identification Card

        [ProducesResponseType(typeof(IdentificationCardDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/IdentificationCard")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        public async Task<ActionResult<IdentificationCardDto>> GetFacultyMemberIdentificationCardAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberDataService
                .GetIdentificationCardAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(IdentificationCardDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/IdentificationCard")]
        [Authorize(Policy = "Permission:FacultyMemberData.Update")]
        public async Task<ActionResult<IdentificationCardDto>> UpdateFacultyMemberIdentificationCardAsync(
            [FromQuery] string facultyMemberEmail,
            IdentificationCardDto identificationCardDto)
            => Ok(await _serviceManager.FacultyMemberDataService
                .UpdateIdentificationCardAsync(identificationCardDto, facultyMemberEmail));

        #endregion

        #region Social Media

        [ProducesResponseType(typeof(SocialMediaPlatformsDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/SocialMediaPlatforms")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        public async Task<ActionResult<SocialMediaPlatformsDto>> GetFacultyMemberSocialMediaPlatformsAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberDataService
                .GetSocialMediaPlatformsAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(SocialMediaPlatformsDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/SocialMediaPlatforms")]
        [Authorize(Policy = "Permission:FacultyMemberData.Update")]
        public async Task<ActionResult<SocialMediaPlatformsDto>> UpdateFacultyMemberSocialMediaPlatformsAsync(
            [FromQuery] string facultyMemberEmail,
            SocialMediaPlatformsDto socialMediaPlatformsDto)
            => Ok(await _serviceManager.FacultyMemberDataService
                .UpdateSocialMediaPlatformsAsync(socialMediaPlatformsDto, facultyMemberEmail));

        #endregion

        #endregion

        #endregion

        #region FacultyMemberContributionsManagementModule

        #region ContributionsToCommunity

        [ProducesResponseType(typeof(PaginatedResult<ContributionsToCommunityServiceResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ContributionsToCommunityService")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<PaginatedResult<ContributionsToCommunityServiceResponseDTO>>> GetFacultyMemberContributionsToCommunityServiceAsync(
        [FromQuery] ContributionsToCommunityServiceSpecificationParameters parameters)
        => Ok(await _serviceManager.ContributionsToCommunityService
            .GetAllContributionsToCommunityServiceAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ContributionsToCommunityServiceResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ContributionsToCommunityService/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<ContributionsToCommunityServiceResponseDTO>> GetFacultyMemberContributionToCommunityServiceByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.ContributionsToCommunityService
                .GetContributionToCommunityServiceByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(ContributionsToCommunityServiceResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ContributionsToCommunityService")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Create")]
        public async Task<ActionResult<ContributionsToCommunityServiceResponseDTO>> CreateFacultyMemberContributionToCommunityServiceAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto)
            => Ok(await _serviceManager.ContributionsToCommunityService
                .CreateContributionToCommunityServiceAsync(contributionsToCommunityServiceCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ContributionsToCommunityServiceResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ContributionsToCommunityService/{contributionToCommunityServiceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Update")]
        public async Task<ActionResult<ContributionsToCommunityServiceResponseDTO>> UpdateFacultyMemberContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId, string memberEmail,
            [FromBody] ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto)
            => Ok(await _serviceManager.ContributionsToCommunityService
                .UpdateContributionToCommunityServiceAsync(
                    contributionToCommunityServiceId,
                    contributionsToCommunityServiceUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ContributionsToCommunityService/{contributionToCommunityServiceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberContributionToCommunityServiceAsync(int contributionToCommunityServiceId , string memberEmail)
        {
            await _serviceManager.ContributionsToCommunityService
                .DeleteContributionToCommunityServiceAsync(contributionToCommunityServiceId , memberEmail);

            return NoContent();
        }

        #endregion

        #region ContributionsToUniverstiy


        [ProducesResponseType(typeof(PaginatedResult<ContributionsToUniversityResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ContributionsToUniversity")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<PaginatedResult<ContributionsToUniversityResponseDTO>>> GetFacultyMemberContributionsToUniversityAsync(
           [FromQuery] ContributionsToUniversitySpecificationParameters parameters)
           => Ok(await _serviceManager.ContributionsToUniversityService
               .GetAllContributionsToUniversityAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ContributionsToUniversityResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ContributionsToUniversity/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<ContributionsToUniversityResponseDTO>> GetFacultyMemberContributionToUniversityByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.ContributionsToUniversityService
                .GetContributionToUniversityByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(ContributionsToUniversityResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ContributionsToUniversity")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Create")]
        public async Task<ActionResult<ContributionsToUniversityResponseDTO>> CreateFacultyMemberContributionToUniversityAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto)
            => Ok(await _serviceManager.ContributionsToUniversityService
                .CreateContributionToUniversityAsync(contributionsToUniversityCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ContributionsToUniversityResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ContributionsToUniversity/{contributionToUniversityId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Update")]
        public async Task<ActionResult<ContributionsToUniversityResponseDTO>> UpdateFacultyMemberContributionToUniversityAsync(
            int contributionToUniversityId,
            [FromBody] ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto , string memberEmail)
            => Ok(await _serviceManager.ContributionsToUniversityService
                .UpdateContributionToUniversityAsync(contributionToUniversityId, contributionsToUniversityUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ContributionsToUniversity/{contributionToUniversityId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberContributionToUniversityAsync(int contributionToUniversityId, string memberEmail)
        {
            await _serviceManager.ContributionsToUniversityService
                .DeleteContributionToUniversityAsync(contributionToUniversityId , memberEmail);

            return NoContent();
        }

        #endregion

        #region ParticipationInQualityWorks

        [ProducesResponseType(typeof(PaginatedResult<ParticipationInQualityWorksResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ParticipationInQualityWorks")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<PaginatedResult<ParticipationInQualityWorksResponseDTO>>> GetFacultyMemberParticipationsInQualityWorksAsync(
            [FromQuery] ParticipationInQualityWorksSpecificationParameters parameters)
            => Ok(await _serviceManager.ParticipationInQualityWorksService
                .GetAllParticipationsInQualityWorksAsync(parameters, parameters.FacultyMemberEmail));


        [ProducesResponseType(typeof(ParticipationInQualityWorksResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ParticipationInQualityWorks/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<ParticipationInQualityWorksResponseDTO>> GetFacultyMemberParticipationInQualityWorksByIdAsync(int id, string memberEmail)
            => Ok(await _serviceManager.ParticipationInQualityWorksService
                .GetParticipationInQualityWorksByIdAsync(id , memberEmail));


        [ProducesResponseType(typeof(ParticipationInQualityWorksResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ParticipationInQualityWorks")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Create")]
        public async Task<ActionResult<ParticipationInQualityWorksResponseDTO>> CreateFacultyMemberParticipationInQualityWorksAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto)
            => Ok(await _serviceManager.ParticipationInQualityWorksService
                .CreateParticipationInQualityWorksAsync(participationInQualityWorksCreateDto, facultyMemberEmail));


        [ProducesResponseType(typeof(ParticipationInQualityWorksResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ParticipationInQualityWorks/{participationInQualityWorksId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Update")]
        public async Task<ActionResult<ParticipationInQualityWorksResponseDTO>> UpdateFacultyMemberParticipationInQualityWorksAsync(
            int participationInQualityWorksId, string memberEmail , 
            [FromBody] ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto)
            => Ok(await _serviceManager.ParticipationInQualityWorksService
                .UpdateParticipationInQualityWorksAsync(participationInQualityWorksId, participationInQualityWorksUpdateDto , memberEmail));


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ParticipationInQualityWorks/{participationInQualityWorksId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberParticipationInQualityWorksAsync(int participationInQualityWorksId , string memberEmail)
        {
            await _serviceManager.ParticipationInQualityWorksService
                .DeleteParticipationInQualityWorksAsync(participationInQualityWorksId , memberEmail);

            return NoContent();
        }

        #endregion

        #endregion

        #region FacultyMemberExperiencesManagementModule

        #region GeneralExperiences


        [ProducesResponseType(typeof(PaginatedResult<GeneralExperiencesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/GeneralExperiences")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Read")]
        public async Task<ActionResult<PaginatedResult<GeneralExperiencesResponseDTO>>> GetFacultyMemberGeneralExperiencesAsync(
            [FromQuery] GeneralExperiencesSpecificationParameters parameters)
            => Ok(await _serviceManager.GeneralExperiencesService
                .GetAllGeneralExperiencesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(GeneralExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/GeneralExperiences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Read")]
        public async Task<ActionResult<GeneralExperiencesResponseDTO>> GetFacultyMemberGeneralExperienceByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.GeneralExperiencesService
                .GetGeneralExperienceByIdAsync(id));

        [ProducesResponseType(typeof(GeneralExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/GeneralExperiences")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Create")]
        public async Task<ActionResult<GeneralExperiencesResponseDTO>> CreateFacultyMemberGeneralExperienceAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] GeneralExperiencesCreateDTO generalExperienceCreateDto)
            => Ok(await _serviceManager.GeneralExperiencesService
                .CreateGeneralExperienceAsync(generalExperienceCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(GeneralExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/GeneralExperiences/{generalExperienceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Update")]
        public async Task<ActionResult<GeneralExperiencesResponseDTO>> UpdateFacultyMemberGeneralExperienceAsync(
            int generalExperienceId, string memberEmail,
            [FromBody] GeneralExperiencesUpdateDTO generalExperienceUpdateDto)
            => Ok(await _serviceManager.GeneralExperiencesService
                .UpdateGeneralExperienceAsync(generalExperienceId, generalExperienceUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/GeneralExperiences/{generalExperienceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberGeneralExperienceAsync(int generalExperienceId , string memberEmail)
        {
            await _serviceManager.GeneralExperiencesService
                .DeleteGeneralExperienceAsync(generalExperienceId , memberEmail);

            return NoContent();
        }

    #endregion

        #region TeachingExperinces

        [ProducesResponseType(typeof(PaginatedResult<TeachingExperiencesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/TeachingExperiences")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Read")]
        public async Task<ActionResult<PaginatedResult<TeachingExperiencesResponseDTO>>> GetFacultyMemberTeachingExperiencesAsync(
            [FromQuery] TeachingExperiencesSpecificationParameters parameters)
            => Ok(await _serviceManager.TeachingExperiencesService
                .GetAllTeachingExperiencesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(TeachingExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/TeachingExperiences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Read")]
        public async Task<ActionResult<TeachingExperiencesResponseDTO>> GetFacultyMemberTeachingExperienceByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.TeachingExperiencesService
                .GetTeachingExperienceByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(TeachingExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/TeachingExperiences")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Create")]
        public async Task<ActionResult<TeachingExperiencesResponseDTO>> CreateFacultyMemberTeachingExperienceAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] TeachingExperiencesCreateDTO teachingExperienceCreateDto)
            => Ok(await _serviceManager.TeachingExperiencesService
                .CreateTeachingExperienceAsync(teachingExperienceCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(TeachingExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/TeachingExperiences/{teachingExperienceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Update")]
        public async Task<ActionResult<TeachingExperiencesResponseDTO>> UpdateFacultyMemberTeachingExperienceAsync(
            int teachingExperienceId, string memberEmail,   
            [FromBody] TeachingExperiencesUpdateDTO teachingExperienceUpdateDto)
            => Ok(await _serviceManager.TeachingExperiencesService
                .UpdateTeachingExperienceAsync(teachingExperienceId, teachingExperienceUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/TeachingExperiences/{teachingExperienceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberTeachingExperienceAsync(int teachingExperienceId , string memberEmail)
        {
            await _serviceManager.TeachingExperiencesService
                .DeleteTeachingExperienceAsync(teachingExperienceId , memberEmail);

            return NoContent();
        }

        #endregion

        #endregion

        #region FacultyMemberMissionsManagementModule

        #region ScientificMissions

        [ProducesResponseType(typeof(PaginatedResult<ScientificMissionResponseDto?>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ScientificMissions")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<PaginatedResult<ScientificMissionResponseDto?>>> GetFacultyMemberScientificMissionsAsync(
           [FromQuery] ScientificMissionSpecificationParamaters parameters)
           => Ok(await _serviceManager.ScientificMissionsService
               .GetAllScientificMissionsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ScientificMissionResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ScientificMissions/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<ScientificMissionResponseDto?>> GetFacultyMemberScientificMissionByIdAsync(int id, string memberEmail)
            => Ok(await _serviceManager.ScientificMissionsService
                .GetScientificMissionByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(ScientificMissionResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ScientificMissions")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Create")]
        public async Task<ActionResult<ScientificMissionResponseDto>> CreateFacultyMemberScientificMissionAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ScientificMissionCreateDto scientificMissionCreateDto)
            => Ok(await _serviceManager.ScientificMissionsService
                .CreateScientificMissionAsync(scientificMissionCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ScientificMissionResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ScientificMissions/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Update")]
        public async Task<ActionResult<ScientificMissionResponseDto>> UpdateFacultyMemberScientificMissionAsync(
            int id, string memberEmail,
            [FromBody] ScientificMissionUpdateDto scientificMissionUpdateDto)
            => Ok(await _serviceManager.ScientificMissionsService
                .UpdateScientificMissionAsync(id, scientificMissionUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ScientificMissions/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberScientificMissionAsync(int id, string memberEmail)
        {
            await _serviceManager.ScientificMissionsService
                .DeleteScientificMissionAsync(id , memberEmail);

            return NoContent();
        }



        #endregion

        #region ConferencesAndSeminars

        [ProducesResponseType(typeof(PaginatedResult<ConferencesAndSeminarsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/SeminarsAndConferences")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<PaginatedResult<ConferencesAndSeminarsResponseDto>>> GetFacultyMemberSeminarsAndConferencesAsync(
            [FromQuery] SeminarsAndConferncesSpecificationParameters parameters)
            => Ok(await _serviceManager.SeminarsAndConferencesService
                .GetAllSeminarsAndConferencesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ConferencesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/SeminarsAndConferences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<ConferencesAndSeminarsResponseDto>> GetFacultyMemberSeminarOrConferenceByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.SeminarsAndConferencesService
                .GetSeminarOrConferenceByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(ConferencesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/SeminarsAndConferences")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Create")]
        public async Task<ActionResult<ConferencesAndSeminarsResponseDto>> CreateFacultyMemberSeminarOrConferenceAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto)
            => Ok(await _serviceManager.SeminarsAndConferencesService
                .CreateSeminarOrConferenceAsync(conferencesAndSeminarsCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ConferencesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/SeminarsAndConferences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Update")]
        public async Task<ActionResult<ConferencesAndSeminarsResponseDto>> UpdateFacultyMemberSeminarOrConferenceAsync(
            int id, string memberEmail,
            [FromBody] ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto)
            => Ok(await _serviceManager.SeminarsAndConferencesService
                .UpdateSeminarOrConferenceAsync(id, conferencesAndSeminarsUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/SeminarsAndConferences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberSeminarOrConferenceAsync(int id , string memberEmail)
        {
            await _serviceManager.SeminarsAndConferencesService
                .DeleteSeminarOrConferenceAsync(id , memberEmail);

            return NoContent();
        }

        #endregion

        #region TrainingPrograms

        [ProducesResponseType(typeof(PaginatedResult<TrainingProgramsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/TrainingPrograms")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<PaginatedResult<TrainingProgramsResponseDto>>> GetFacultyMemberTrainingProgramsAsync(
         [FromQuery] TrainingProgramsSpecificationParameters parameters)
         => Ok(await _serviceManager.TrainingProgramsService
             .GetAllTrainingProgramsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(TrainingProgramsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/TrainingPrograms/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<TrainingProgramsResponseDto>> GetFacultyMemberTrainingProgramByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.TrainingProgramsService
                .GetTrainingProgramByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(TrainingProgramsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/TrainingPrograms")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Create")]
        public async Task<ActionResult<TrainingProgramsResponseDto>> CreateFacultyMemberTrainingProgramAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] TrainingProgramsCreateDto trainingProgramsCreateDto)
            => Ok(await _serviceManager.TrainingProgramsService
                .CreateTrainingProgramAsync(trainingProgramsCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(TrainingProgramsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/TrainingPrograms/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Update")]
        public async Task<ActionResult<TrainingProgramsResponseDto>> UpdateFacultyMemberTrainingProgramAsync(
            int id, string memberEmail ,
            [FromBody] TrainingProgramsUpdateDto trainingProgramsUpdateDto)
            => Ok(await _serviceManager.TrainingProgramsService
                .UpdateTrainingProgramAsync(id, trainingProgramsUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/TrainingPrograms/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberTrainingProgramAsync(int id , string memberEmail)
        {
            await _serviceManager.TrainingProgramsService
                .DeleteTrainingProgramAsync(id , memberEmail);

            return NoContent();
        }

        #endregion


        #endregion

        #region FacultyMemberPrizesManagementModule

        #region ManifestationsOfScientificAppreciation 

        [ProducesResponseType(typeof(PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ManifestationsOfScientificAppreciation")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Read")]
        public async Task<ActionResult<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>>> GetFacultyMemberManifestationsOfScientificAppreciationAsync(
            [FromQuery] ManifestationsOfScientificAppreciationSpecificationParameters parameters)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationService
                .GetAllManifestationsOfScientificAppreciationAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ManifestationsOfScientificAppreciationResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ManifestationsOfScientificAppreciation/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Read")]
        public async Task<ActionResult<ManifestationsOfScientificAppreciationResponseDTO>> GetFacultyMemberManifestationOfScientificAppreciationByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationService
                .GetManifestationOfScientificAppreciationByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(ManifestationsOfScientificAppreciationResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ManifestationsOfScientificAppreciation")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Create")]
        public async Task<ActionResult<ManifestationsOfScientificAppreciationResponseDTO>> CreateFacultyMemberManifestationOfScientificAppreciationAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ManifestationsOfScientificAppreciationCreateDTO manifestationsOfScientificAppreciationCreateDto)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationService
                .CreateManifestationOfScientificAppreciationAsync(
                    manifestationsOfScientificAppreciationCreateDto,
                    facultyMemberEmail));

        [ProducesResponseType(typeof(ManifestationsOfScientificAppreciationResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ManifestationsOfScientificAppreciation/{manifestationsOfScientificAppreciationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Update")]
        public async Task<ActionResult<ManifestationsOfScientificAppreciationResponseDTO>> UpdateFacultyMemberManifestationOfScientificAppreciationAsync(
            int manifestationsOfScientificAppreciationId, string memberEmail ,
            [FromBody] ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDto)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationService
                .UpdateManifestationOfScientificAppreciationAsync(
                    manifestationsOfScientificAppreciationId,
                    manifestationsOfScientificAppreciationUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ManifestationsOfScientificAppreciation/{manifestationsOfScientificAppreciationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberManifestationOfScientificAppreciationAsync(
            int manifestationsOfScientificAppreciationId , string memberEmail)
        {
            await _serviceManager.ManifestationsOfScientificAppreciationService
                .DeleteManifestationOfScientificAppreciationAsync(manifestationsOfScientificAppreciationId , memberEmail);

            return NoContent();
        }

        #endregion

        #region PrizesAndAwards

        [ProducesResponseType(typeof(PaginatedResult<PrizesAndRewardsResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/PrizesAndRewards")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Read")]
        public async Task<ActionResult<PaginatedResult<PrizesAndRewardsResponseDTO>>> GetFacultyMemberPrizesAndRewardsAsync(
          [FromQuery] PrizesAndRewardsSpecificationParameters parameters)
          => Ok(await _serviceManager.PrizesAndRewardsService
              .GetAllPrizesAndRewardsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(PrizesAndRewardsResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/PrizesAndRewards/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Read")]
        public async Task<ActionResult<PrizesAndRewardsResponseDTO>> GetFacultyMemberPrizeOrRewardByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.PrizesAndRewardsService
                .GetPrizeOrRewardByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(PrizesAndRewardsResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/PrizesAndRewards")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Create")]
        public async Task<ActionResult<PrizesAndRewardsResponseDTO>> CreateFacultyMemberPrizeOrRewardAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] PrizesAndRewardsCreateDTO prizesAndRewardsCreateDto)
            => Ok(await _serviceManager.PrizesAndRewardsService
                .CreatePrizeOrRewardAsync(prizesAndRewardsCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(PrizesAndRewardsResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/PrizesAndRewards/{prizesOrRewardId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Update")]
        public async Task<ActionResult<PrizesAndRewardsResponseDTO>> UpdateFacultyMemberPrizeOrRewardAsync(
            int prizesOrRewardId, string memberEmail ,
            [FromBody] PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDto)
            => Ok(await _serviceManager.PrizesAndRewardsService
                .UpdatePrizeOrRewardAsync(prizesOrRewardId, prizesAndRewardsUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/PrizesAndRewards/{prizesOrRewardId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberPrizeOrRewardAsync(int prizesOrRewardId , string memberEmail)
        {
            await _serviceManager.PrizesAndRewardsService
                .DeletePrizeOrRewardAsync(prizesOrRewardId , memberEmail);

            return NoContent();
        }

        #endregion

        #endregion

        #region FacultyMemberCommitteesAndAssociationsManagementModule

        #region CommitteesAndAssociations

        [ProducesResponseType(typeof(PaginatedResult<CommitteesAndAssociationsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/CommitteesAndAssociations")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<PaginatedResult<CommitteesAndAssociationsResponseDto>>> GetFacultyMemberCommitteesAndAssociationsAsync(
      [FromQuery] CommitteesAndAssociationsSpecificationsParameters parameters)
      => Ok(await _serviceManager.CommitteesAndAssociationsService
          .GetAllCommitteesAndAssociationsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/CommitteesAndAssociations/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> GetFacultyMemberCommitteeOrAssociationByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.CommitteesAndAssociationsService
                .GetCommitteeOrAssociationByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/CommitteesAndAssociations")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Create")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> CreateFacultyMemberCommitteeOrAssociationAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto)
            => Ok(await _serviceManager.CommitteesAndAssociationsService
                .CreateCommitteeOrAssociationAsync(committeeOrAssociationCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/CommitteesAndAssociations/{committeeOrAssociationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Update")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> UpdateFacultyMemberCommitteeOrAssociationAsync(
            int committeeOrAssociationId, string memberEmail,
            [FromBody] CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto)
            => Ok(await _serviceManager.CommitteesAndAssociationsService
                .UpdateCommitteeOrAssociationAsync(committeeOrAssociationId, committeeOrAssociationUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/CommitteesAndAssociations/{committeeOrAssociationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberCommitteeOrAssociationAsync(int committeeOrAssociationId , string memberEmail)
        {
            await _serviceManager.CommitteesAndAssociationsService
                .DeleteCommitteeOrAssociationAsync(committeeOrAssociationId , memberEmail);

            return NoContent();
        }

        #endregion

        #region ParticipationInMagazines

        [ProducesResponseType(typeof(PaginatedResult<ParticipationInMagazinesResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ParticipationInMagazines")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<PaginatedResult<ParticipationInMagazinesResponseDto>>> GetFacultyMemberParticipationInMagazinesAsync(
            [FromQuery] ParticipationInMagazinesSpecificationsParameters parameters)
            => Ok(await _serviceManager.ParticipationInMagazinesService
                .GetAllParticipationInMagazinesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ParticipationInMagazines/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> GetFacultyMemberParticipationInMagazineByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.ParticipationInMagazinesService
                .GetParticipationInMagazineByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ParticipationInMagazines")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Create")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> CreateFacultyMemberParticipationInMagazineAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ParticipationInMagazineCreateDto participationInMagazinesCreateDto)
            => Ok(await _serviceManager.ParticipationInMagazinesService
                .CreateParticipationInMagazineAsync(participationInMagazinesCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ParticipationInMagazines/{participationInMagazineId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Update")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> UpdateFacultyMemberParticipationInMagazineAsync(
            int participationInMagazineId,string memberEmail,
            [FromBody] ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto)
            => Ok(await _serviceManager.ParticipationInMagazinesService
                .UpdateParticipationInMagazineAsync(participationInMagazineId, participationInMagazinesUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ParticipationInMagazines/{participationInMagazineId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberParticipationInMagazineAsync(int participationInMagazineId , string memberEmail)
        {
            await _serviceManager.ParticipationInMagazinesService
                .DeleteParticipationInMagazineAsync(participationInMagazineId , memberEmail);

            return NoContent();
        }

        #endregion

        #region Projects

        [ProducesResponseType(typeof(PaginatedResult<ProjectsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/Projects")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<PaginatedResult<ProjectsResponseDto>>> GetFacultyMemberProjectsAsync(
            [FromQuery] ProjectsSpecifcationsParameters parameters)
            => Ok(await _serviceManager.ProjectsService
                .GetAllProjectsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/Projects/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<ProjectsResponseDto>> GetFacultyMemberProjectByIdAsync(int id, string memberEmail)
            => Ok(await _serviceManager.ProjectsService
                .GetProjectByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/Projects")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Create")]
        public async Task<ActionResult<ProjectsResponseDto>> CreateFacultyMemberProjectAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ProjectCreateDto projectCreateDto)
            => Ok(await _serviceManager.ProjectsService
                .CreateProjectAsync(projectCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/Projects/{projectId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Update")]
        public async Task<ActionResult<ProjectsResponseDto>> UpdateFacultyMemberProjectAsync(
            int projectId, string memberEmail,
            [FromBody] ProjectUpdateDto projectUpdateDto)
            => Ok(await _serviceManager.ProjectsService
                .UpdateProjectAsync(projectId, projectUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/Projects/{projectId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberProjectAsync(int projectId , string memberEmail)
        {
            await _serviceManager.ProjectsService
                .DeleteProjectAsync(projectId , memberEmail);

            return NoContent();
        }

        #endregion

        #region ReviewingArticles

        [ProducesResponseType(typeof(PaginatedResult<ReviewingArticlesDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ReviewingArticles")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<PaginatedResult<ReviewingArticlesDto>>> GetFacultyMemberReviewingArticlesAsync(
     [FromQuery] ReviewingArticlesSpecificationsParameters parameters)
     => Ok(await _serviceManager.ReviewingArticlesService
         .GetAllReviewingArticlesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ReviewingArticles/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<ReviewingArticlesDto>> GetFacultyMemberReviewingArticleByIdAsync(int id, string memberEmail)
            => Ok(await _serviceManager.ReviewingArticlesService
                .GetReviewingArticleByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ReviewingArticles")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Create")]
        public async Task<ActionResult<ReviewingArticlesDto>> CreateFacultyMemberReviewingArticleAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ReviewingArticleCreateDto reviewingArticleCreateDto)
            => Ok(await _serviceManager.ReviewingArticlesService
                .CreateReviewingArticleAsync(reviewingArticleCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ReviewingArticles/{reviewingArticleId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Update")]
        public async Task<ActionResult<ReviewingArticlesDto>> UpdateFacultyMemberReviewingArticleAsync(
            int reviewingArticleId, string memberEmail,
            [FromBody] ReviewArticleUpdateDto reviewingArticleUpdateDto)
            => Ok(await _serviceManager.ReviewingArticlesService
                .UpdateReviewingArticleAsync(reviewingArticleId, reviewingArticleUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ReviewingArticles/{reviewingArticleId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberReviewingArticleAsync(int reviewingArticleId , string memberEmail)
        {
            await _serviceManager.ReviewingArticlesService
                .DeleteReviewingArticleAsync(reviewingArticleId , memberEmail);

            return NoContent();
        }

        #endregion

        #endregion

        #region FacultyMemberScientificProgressionModuleManagementModule

        #region AcademicQualification

           
        [ProducesResponseType(typeof(PaginatedResult<AcademicQualificationResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/AcademicQualifications")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<PaginatedResult<AcademicQualificationResponseDto>>> GetFacultyMemberAcademicQualificationsAsync(
            [FromQuery] AcademicQualificationsSpecificationParamters parameters)
            => Ok(await _serviceManager.AcademicQualificationsService
                .GetAllAcademicQualificationsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/AcademicQualifications/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> GetFacultyMemberAcademicQualificationByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.AcademicQualificationsService
                .GetAcademicQualificationByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/AcademicQualifications")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Create")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> CreateFacultyMemberAcademicQualificationAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] AcademicQualificationCreateDto academicQualificationCreateDto)
            => Ok(await _serviceManager.AcademicQualificationsService
                .CreateAcademicQualificationAsync(academicQualificationCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/AcademicQualifications/{academicQualificationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Update")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> UpdateFacultyMemberAcademicQualificationAsync(
            int academicQualificationId, string memberEmail,
            [FromBody] AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
            => Ok(await _serviceManager.AcademicQualificationsService
                .UpdateAcademicQualificationAsync(academicQualificationId, academicQualificationsUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/AcademicQualifications/{academicQualificationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberAcademicQualificationAsync(int academicQualificationId , string memberEmail)
        {
            await _serviceManager.AcademicQualificationsService
                .DeleteAcademicQualificationAsync(academicQualificationId , memberEmail);

            return NoContent();
        }

        #endregion

        #region AdminstrtivePostions


        [ProducesResponseType(typeof(PaginatedResult<AdministrativePositionDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/AdministrativePositions")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<PaginatedResult<AdministrativePositionDto>>> GetFacultyMemberAdministrativePositionsAsync(
     [FromQuery] AdministrativePositionsSpecificationParameters parameters)
     => Ok(await _serviceManager.AdministrativePositionsService
         .GetAllAdministrativePositionsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/AdministrativePositions/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<AdministrativePositionDto>> GetFacultyMemberAdministrativePositionByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.AdministrativePositionsService
                .GetAdministrativePositionByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/AdministrativePositions")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Create")]
        public async Task<ActionResult<AdministrativePositionDto>> CreateFacultyMemberAdministrativePositionAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] AdministrativePositionCreateDto administrativePositionCreateDto)
            => Ok(await _serviceManager.AdministrativePositionsService
                .CreateAdministrativePositionAsync(administrativePositionCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/AdministrativePositions/{administrativePositionId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Update")]
        public async Task<ActionResult<AdministrativePositionDto>> UpdateFacultyMemberAdministrativePositionAsync(
            int administrativePositionId, string memberEmail,
            [FromBody] AdministrativePositionDto administrativePositionUpdateDto)
            => Ok(await _serviceManager.AdministrativePositionsService
                .UpdateAdministrativePositionAsync(administrativePositionId, administrativePositionUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/AdministrativePositions/{administrativePositionId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberAdministrativePositionAsync(int administrativePositionId, string memberEmail)
        {
            await _serviceManager.AdministrativePositionsService
                .DeleteAdministrativePositionAsync(administrativePositionId , memberEmail);

            return NoContent();
        }

        #endregion

        #region JobRanks

        [ProducesResponseType(typeof(PaginatedResult<JobRankResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/JobRanks")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<PaginatedResult<JobRankResponseDto>>> GetFacultyMemberJobRanksAsync(
          [FromQuery] JobRanksSpecificationsParameters parameters)
          => Ok(await _serviceManager.JobRanksService
              .GetAllAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/JobRanks/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<JobRankResponseDto>> GetFacultyMemberJobRankByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.JobRanksService
                .GetByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/JobRanks")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Create")]
        public async Task<ActionResult<JobRankResponseDto>> CreateFacultyMemberJobRankAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] JobRankCreateDto jobRanksCreateDto)
            => Ok(await _serviceManager.JobRanksService
                .CreateAsync(jobRanksCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/JobRanks/{jobRankId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Update")]
        public async Task<ActionResult<JobRankResponseDto>> UpdateFacultyMemberJobRankAsync(
            int jobRankId, string facultyMemberEmail,
            [FromBody] JobRankUpdateDto jobRanksUpdateDto)
            => Ok(await _serviceManager.JobRanksService
                .UpdateAsync(jobRankId, jobRanksUpdateDto , facultyMemberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/JobRanks/{jobRankId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberJobRankAsync(int jobRankId , string memberEmail)
        {
            await _serviceManager.JobRanksService
                .DeleteAsync(jobRankId , memberEmail);

            return NoContent();
        }

        #endregion

        #endregion

        #region FacultyMemberWritingsAndPatentsModule

        #region Patents

        [ProducesResponseType(typeof(PaginatedResult<PatentsResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/Patents")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        public async Task<ActionResult<PaginatedResult<PatentsResponseDTO>>> GetFacultyMemberPatentsAsync(
    [FromQuery] PatentsSpecificationParameters parameters)
    => Ok(await _serviceManager.PatentsService
        .GetAllPatentsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(PatentsResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/Patents/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        public async Task<ActionResult<PatentsResponseDTO>> GetFacultyMemberPatentByIdAsync(int id, string memberEmail  )
            => Ok(await _serviceManager.PatentsService
                .GetPatentByIdAsync(id  , memberEmail));

        [ProducesResponseType(typeof(PatentsResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/Patents")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Create")]
        public async Task<ActionResult<PatentsResponseDTO>> CreateFacultyMemberPatentAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] PatentsCreateDTO patentCreateDto)
            => Ok(await _serviceManager.PatentsService
                .CreatePatentAsync(patentCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(PatentsResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/Patents/{patentId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Update")]
        public async Task<ActionResult<PatentsResponseDTO>> UpdateFacultyMemberPatentAsync(
            int patentId, string memberEmail,
            [FromBody] PatentsUpdateDTO patentUpdateDto)
            => Ok(await _serviceManager.PatentsService
                .UpdatePatentAsync(patentId, patentUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/Patents/{patentId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberPatentAsync(int patentId, string memberEmail)
        {
            await _serviceManager.PatentsService
                .DeletePatentAsync(patentId , memberEmail);

            return NoContent();
        }

        #endregion

        #region Writings


        [ProducesResponseType(typeof(PaginatedResult<ScientificWritingsResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ScientificWritings")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        public async Task<ActionResult<PaginatedResult<ScientificWritingsResponseDTO>>> GetFacultyMemberScientificWritingsAsync(
    [FromQuery] ScientificWritingsSpecificationParameters parameters)
    => Ok(await _serviceManager.ScientificWritingsService
        .GetAllScientificWritingsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ScientificWritingsResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ScientificWritings/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        public async Task<ActionResult<ScientificWritingsResponseDTO>> GetFacultyMemberScientificWritingByIdAsync(int id , string memberEmail)
            => Ok(await _serviceManager.ScientificWritingsService
                .GetScientificWritingByIdAsync(id , memberEmail));

        [ProducesResponseType(typeof(ScientificWritingsResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ScientificWritings")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Create")]
        public async Task<ActionResult<ScientificWritingsResponseDTO>> CreateFacultyMemberScientificWritingAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ScientificWritingsCreateDTO scientificWritingCreateDto)
            => Ok(await _serviceManager.ScientificWritingsService
                .CreateScientificWritingAsync(scientificWritingCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ScientificWritingsResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ScientificWritings/{scientificWritingId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Update")]
        public async Task<ActionResult<ScientificWritingsResponseDTO>> UpdateFacultyMemberScientificWritingAsync(
            int scientificWritingId, string memberEmail,
            [FromBody] ScientificWritingsUpdateDTO scientificWritingUpdateDto)
            => Ok(await _serviceManager.ScientificWritingsService
                .UpdateScientificWritingAsync(scientificWritingId, scientificWritingUpdateDto , memberEmail));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ScientificWritings/{scientificWritingId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberScientificWritingAsync(int scientificWritingId , string memberEmail)
        {
            await _serviceManager.ScientificWritingsService
                .DeleteScientificWritingAsync(scientificWritingId , memberEmail);

            return NoContent();
        }


        #endregion

        #endregion

        #region FacultyMemberResearchesManagementModule

        #region Researches

        [ProducesResponseType(typeof(ResearcherProfileResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Admin/FacultyMember/Researches/ResearcherProfile/{memberId}")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        public async Task<ActionResult<ResearcherProfileResponseDTO>> GetAdminFacultyMemberResearcherProfileAsync(
            [FromQuery] Guid memberId)
            => Ok(await _serviceManager.ResearcherProfileService
                .GetResearcherProfile(memberId));


        [ProducesResponseType(typeof(PaginatedResult<ResearchResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Admin/FacultyMember/Researches")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        public async Task<ActionResult<PaginatedResult<ResearchResponseDTO>>> GetAdminFacultyMemberResearchesAsync(
    [FromQuery] ResearchSpecificationParameters parameters)
    => Ok(await _serviceManager.ResearchesService
        .GetAllResearches(parameters, parameters.FacultyMemberId));


        [ProducesResponseType(typeof(PaginatedResult<ResearchResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Admin/FacultyMember/Researches/Recommended")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        public async Task<ActionResult<PaginatedResult<ResearchResponseDTO>>> GetAdminFacultyMemberRecommendedResearchesAsync(
            [FromQuery] ResearchSpecificationParameters parameters)
            => Ok(await _serviceManager.ResearchesService
                .GetAllRecommendedResearches(parameters, parameters.FacultyMemberId));


        [ProducesResponseType(typeof(ResearchResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Admin/FacultyMember/Researches/{researchId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        public async Task<ActionResult<ResearchResponseDTO>> GetAdminFacultyMemberResearchByIdAsync(
            int researchId,
            [FromQuery] Guid facultyMemberId)
            => Ok(await _serviceManager.ResearchesService
                .GetResarchById(researchId, facultyMemberId));


        [ProducesResponseType(typeof(ResearchResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("Admin/FacultyMember/Researches")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Create")]
        public async Task<ActionResult<ResearchResponseDTO>> CreateAdminFacultyMemberResearchAsync(
            [FromQuery] Guid facultyMemberId,
            [FromBody] ResearchDTO researchDto)
            => Ok(await _serviceManager.ResearchesService
                .AddResearch(researchDto, facultyMemberId));


        [ProducesResponseType(typeof(ResearchResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Admin/FacultyMember/Researches/{researchId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Update")]
        public async Task<ActionResult<ResearchResponseDTO>> UpdateAdminFacultyMemberResearchAsync(
            int researchId,
            [FromQuery] Guid facultyMemberId,
            [FromBody] ResearchUpdateDTO researchUpdateDto)
            => Ok(await _serviceManager.ResearchesService
                .UpdateResearch(researchId, researchUpdateDto, facultyMemberId));


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("Admin/FacultyMember/Researches/{researchId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Delete")]
        public async Task<IActionResult> DeleteAdminFacultyMemberResearchAsync(
            int researchId,
            [FromQuery] Guid facultyMemberId)
        {
            await _serviceManager.ResearchesService
                .DeleteResearch(researchId, facultyMemberId);

            return NoContent();
        }


        [ProducesResponseType(typeof(ResearchResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Admin/FacultyMember/Researches/{researchId:int}/Confirm")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Update")]
        public async Task<ActionResult<ResearchResponseDTO>> ConfirmAdminFacultyMemberResearchAsync(
            int researchId,
            [FromQuery] Guid facultyMemberId)
            => Ok(await _serviceManager.ResearchesService
                .ConfirmRecommendedResearch(researchId, facultyMemberId));


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("Admin/FacultyMember/Researches/{researchId:int}/Reject")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Update")]
        public async Task<IActionResult> RejectAdminFacultyMemberResearchAsync(
            int researchId,
            [FromQuery] Guid facultyMemberId)
        {
            await _serviceManager.ResearchesService
                .RejectResearch(researchId, facultyMemberId);

            return NoContent();
        }

        #endregion

        #region Supervisings

        [ProducesResponseType(typeof(PaginatedResult<SupervisingThsesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Admin/FacultyMember/HigherStudies/ThesesSupervisings")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Read")]
        public async Task<ActionResult<PaginatedResult<SupervisingThsesResponseDTO>>> GetAdminFacultyMemberThesesSupervisingsAsync(
    [FromQuery] ThesesSupervisingSpecificationParameters parameters)
    => Ok(await _serviceManager.ThesesSupervisingService
        .GetAllSupervisings(parameters, parameters.FacultyMemberId));


        [ProducesResponseType(typeof(SupervisingThsesResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Admin/FacultyMember/HigherStudies/ThesesSupervisings/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Read")]
        public async Task<ActionResult<SupervisingThsesResponseDTO>> GetAdminFacultyMemberThesesSupervisingByIdAsync(
            int id,
            [FromQuery] Guid facultyMemberId)
            => Ok(await _serviceManager.ThesesSupervisingService
                .GetThesesSupervisingById(id, facultyMemberId));


        [ProducesResponseType(typeof(SupervisingThesesAddDTO), StatusCodes.Status200OK)]
        [HttpPost("Admin/FacultyMember/HigherStudies/ThesesSupervisings")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Create")]
        public async Task<ActionResult<SupervisingThesesAddDTO>> CreateAdminFacultyMemberThesesSupervisingAsync(
            [FromQuery] Guid facultyMemberId,
            [FromBody] SupervisingThesesAddDTO thesesDTO)
            => Ok(await _serviceManager.ThesesSupervisingService
                .AddThesesSupervising(thesesDTO, facultyMemberId));


        [ProducesResponseType(typeof(SupervisingThsesResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Admin/FacultyMember/HigherStudies/ThesesSupervisings/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Update")]
        public async Task<ActionResult<SupervisingThsesResponseDTO>> UpdateAdminFacultyMemberThesesSupervisingAsync(
            int id,
            [FromQuery] Guid facultyMemberId,
            [FromBody] SupervisingThesesUpdateDTO supervisingThesesUpdateDTO)
            => Ok(await _serviceManager.ThesesSupervisingService
                .UpdateThesesSupervising(id, supervisingThesesUpdateDTO, facultyMemberId));


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("Admin/FacultyMember/HigherStudies/ThesesSupervisings/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Delete")]
        public async Task<IActionResult> DeleteAdminFacultyMemberThesesSupervisingAsync(
            int id,
            [FromQuery] Guid facultyMemberId)
        {
            await _serviceManager.ThesesSupervisingService
                .DeleteThesesSupervising(id, facultyMemberId);

            return NoContent();
        }

        #endregion

        #region Theses

        [ProducesResponseType(typeof(PaginatedResult<ThesesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Admin/FacultyMember/HigherStudies/Theses")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Read")]
        public async Task<ActionResult<PaginatedResult<ThesesResponseDTO>>> GetAdminFacultyMemberThesesAsync(
    [FromQuery] ThesesSpecificationParameters parameters)
    => Ok(await _serviceManager.ThesesService
        .GetAllTheses(parameters, parameters.FacultyMemberId));


        [ProducesResponseType(typeof(ThesesResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Admin/FacultyMember/HigherStudies/Theses/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Read")]
        public async Task<ActionResult<ThesesResponseDTO>> GetAdminFacultyMemberThesisByIdAsync(
            int id,
            [FromQuery] Guid facultyMemberId)
            => Ok(await _serviceManager.ThesesService
                .GetThesesById(id, facultyMemberId));


        [ProducesResponseType(typeof(ThesesResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("Admin/FacultyMember/HigherStudies/Theses")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Create")]
        public async Task<ActionResult<ThesesResponseDTO>> CreateAdminFacultyMemberThesisAsync(
            [FromQuery] Guid facultyMemberId,
            [FromBody] ThesesDTO thesesDto)
            => Ok(await _serviceManager.ThesesService
                .AddTheses(thesesDto, facultyMemberId));


        [ProducesResponseType(typeof(ThesesResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Admin/FacultyMember/HigherStudies/Theses/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Update")]
        public async Task<ActionResult<ThesesResponseDTO>> UpdateAdminFacultyMemberThesisAsync(
            int id,
            [FromQuery] Guid facultyMemberId,
            [FromBody] ThesesUpdateDTO thesesUpdateDto)
            => Ok(await _serviceManager.ThesesService
                .UpdateTheses(id, thesesUpdateDto, facultyMemberId));


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("Admin/FacultyMember/HigherStudies/Theses/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberHigherStudiesData.Delete")]
        public async Task<IActionResult> DeleteAdminFacultyMemberThesisAsync(
            int id,
            [FromQuery] Guid facultyMemberId)
        {
            await _serviceManager.ThesesService
                .DeleteTheses(id, facultyMemberId);

            return NoContent();
        }


        #endregion

        #endregion

    }
}
