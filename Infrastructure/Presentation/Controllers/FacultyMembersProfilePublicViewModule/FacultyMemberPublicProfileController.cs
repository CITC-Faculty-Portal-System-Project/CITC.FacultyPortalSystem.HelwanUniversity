using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.FacultyMembersProfilesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.FacultyMembersProfilesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.FacultyMembersDataModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Presentation.Controllers.FacultyMembersProfilePublicViewModule
{
    [Authorize]
    public class FacultyMemberPublicProfileController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(PaginatedResult<OtherUsersPageResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Profiles")]
        public async Task<ActionResult<PaginatedResult<OtherUsersPageResponseDTO>>> GetAllFacultyMembersProfiles
                    ([FromQuery] FacultyMembersProfileSpecificationParamters parameters)
              => Ok(await _serviceManager.FacultyMemberPublicProfileService
                  .GetAllFacultyMembersProfiles(parameters));



        [ProducesResponseType(typeof(FacultyMemberPublicProfileResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Profile/{id}")]
        public async Task<ActionResult<FacultyMemberPublicProfileResponseDTO>> GetFacultyMemberProfile
            (Guid id)
      => Ok(await _serviceManager.FacultyMemberPublicProfileService
          .GetFacultyMemberPublicProfile(id));
    }
}
