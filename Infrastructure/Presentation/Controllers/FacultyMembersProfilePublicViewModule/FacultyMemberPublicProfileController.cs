using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Dtos.FacultyMembersProfilesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.FacultyMembersProfilesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.FacultyMembersDataModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Presentation.Controllers.FacultyMembersProfilePublicViewModule
{
    [Authorize]
    public class FacultyMemberPublicProfileController(IServiceManager _serviceManager) : ApiController
    {
        
        
        [ProducesResponseType(typeof(CursorPaginatedResult<OtherUsersPageResponseDTO, Guid>), StatusCodes.Status200OK)]
        [HttpGet("Profiles")]
        public async Task<ActionResult<CursorPaginatedResult<OtherUsersPageResponseDTO, Guid>>> GetAllFacultyMembersProfiles
                    ([FromQuery] FacultyMembersProfileSpecificationParamters parameters)
              => Ok(await _serviceManager.FacultyMemberPublicProfileService
                  .GetAllFacultyMembersProfiles(parameters));


        [ProducesResponseType(typeof(IEnumerable<OtherUsersPageResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Profiles/Search")]
        public async Task<ActionResult<IEnumerable<OtherUsersPageResponseDTO>>> SearchFacultyMembersProfiles
                    ([FromQuery] BaseFacultyMemberProfileSpecificationParamters parameters)
              => Ok(await _serviceManager.FacultyMemberPublicProfileService
                  .SearchMemberPublicProfile(parameters));



        [ProducesResponseType(typeof(FacultyMemberPublicProfileResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Profile/{facultyMemberId}")]
        public async Task<ActionResult<FacultyMemberPublicProfileResponseDTO>> GetFacultyMemberProfile
            (Guid facultyMemberId)
      => Ok(await _serviceManager.FacultyMemberPublicProfileService
          .GetFacultyMemberPublicProfile(facultyMemberId));



        [ProducesResponseType(typeof(PaginatedResult<ResearchResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Public/Researches/{facultyMemberId}")]
        public async Task<ActionResult<PaginatedResult<ResearchResponseDTO>>> GetFacultyMemberResearches
                ([FromQuery] ResearchSpecificationParameters parameters , Guid facultyMemberId)
            => Ok(await _serviceManager.ResearchesService
              .GetAllResearches(parameters, facultyMemberId));


        [ProducesResponseType(typeof(PaginatedResult<ScientificMissionResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("Public/Missions")]
        public async Task<ActionResult<PaginatedResult<ScientificMissionResponseDto>>> GetFacultyMemberMissions
        ([FromQuery] ScientificMissionSpecificationParamaters parameters, string facultyMemberEmail)
            => Ok(await _serviceManager.ScientificMissionsService
              .GetAllScientificMissionsAsync(parameters, facultyMemberEmail));



        [ProducesResponseType(typeof(PaginatedResult<GeneralExperiencesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Public/GeneralExpereinces")]
        public async Task<ActionResult<PaginatedResult<GeneralExperiencesResponseDTO>>> GetFacultyMemberGeneralExpereinces
        ([FromQuery] GeneralExperiencesSpecificationParameters parameters, string facultyMemberEmail)
            => Ok(await _serviceManager.GeneralExperiencesService
              .GetAllGeneralExperiencesAsync(parameters, facultyMemberEmail));



        [ProducesResponseType(typeof(PaginatedResult<TeachingExperiencesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Public/TeachingExpereinces")]
        public async Task<ActionResult<PaginatedResult<TeachingExperiencesResponseDTO>>> GetFacultyMemberTeachingExpereinces
                ([FromQuery] TeachingExperiencesSpecificationParameters parameters, string facultyMemberEmail)
                    => Ok(await _serviceManager.TeachingExperiencesService
                      .GetAllTeachingExperiencesAsync(parameters, facultyMemberEmail));
    }
}
