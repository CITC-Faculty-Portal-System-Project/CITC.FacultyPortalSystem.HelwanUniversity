using Shared.Dtos.IdentityModule;
using Shared.Dtos.TicketingModule;
using Shared.Enums.TicketingModule;
using Shared.SpecificationParameters.TicketingModule;

namespace Services.Abstraction.Contracts.TicketingModule
{
    public interface ITicketingService
    {
        public Task<TicketResponseDTO> CreateTicketAsync(TicketCreateDTO ticket);
        public Task<PaginatedResult<TicketResponseDTO>> GetAllSystemTicketsAsync(TicketSepcificationParameters parameters);
        public Task<TicketResponseDTO> AssignTicketToSupportAdminAsync(int ticketId , TicketUpdateDTO assignment);
        public Task<TicketResponseDTO> GetTicketByIdAsync(int ticketId);
        public Task<IEnumerable<UserShowForAdminResponseDTO>> GetAllSuitableAdminsForTicketAsync(TicketType type);
        public Task<PaginatedResult<TicketResponseDTO>> GetAllMemberTicketsAsync(TicketSepcificationParameters parameters);
        public Task<PaginatedResult<TicketResponseDTO>> GetAllSupportAdminAssignedTicketsAsync(TicketSepcificationParameters parameters);
        public Task<TicketResponseDTO> RevokeTicketAsync(int ticketId);
        public Task<TicketResponseDTO> MarkTicketAsResolvedAsync(int ticketId);
        public Task<TicketResponseDTO> CloseTicketAsync(int ticketId);
        public Task<TicketResponseDTO> ReopenTicketAsync(int ticketId);
        public Task DeleteTicketAsync(int ticketId);
    }
}
