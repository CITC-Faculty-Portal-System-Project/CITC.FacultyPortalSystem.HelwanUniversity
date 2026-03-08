using Microsoft.AspNetCore.Authorization;
using Shared.Dtos.FacultyMemberDataModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class ProfileDashboardController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(ProfileDashboardResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Profile")]
        public async Task<ActionResult<ProfileDashboardResponseDTO>> GetProfileDashboardAsync()
            => Ok(await _serviceManager.ProfileDashboardService.GetProfileDashboardAsync());

        [ProducesResponseType(typeof(SkillsDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateSkills")]
        public async Task<ActionResult<SkillsDTO>> UpdateSkillAsync(SkillsDTO skills)
            => Ok(await _serviceManager.ProfileDashboardService.UpdateSkillAsync(skills));

        [ProducesResponseType(typeof(BioSummaryDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateBioSummary")]
        public async Task<ActionResult<BioSummaryDTO>> UpdateBioSummaryAsync(BioSummaryDTO bioSummary)
            => Ok(await _serviceManager.ProfileDashboardService.UpdateBioSummaryAsync(bioSummary));
    }
}
