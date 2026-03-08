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
            => Ok(await _serviceManager.FacultyMemberMainDataManagementService
                .GetMemberPersonalDataAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(PersonalDataResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/PersonalData")]
        [Authorize(Policy = "Permission:FacultyMemberData.Update")]
        public async Task<ActionResult<PersonalDataResponseDto>> UpdateFacultyMemberPersonalDataAsync(
            [FromQuery] string facultyMemberEmail,
            PersonalDataUpdateDto personalDataUpdateDto)
            => Ok(await _serviceManager.FacultyMemberMainDataManagementService
                .UpdateMemberPersonalDataAsync(personalDataUpdateDto, facultyMemberEmail));

        #endregion

        #region Contact Data

        [ProducesResponseType(typeof(ContactDataResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ContactData")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        public async Task<ActionResult<ContactDataResponseDto>> GetFacultyMemberContactDataAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberMainDataManagementService
                .GetMemberContactDataAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(ContactDataResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ContactData")]
        [Authorize(Policy = "Permission:FacultyMemberData.Update")]
        public async Task<ActionResult<ContactDataResponseDto>> UpdateFacultyMemberContactDataAsync(
            [FromQuery] string facultyMemberEmail,
            ContactDataUpdateDto contactDataUpdateDto)
            => Ok(await _serviceManager.FacultyMemberMainDataManagementService
                .UpdateMemberContactDataAsync(contactDataUpdateDto, facultyMemberEmail));

        #endregion

        #region Identification Card

        [ProducesResponseType(typeof(IdentificationCardDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/IdentificationCard")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        public async Task<ActionResult<IdentificationCardDto>> GetFacultyMemberIdentificationCardAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberMainDataManagementService
                .GetMemberIdentificationCardAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(IdentificationCardDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/IdentificationCard")]
        [Authorize(Policy = "Permission:FacultyMemberData.Update")]
        public async Task<ActionResult<IdentificationCardDto>> UpdateFacultyMemberIdentificationCardAsync(
            [FromQuery] string facultyMemberEmail,
            IdentificationCardDto identificationCardDto)
            => Ok(await _serviceManager.FacultyMemberMainDataManagementService
                .UpdateMemberIdentificationCardAsync(identificationCardDto, facultyMemberEmail));

        #endregion

        #region Social Media

        [ProducesResponseType(typeof(SocialMediaPlatformsDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/SocialMediaPlatforms")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        public async Task<ActionResult<SocialMediaPlatformsDto>> GetFacultyMemberSocialMediaPlatformsAsync([FromQuery] string facultyMemberEmail)
            => Ok(await _serviceManager.FacultyMemberMainDataManagementService
                .GetMemberSocialMediaPlatformsAsync(facultyMemberEmail));

        [ProducesResponseType(typeof(SocialMediaPlatformsDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/SocialMediaPlatforms")]
        [Authorize(Policy = "Permission:FacultyMemberData.Update")]
        public async Task<ActionResult<SocialMediaPlatformsDto>> UpdateFacultyMemberSocialMediaPlatformsAsync(
            [FromQuery] string facultyMemberEmail,
            SocialMediaPlatformsDto socialMediaPlatformsDto)
            => Ok(await _serviceManager.FacultyMemberMainDataManagementService
                .UpdateMemberSocialMediaPlatformsAsync(socialMediaPlatformsDto, facultyMemberEmail));

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
        => Ok(await _serviceManager.CommunityServiceManagementService
            .GetFacultyMemberContributionsToCommunityServiceAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ContributionsToCommunityServiceResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ContributionsToCommunityService/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<ContributionsToCommunityServiceResponseDTO>> GetFacultyMemberContributionToCommunityServiceByIdAsync(int id)
            => Ok(await _serviceManager.CommunityServiceManagementService
                .GetFacultyMemberContributionToCommunityServiceByIdAsync(id));

        [ProducesResponseType(typeof(ContributionsToCommunityServiceResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ContributionsToCommunityService")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Create")]
        public async Task<ActionResult<ContributionsToCommunityServiceResponseDTO>> CreateFacultyMemberContributionToCommunityServiceAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto)
            => Ok(await _serviceManager.CommunityServiceManagementService
                .CreateFacultyMemberContributionToCommunityServiceAsync(contributionsToCommunityServiceCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ContributionsToCommunityServiceResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ContributionsToCommunityService/{contributionToCommunityServiceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Update")]
        public async Task<ActionResult<ContributionsToCommunityServiceResponseDTO>> UpdateFacultyMemberContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId,
            [FromBody] ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto)
            => Ok(await _serviceManager.CommunityServiceManagementService
                .UpdateFacultyMemberContributionToCommunityServiceAsync(
                    contributionToCommunityServiceId,
                    contributionsToCommunityServiceUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ContributionsToCommunityService/{contributionToCommunityServiceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberContributionToCommunityServiceAsync(int contributionToCommunityServiceId)
        {
            await _serviceManager.CommunityServiceManagementService
                .DeleteFacultyMemberContributionToCommunityServiceAsync(contributionToCommunityServiceId);

            return NoContent();
        }

        #endregion

        #region ContributionsToUniverstiy


        [ProducesResponseType(typeof(PaginatedResult<ContributionsToUniversityResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ContributionsToUniversity")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<PaginatedResult<ContributionsToUniversityResponseDTO>>> GetFacultyMemberContributionsToUniversityAsync(
           [FromQuery] ContributionsToUniversitySpecificationParameters parameters)
           => Ok(await _serviceManager.ContributionsToUniversityManagementService
               .GetFacultyMemberContributionsToUniversityAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ContributionsToUniversityResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ContributionsToUniversity/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<ContributionsToUniversityResponseDTO>> GetFacultyMemberContributionToUniversityByIdAsync(int id)
            => Ok(await _serviceManager.ContributionsToUniversityManagementService
                .GetFacultyMemberContributionToUniversityByIdAsync(id));

        [ProducesResponseType(typeof(ContributionsToUniversityResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ContributionsToUniversity")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Create")]
        public async Task<ActionResult<ContributionsToUniversityResponseDTO>> CreateFacultyMemberContributionToUniversityAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto)
            => Ok(await _serviceManager.ContributionsToUniversityManagementService
                .CreateFacultyMemberContributionToUniversityAsync(contributionsToUniversityCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ContributionsToUniversityResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ContributionsToUniversity/{contributionToUniversityId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Update")]
        public async Task<ActionResult<ContributionsToUniversityResponseDTO>> UpdateFacultyMemberContributionToUniversityAsync(
            int contributionToUniversityId,
            [FromBody] ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto)
            => Ok(await _serviceManager.ContributionsToUniversityManagementService
                .UpdateFacultyMemberContributionToUniversityAsync(contributionToUniversityId, contributionsToUniversityUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ContributionsToUniversity/{contributionToUniversityId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberContributionToUniversityAsync(int contributionToUniversityId)
        {
            await _serviceManager.ContributionsToUniversityManagementService
                .DeleteFacultyMemberContributionToUniversityAsync(contributionToUniversityId);

            return NoContent();
        }

        #endregion

        #region ParticipationInQualityWorks

        [ProducesResponseType(typeof(PaginatedResult<ParticipationInQualityWorksResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ParticipationInQualityWorks")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<PaginatedResult<ParticipationInQualityWorksResponseDTO>>> GetFacultyMemberParticipationsInQualityWorksAsync(
            [FromQuery] ParticipationInQualityWorksSpecificationParameters parameters)
            => Ok(await _serviceManager.ParticipationInQualityWorksManagementService
                .GetFacultyMemberParticipationsInQualityWorksAsync(parameters, parameters.FacultyMemberEmail));


        [ProducesResponseType(typeof(ParticipationInQualityWorksResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ParticipationInQualityWorks/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        public async Task<ActionResult<ParticipationInQualityWorksResponseDTO>> GetFacultyMemberParticipationInQualityWorksByIdAsync(int id)
            => Ok(await _serviceManager.ParticipationInQualityWorksManagementService
                .GetFacultyMemberParticipationInQualityWorksByIdAsync(id));


        [ProducesResponseType(typeof(ParticipationInQualityWorksResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ParticipationInQualityWorks")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Create")]
        public async Task<ActionResult<ParticipationInQualityWorksResponseDTO>> CreateFacultyMemberParticipationInQualityWorksAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto)
            => Ok(await _serviceManager.ParticipationInQualityWorksManagementService
                .CreateFacultyMemberParticipationInQualityWorksAsync(participationInQualityWorksCreateDto, facultyMemberEmail));


        [ProducesResponseType(typeof(ParticipationInQualityWorksResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ParticipationInQualityWorks/{participationInQualityWorksId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Update")]
        public async Task<ActionResult<ParticipationInQualityWorksResponseDTO>> UpdateFacultyMemberParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            [FromBody] ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto)
            => Ok(await _serviceManager.ParticipationInQualityWorksManagementService
                .UpdateFacultyMemberParticipationInQualityWorksAsync(participationInQualityWorksId, participationInQualityWorksUpdateDto));


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ParticipationInQualityWorks/{participationInQualityWorksId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberParticipationInQualityWorksAsync(int participationInQualityWorksId)
        {
            await _serviceManager.ParticipationInQualityWorksManagementService
                .DeleteFacultyMemberParticipationInQualityWorksAsync(participationInQualityWorksId);

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
            => Ok(await _serviceManager.GeneralExperiencesManagementService
                .GetFacultyMemberGeneralExperiencesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(GeneralExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/GeneralExperiences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Read")]
        public async Task<ActionResult<GeneralExperiencesResponseDTO>> GetFacultyMemberGeneralExperienceByIdAsync(int id)
            => Ok(await _serviceManager.GeneralExperiencesManagementService
                .GetFacultyMemberGeneralExperienceByIdAsync(id));

        [ProducesResponseType(typeof(GeneralExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/GeneralExperiences")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Create")]
        public async Task<ActionResult<GeneralExperiencesResponseDTO>> CreateFacultyMemberGeneralExperienceAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] GeneralExperiencesCreateDTO generalExperienceCreateDto)
            => Ok(await _serviceManager.GeneralExperiencesManagementService
                .CreateFacultyMemberGeneralExperienceAsync(generalExperienceCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(GeneralExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/GeneralExperiences/{generalExperienceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Update")]
        public async Task<ActionResult<GeneralExperiencesResponseDTO>> UpdateFacultyMemberGeneralExperienceAsync(
            int generalExperienceId,
            [FromBody] GeneralExperiencesUpdateDTO generalExperienceUpdateDto)
            => Ok(await _serviceManager.GeneralExperiencesManagementService
                .UpdateFacultyMemberGeneralExperienceAsync(generalExperienceId, generalExperienceUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/GeneralExperiences/{generalExperienceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberGeneralExperienceAsync(int generalExperienceId)
        {
            await _serviceManager.GeneralExperiencesManagementService
                .DeleteFacultyMemberGeneralExperienceAsync(generalExperienceId);

            return NoContent();
        }

    #endregion

        #region TeachingExperinces

        [ProducesResponseType(typeof(PaginatedResult<TeachingExperiencesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/TeachingExperiences")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Read")]
        public async Task<ActionResult<PaginatedResult<TeachingExperiencesResponseDTO>>> GetFacultyMemberTeachingExperiencesAsync(
            [FromQuery] TeachingExperiencesSpecificationParameters parameters)
            => Ok(await _serviceManager.TeachingExperiencesManagementService
                .GetFacultyMemberTeachingExperiencesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(TeachingExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/TeachingExperiences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Read")]
        public async Task<ActionResult<TeachingExperiencesResponseDTO>> GetFacultyMemberTeachingExperienceByIdAsync(int id)
            => Ok(await _serviceManager.TeachingExperiencesManagementService
                .GetFacultyMemberTeachingExperienceByIdAsync(id));

        [ProducesResponseType(typeof(TeachingExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/TeachingExperiences")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Create")]
        public async Task<ActionResult<TeachingExperiencesResponseDTO>> CreateFacultyMemberTeachingExperienceAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] TeachingExperiencesCreateDTO teachingExperienceCreateDto)
            => Ok(await _serviceManager.TeachingExperiencesManagementService
                .CreateFacultyMemberTeachingExperienceAsync(teachingExperienceCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(TeachingExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/TeachingExperiences/{teachingExperienceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Update")]
        public async Task<ActionResult<TeachingExperiencesResponseDTO>> UpdateFacultyMemberTeachingExperienceAsync(
            int teachingExperienceId,
            [FromBody] TeachingExperiencesUpdateDTO teachingExperienceUpdateDto)
            => Ok(await _serviceManager.TeachingExperiencesManagementService
                .UpdateFacultyMemberTeachingExperienceAsync(teachingExperienceId, teachingExperienceUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/TeachingExperiences/{teachingExperienceId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberTeachingExperienceAsync(int teachingExperienceId)
        {
            await _serviceManager.TeachingExperiencesManagementService
                .DeleteFacultyMemberTeachingExperienceAsync(teachingExperienceId);

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
           => Ok(await _serviceManager.ScientificMissionsManagementService
               .GetFacultyMemberScientificMissionsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ScientificMissionResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ScientificMissions/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<ScientificMissionResponseDto?>> GetFacultyMemberScientificMissionByIdAsync(int id)
            => Ok(await _serviceManager.ScientificMissionsManagementService
                .GetFacultyMemberScientificMissionByIdAsync(id));

        [ProducesResponseType(typeof(ScientificMissionResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ScientificMissions")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Create")]
        public async Task<ActionResult<ScientificMissionResponseDto>> CreateFacultyMemberScientificMissionAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ScientificMissionCreateDto scientificMissionCreateDto)
            => Ok(await _serviceManager.ScientificMissionsManagementService
                .CreateFacultyMemberScientificMissionAsync(scientificMissionCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ScientificMissionResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ScientificMissions/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Update")]
        public async Task<ActionResult<ScientificMissionResponseDto>> UpdateFacultyMemberScientificMissionAsync(
            int id,
            [FromBody] ScientificMissionUpdateDto scientificMissionUpdateDto)
            => Ok(await _serviceManager.ScientificMissionsManagementService
                .UpdateFacultyMemberScientificMissionAsync(id, scientificMissionUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ScientificMissions/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberScientificMissionAsync(int id)
        {
            await _serviceManager.ScientificMissionsManagementService
                .DeleteFacultyMemberScientificMissionAsync(id);

            return NoContent();
        }



        #endregion

        #region ConferencesAndSeminars

        [ProducesResponseType(typeof(PaginatedResult<ConferencesAndSeminarsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/SeminarsAndConferences")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<PaginatedResult<ConferencesAndSeminarsResponseDto>>> GetFacultyMemberSeminarsAndConferencesAsync(
            [FromQuery] SeminarsAndConferncesSpecificationParameters parameters)
            => Ok(await _serviceManager.SeminarsAndConferencesManagementService
                .GetFacultyMemberSeminarsAndConferencesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ConferencesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/SeminarsAndConferences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<ConferencesAndSeminarsResponseDto>> GetFacultyMemberSeminarOrConferenceByIdAsync(int id)
            => Ok(await _serviceManager.SeminarsAndConferencesManagementService
                .GetFacultyMemberSeminarOrConferenceByIdAsync(id));

        [ProducesResponseType(typeof(ConferencesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/SeminarsAndConferences")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Create")]
        public async Task<ActionResult<ConferencesAndSeminarsResponseDto>> CreateFacultyMemberSeminarOrConferenceAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto)
            => Ok(await _serviceManager.SeminarsAndConferencesManagementService
                .CreateFacultyMemberSeminarOrConferenceAsync(conferencesAndSeminarsCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ConferencesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/SeminarsAndConferences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Update")]
        public async Task<ActionResult<ConferencesAndSeminarsResponseDto>> UpdateFacultyMemberSeminarOrConferenceAsync(
            int id,
            [FromBody] ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto)
            => Ok(await _serviceManager.SeminarsAndConferencesManagementService
                .UpdateFacultyMemberSeminarOrConferenceAsync(id, conferencesAndSeminarsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/SeminarsAndConferences/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberSeminarOrConferenceAsync(int id)
        {
            await _serviceManager.SeminarsAndConferencesManagementService
                .DeleteFacultyMemberSeminarOrConferenceAsync(id);

            return NoContent();
        }

        #endregion

        #region TrainingPrograms

        [ProducesResponseType(typeof(PaginatedResult<TrainingProgramsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/TrainingPrograms")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<PaginatedResult<TrainingProgramsResponseDto>>> GetFacultyMemberTrainingProgramsAsync(
         [FromQuery] TrainingProgramsSpecificationParameters parameters)
         => Ok(await _serviceManager.TrainingProgramsManagementService
             .GetFacultyMemberTrainingProgramsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(TrainingProgramsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/TrainingPrograms/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        public async Task<ActionResult<TrainingProgramsResponseDto>> GetFacultyMemberTrainingProgramByIdAsync(int id)
            => Ok(await _serviceManager.TrainingProgramsManagementService
                .GetFacultyMemberTrainingProgramByIdAsync(id));

        [ProducesResponseType(typeof(TrainingProgramsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/TrainingPrograms")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Create")]
        public async Task<ActionResult<TrainingProgramsResponseDto>> CreateFacultyMemberTrainingProgramAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] TrainingProgramsCreateDto trainingProgramsCreateDto)
            => Ok(await _serviceManager.TrainingProgramsManagementService
                .CreateFacultyMemberTrainingProgramAsync(trainingProgramsCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(TrainingProgramsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/TrainingPrograms/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Update")]
        public async Task<ActionResult<TrainingProgramsResponseDto>> UpdateFacultyMemberTrainingProgramAsync(
            int id,
            [FromBody] TrainingProgramsUpdateDto trainingProgramsUpdateDto)
            => Ok(await _serviceManager.TrainingProgramsManagementService
                .UpdateFacultyMemberTrainingProgramAsync(id, trainingProgramsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/TrainingPrograms/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberTrainingProgramAsync(int id)
        {
            await _serviceManager.TrainingProgramsManagementService
                .DeleteFacultyMemberTrainingProgramAsync(id);

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
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationManagementService
                .GetFacultyMemberManifestationsOfScientificAppreciationAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ManifestationsOfScientificAppreciationResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ManifestationsOfScientificAppreciation/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Read")]
        public async Task<ActionResult<ManifestationsOfScientificAppreciationResponseDTO>> GetFacultyMemberManifestationOfScientificAppreciationByIdAsync(int id)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationManagementService
                .GetFacultyMemberManifestationOfScientificAppreciationByIdAsync(id));

        [ProducesResponseType(typeof(ManifestationsOfScientificAppreciationResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ManifestationsOfScientificAppreciation")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Create")]
        public async Task<ActionResult<ManifestationsOfScientificAppreciationResponseDTO>> CreateFacultyMemberManifestationOfScientificAppreciationAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ManifestationsOfScientificAppreciationCreateDTO manifestationsOfScientificAppreciationCreateDto)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationManagementService
                .CreateFacultyMemberManifestationOfScientificAppreciationAsync(
                    manifestationsOfScientificAppreciationCreateDto,
                    facultyMemberEmail));

        [ProducesResponseType(typeof(ManifestationsOfScientificAppreciationResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ManifestationsOfScientificAppreciation/{manifestationsOfScientificAppreciationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Update")]
        public async Task<ActionResult<ManifestationsOfScientificAppreciationResponseDTO>> UpdateFacultyMemberManifestationOfScientificAppreciationAsync(
            int manifestationsOfScientificAppreciationId,
            [FromBody] ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDto)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationManagementService
                .UpdateFacultyMemberManifestationOfScientificAppreciationAsync(
                    manifestationsOfScientificAppreciationId,
                    manifestationsOfScientificAppreciationUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ManifestationsOfScientificAppreciation/{manifestationsOfScientificAppreciationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberManifestationOfScientificAppreciationAsync(
            int manifestationsOfScientificAppreciationId)
        {
            await _serviceManager.ManifestationsOfScientificAppreciationManagementService
                .DeleteFacultyMemberManifestationOfScientificAppreciationAsync(manifestationsOfScientificAppreciationId);

            return NoContent();
        }

        #endregion

        #region PrizesAndAwards

        [ProducesResponseType(typeof(PaginatedResult<PrizesAndRewardsResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/PrizesAndRewards")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Read")]
        public async Task<ActionResult<PaginatedResult<PrizesAndRewardsResponseDTO>>> GetFacultyMemberPrizesAndRewardsAsync(
          [FromQuery] PrizesAndRewardsSpecificationParameters parameters)
          => Ok(await _serviceManager.PrizesAndRewardsManagementService
              .GetFacultyMemberPrizesAndRewardsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(PrizesAndRewardsResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/PrizesAndRewards/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Read")]
        public async Task<ActionResult<PrizesAndRewardsResponseDTO>> GetFacultyMemberPrizeOrRewardByIdAsync(int id)
            => Ok(await _serviceManager.PrizesAndRewardsManagementService
                .GetFacultyMemberPrizeOrRewardByIdAsync(id));

        [ProducesResponseType(typeof(PrizesAndRewardsResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/PrizesAndRewards")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Create")]
        public async Task<ActionResult<PrizesAndRewardsResponseDTO>> CreateFacultyMemberPrizeOrRewardAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] PrizesAndRewardsCreateDTO prizesAndRewardsCreateDto)
            => Ok(await _serviceManager.PrizesAndRewardsManagementService
                .CreateFacultyMemberPrizeOrRewardAsync(prizesAndRewardsCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(PrizesAndRewardsResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/PrizesAndRewards/{prizesOrRewardId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Update")]
        public async Task<ActionResult<PrizesAndRewardsResponseDTO>> UpdateFacultyMemberPrizeOrRewardAsync(
            int prizesOrRewardId,
            [FromBody] PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDto)
            => Ok(await _serviceManager.PrizesAndRewardsManagementService
                .UpdateFacultyMemberPrizeOrRewardAsync(prizesOrRewardId, prizesAndRewardsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/PrizesAndRewards/{prizesOrRewardId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberPrizeOrRewardAsync(int prizesOrRewardId)
        {
            await _serviceManager.PrizesAndRewardsManagementService
                .DeleteFacultyMemberPrizeOrRewardAsync(prizesOrRewardId);

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
      => Ok(await _serviceManager.CommitteesAndAssociationsManagementService
          .GetFacultyMemberCommitteesAndAssociationsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/CommitteesAndAssociations/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> GetFacultyMemberCommitteeOrAssociationByIdAsync(int id)
            => Ok(await _serviceManager.CommitteesAndAssociationsManagementService
                .GetFacultyMemberCommitteeOrAssociationByIdAsync(id));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/CommitteesAndAssociations")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Create")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> CreateFacultyMemberCommitteeOrAssociationAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto)
            => Ok(await _serviceManager.CommitteesAndAssociationsManagementService
                .CreateFacultyMemberCommitteeOrAssociationAsync(committeeOrAssociationCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/CommitteesAndAssociations/{committeeOrAssociationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Update")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> UpdateFacultyMemberCommitteeOrAssociationAsync(
            int committeeOrAssociationId,
            [FromBody] CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto)
            => Ok(await _serviceManager.CommitteesAndAssociationsManagementService
                .UpdateFacultyMemberCommitteeOrAssociationAsync(committeeOrAssociationId, committeeOrAssociationUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/CommitteesAndAssociations/{committeeOrAssociationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberCommitteeOrAssociationAsync(int committeeOrAssociationId)
        {
            await _serviceManager.CommitteesAndAssociationsManagementService
                .DeleteFacultyMemberCommitteeOrAssociationAsync(committeeOrAssociationId);

            return NoContent();
        }

        #endregion

        #region ParticipationInMagazines

        [ProducesResponseType(typeof(PaginatedResult<ParticipationInMagazinesResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ParticipationInMagazines")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<PaginatedResult<ParticipationInMagazinesResponseDto>>> GetFacultyMemberParticipationInMagazinesAsync(
            [FromQuery] ParticipationInMagazinesSpecificationsParameters parameters)
            => Ok(await _serviceManager.ParticipationInMagazinesManagementService
                .GetFacultyMemberParticipationInMagazinesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ParticipationInMagazines/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> GetFacultyMemberParticipationInMagazineByIdAsync(int id)
            => Ok(await _serviceManager.ParticipationInMagazinesManagementService
                .GetFacultyMemberParticipationInMagazineByIdAsync(id));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ParticipationInMagazines")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Create")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> CreateFacultyMemberParticipationInMagazineAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ParticipationInMagazineCreateDto participationInMagazinesCreateDto)
            => Ok(await _serviceManager.ParticipationInMagazinesManagementService
                .CreateFacultyMemberParticipationInMagazineAsync(participationInMagazinesCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ParticipationInMagazines/{participationInMagazineId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Update")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> UpdateFacultyMemberParticipationInMagazineAsync(
            int participationInMagazineId,
            [FromBody] ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto)
            => Ok(await _serviceManager.ParticipationInMagazinesManagementService
                .UpdateFacultyMemberParticipationInMagazineAsync(participationInMagazineId, participationInMagazinesUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ParticipationInMagazines/{participationInMagazineId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberParticipationInMagazineAsync(int participationInMagazineId)
        {
            await _serviceManager.ParticipationInMagazinesManagementService
                .DeleteFacultyMemberParticipationInMagazineAsync(participationInMagazineId);

            return NoContent();
        }

        #endregion

        #region Projects

        [ProducesResponseType(typeof(PaginatedResult<ProjectsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/Projects")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<PaginatedResult<ProjectsResponseDto>>> GetFacultyMemberProjectsAsync(
            [FromQuery] ProjectsSpecifcationsParameters parameters)
            => Ok(await _serviceManager.ProjectsManagementService
                .GetFacultyMemberProjectsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/Projects/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<ProjectsResponseDto>> GetFacultyMemberProjectByIdAsync(int id)
            => Ok(await _serviceManager.ProjectsManagementService
                .GetFacultyMemberProjectByIdAsync(id));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/Projects")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Create")]
        public async Task<ActionResult<ProjectsResponseDto>> CreateFacultyMemberProjectAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ProjectCreateDto projectCreateDto)
            => Ok(await _serviceManager.ProjectsManagementService
                .CreateFacultyMemberProjectAsync(projectCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/Projects/{projectId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Update")]
        public async Task<ActionResult<ProjectsResponseDto>> UpdateFacultyMemberProjectAsync(
            int projectId,
            [FromBody] ProjectUpdateDto projectUpdateDto)
            => Ok(await _serviceManager.ProjectsManagementService
                .UpdateFacultyMemberProjectAsync(projectId, projectUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/Projects/{projectId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberProjectAsync(int projectId)
        {
            await _serviceManager.ProjectsManagementService
                .DeleteFacultyMemberProjectAsync(projectId);

            return NoContent();
        }

        #endregion

        #region ReviewingArticles

        [ProducesResponseType(typeof(PaginatedResult<ReviewingArticlesDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ReviewingArticles")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<PaginatedResult<ReviewingArticlesDto>>> GetFacultyMemberReviewingArticlesAsync(
     [FromQuery] ReviewingArticlesSpecificationsParameters parameters)
     => Ok(await _serviceManager.ReviewingArticlesManagementService
         .GetFacultyMemberReviewingArticlesAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/ReviewingArticles/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Read")]
        public async Task<ActionResult<ReviewingArticlesDto>> GetFacultyMemberReviewingArticleByIdAsync(int id)
            => Ok(await _serviceManager.ReviewingArticlesManagementService
                .GetFacultyMemberReviewingArticleByIdAsync(id));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/ReviewingArticles")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Create")]
        public async Task<ActionResult<ReviewingArticlesDto>> CreateFacultyMemberReviewingArticleAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] ReviewingArticleCreateDto reviewingArticleCreateDto)
            => Ok(await _serviceManager.ReviewingArticlesManagementService
                .CreateFacultyMemberReviewingArticleAsync(reviewingArticleCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/ReviewingArticles/{reviewingArticleId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Update")]
        public async Task<ActionResult<ReviewingArticlesDto>> UpdateFacultyMemberReviewingArticleAsync(
            int reviewingArticleId,
            [FromBody] ReviewArticleUpdateDto reviewingArticleUpdateDto)
            => Ok(await _serviceManager.ReviewingArticlesManagementService
                .UpdateFacultyMemberReviewingArticleAsync(reviewingArticleId, reviewingArticleUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/ReviewingArticles/{reviewingArticleId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndCommitteesData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberReviewingArticleAsync(int reviewingArticleId)
        {
            await _serviceManager.ReviewingArticlesManagementService
                .DeleteFacultyMemberReviewingArticleAsync(reviewingArticleId);

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
            => Ok(await _serviceManager.AcademicQualificationsManagementService
                .GetFacultyMemberAcademicQualificationsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/AcademicQualifications/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> GetFacultyMemberAcademicQualificationByIdAsync(int id)
            => Ok(await _serviceManager.AcademicQualificationsManagementService
                .GetFacultyMemberAcademicQualificationByIdAsync(id));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/AcademicQualifications")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Create")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> CreateFacultyMemberAcademicQualificationAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] AcademicQualificationCreateDto academicQualificationCreateDto)
            => Ok(await _serviceManager.AcademicQualificationsManagementService
                .CreateFacultyMemberAcademicQualificationAsync(academicQualificationCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/AcademicQualifications/{academicQualificationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Update")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> UpdateFacultyMemberAcademicQualificationAsync(
            int academicQualificationId,
            [FromBody] AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
            => Ok(await _serviceManager.AcademicQualificationsManagementService
                .UpdateFacultyMemberAcademicQualificationAsync(academicQualificationId, academicQualificationsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/AcademicQualifications/{academicQualificationId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberAcademicQualificationAsync(int academicQualificationId)
        {
            await _serviceManager.AcademicQualificationsManagementService
                .DeleteFacultyMemberAcademicQualificationAsync(academicQualificationId);

            return NoContent();
        }

        #endregion

        #region AdminstrtivePostions


        [ProducesResponseType(typeof(PaginatedResult<AdministrativePositionDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/AdministrativePositions")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<PaginatedResult<AdministrativePositionDto>>> GetFacultyMemberAdministrativePositionsAsync(
     [FromQuery] AdministrativePositionsSpecificationParameters parameters)
     => Ok(await _serviceManager.AdministrativePositionsManagementService
         .GetFacultyMemberAdministrativePositionsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/AdministrativePositions/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<AdministrativePositionDto>> GetFacultyMemberAdministrativePositionByIdAsync(int id)
            => Ok(await _serviceManager.AdministrativePositionsManagementService
                .GetFacultyMemberAdministrativePositionByIdAsync(id));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/AdministrativePositions")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Create")]
        public async Task<ActionResult<AdministrativePositionDto>> CreateFacultyMemberAdministrativePositionAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] AdministrativePositionCreateDto administrativePositionCreateDto)
            => Ok(await _serviceManager.AdministrativePositionsManagementService
                .CreateFacultyMemberAdministrativePositionAsync(administrativePositionCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/AdministrativePositions/{administrativePositionId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Update")]
        public async Task<ActionResult<AdministrativePositionDto>> UpdateFacultyMemberAdministrativePositionAsync(
            int administrativePositionId,
            [FromBody] AdministrativePositionDto administrativePositionUpdateDto)
            => Ok(await _serviceManager.AdministrativePositionsManagementService
                .UpdateFacultyMemberAdministrativePositionAsync(administrativePositionId, administrativePositionUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/AdministrativePositions/{administrativePositionId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberAdministrativePositionAsync(int administrativePositionId)
        {
            await _serviceManager.AdministrativePositionsManagementService
                .DeleteFacultyMemberAdministrativePositionAsync(administrativePositionId);

            return NoContent();
        }

        #endregion

        #region JobRanks

        [ProducesResponseType(typeof(PaginatedResult<JobRankResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/JobRanks")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<PaginatedResult<JobRankResponseDto>>> GetFacultyMemberJobRanksAsync(
       [FromQuery] JobRanksSpecificationsParameters parameters)
       => Ok(await _serviceManager.JobRanksManagementService
           .GetFacultyMemberJobRanksAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/JobRanks/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Read")]
        public async Task<ActionResult<JobRankResponseDto>> GetFacultyMemberJobRankByIdAsync(int id)
            => Ok(await _serviceManager.JobRanksManagementService
                .GetFacultyMemberJobRankByIdAsync(id));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/JobRanks")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Create")]
        public async Task<ActionResult<JobRankResponseDto>> CreateFacultyMemberJobRankAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] JobRankCreateDto jobRanksCreateDto)
            => Ok(await _serviceManager.JobRanksManagementService
                .CreateFacultyMemberJobRankAsync(jobRanksCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/JobRanks/{jobRankId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Update")]
        public async Task<ActionResult<JobRankResponseDto>> UpdateFacultyMemberJobRankAsync(
            int jobRankId,
            [FromBody] JobRankUpdateDto jobRanksUpdateDto)
            => Ok(await _serviceManager.JobRanksManagementService
                .UpdateFacultyMemberJobRankAsync(jobRankId, jobRanksUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/JobRanks/{jobRankId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberScientificProgressionData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberJobRankAsync(int jobRankId)
        {
            await _serviceManager.JobRanksManagementService
                .DeleteFacultyMemberJobRankAsync(jobRankId);

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
    => Ok(await _serviceManager.PatentsManagementService
        .GetFacultyMemberPatentsAsync(parameters, parameters.FacultyMemberEmail));

        [ProducesResponseType(typeof(PatentsResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("FacultyMember/Patents/{id:int}")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        public async Task<ActionResult<PatentsResponseDTO>> GetFacultyMemberPatentByIdAsync(int id)
            => Ok(await _serviceManager.PatentsManagementService
                .GetFacultyMemberPatentByIdAsync(id));

        [ProducesResponseType(typeof(PatentsResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("FacultyMember/Patents")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Create")]
        public async Task<ActionResult<PatentsResponseDTO>> CreateFacultyMemberPatentAsync(
            [FromQuery] string facultyMemberEmail,
            [FromBody] PatentsCreateDTO patentCreateDto)
            => Ok(await _serviceManager.PatentsManagementService
                .CreateFacultyMemberPatentAsync(patentCreateDto, facultyMemberEmail));

        [ProducesResponseType(typeof(PatentsResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("FacultyMember/Patents/{patentId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Update")]
        public async Task<ActionResult<PatentsResponseDTO>> UpdateFacultyMemberPatentAsync(
            int patentId,
            [FromBody] PatentsUpdateDTO patentUpdateDto)
            => Ok(await _serviceManager.PatentsManagementService
                .UpdateFacultyMemberPatentAsync(patentId, patentUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("FacultyMember/Patents/{patentId:int}")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Delete")]
        public async Task<IActionResult> DeleteFacultyMemberPatentAsync(int patentId)
        {
            await _serviceManager.PatentsManagementService
                .DeleteFacultyMemberPatentAsync(patentId);

            return NoContent();
        }

        #endregion


        #endregion

    }
}
