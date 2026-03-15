using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.TicketingModule;
using Shared.Enums.TicketingModule;
using Shared.SpecificationParameters.TicketingModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers.TicketingModule
{
    [Authorize]
    public class TicketingController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(TicketResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("Ticket")]
        public async Task<ActionResult<TicketResponseDTO>> CreateTicketAsync
            (TicketCreateDTO ticket)
            => Ok(await _serviceManager.TicketingService.CreateTicketAsync(ticket));

        
        [ProducesResponseType(typeof(PaginatedResult<TicketResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Ticket/FacultyMember")]
        public async Task<ActionResult<PaginatedResult<TicketResponseDTO>>> GetAllMemberTicketsAsync(
            [FromQuery] TicketSepcificationParameters parameters)
            => Ok(await _serviceManager.TicketingService.GetAllMemberTicketsAsync(parameters));

        
        [ProducesResponseType(typeof(TicketResponseDTO) , StatusCodes.Status200OK)]
        [HttpPut("Ticket/Revoke/{ticketId:int}")]
        public async Task<ActionResult> RevokeTicketAsync(int ticketId)
            => Ok(await _serviceManager.TicketingService.RevokeTicketAsync(ticketId));
        
        [ProducesResponseType(typeof(TicketResponseDTO) , StatusCodes.Status200OK)]
        [HttpPut("Ticket/Reopen/{ticketId:int}")]
        public async Task<ActionResult> ReopenTicketAsync(int ticketId)
            =>Ok(await _serviceManager.TicketingService.ReopenTicketAsync(ticketId));


        [Authorize(Policy = "Permission:Tickets.Assign")]
        [ProducesResponseType(typeof(IEnumerable<UserShowForAdminResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Ticket/SuitableSupportAdmins")]
        public async Task<ActionResult<IEnumerable<UserShowForAdminResponseDTO>>> GetAllSuitableAdminsForTicketAsync(
            TicketType type)
            => Ok(await _serviceManager.TicketingService.GetAllSuitableAdminsForTicketAsync(type));


        [Authorize(Policy = "Permission:Tickets.Assign")]
        [ProducesResponseType(typeof(TicketResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Ticket/Assign/{ticketId:int}")]
        public async Task<ActionResult<TicketResponseDTO>> AssignTicketToSupportAdminAsync(
            int ticketId,
            [FromBody] TicketUpdateDTO assignment)
            => Ok(await _serviceManager.TicketingService.AssignTicketToSupportAdminAsync(ticketId, assignment));


        [Authorize(Policy = "Permission:Tickets.ViewAssigned")]
        [ProducesResponseType(typeof(PaginatedResult<TicketResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Ticket/SupportAdmin")]
        public async Task<ActionResult<PaginatedResult<TicketResponseDTO>>> GetAllSupportAdminAssignedTicketsAsync(
            [FromQuery] TicketSepcificationParameters parameters)
            => Ok(await _serviceManager.TicketingService.GetAllSupportAdminAssignedTicketsAsync(parameters));


        [Authorize(Policy = "Permission:Tickets.ChangeStatus")]
        [ProducesResponseType(typeof(TicketResponseDTO) , StatusCodes.Status200OK)]
        [HttpPut("Ticket/Resolve/{ticketId:int}")]
        public async Task<ActionResult> MarkTicketAsResolvedAsync(int ticketId)
            => Ok(await _serviceManager.TicketingService.MarkTicketAsResolvedAsync(ticketId));


        [Authorize(Policy = "Permission:Tickets.Close")]
        [ProducesResponseType(typeof(TicketResponseDTO) , StatusCodes.Status200OK)]
        [HttpPut("Ticket/Close/{ticketId:int}")]
        public async Task<ActionResult> CloseTicketAsync(int ticketId)
          => Ok(await _serviceManager.TicketingService.CloseTicketAsync(ticketId));


        [Authorize(Policy = "Permission:Tickets.Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("Ticket/{ticketId:int}")]
        public async Task<ActionResult> DeleteTicketAsync(int ticketId)
        {
            await _serviceManager.TicketingService.DeleteTicketAsync(ticketId);
            return NoContent();
        }

        [Authorize(Policy = "Permission:Tickets.ViewAll")]
        [ProducesResponseType(typeof(PaginatedResult<TicketResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Ticket")]
        public async Task<ActionResult<PaginatedResult<TicketResponseDTO>>> GetAllSystemTicketsAsync(
            [FromQuery] TicketSepcificationParameters parameters)
            => Ok(await _serviceManager.TicketingService.GetAllSystemTicketsAsync(parameters));


        [Authorize(Policy = "Permission:Tickets.ViewAll")]
        [ProducesResponseType(typeof(PaginatedResult<TicketResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Ticket/{ticketId:int}")]
        public async Task<ActionResult<PaginatedResult<TicketResponseDTO>>> GetTicketByIdAsync(
            int ticketId)
                => Ok(await _serviceManager.TicketingService.GetTicketByIdAsync(ticketId));
    }
}
