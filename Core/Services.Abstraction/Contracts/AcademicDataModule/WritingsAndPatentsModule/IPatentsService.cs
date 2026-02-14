using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule
{
    public interface IPatentsService
    {
        public Task<PaginatedResult<PatentsResponseDTO>> GetAllPatentsAsync(PatentsSpecificationParameters parameters);
        public Task<PatentsResponseDTO> GetPatentByIdAsync(int id);
        public Task<PatentsResponseDTO> CreatePatentAsync(PatentsCreateDTO patentCreateDto);
        public Task<PatentsResponseDTO> UpdatePatentAsync(int patentId, PatentsUpdateDTO patentUpdateDto);
        public Task DeletePatentAsync(int patentId);
    }
}
