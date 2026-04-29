using Shared.Dtos;
namespace Services.Abstraction.Contracts
{
    public interface ILookUpItemService
    {
        public Task<IEnumerable<LookupItemDto>> GetLookUpItemByType(string type);
        public Task<IEnumerable<FacultyResponseDTO>> GetAllFacultiesAsync();

    }
}
