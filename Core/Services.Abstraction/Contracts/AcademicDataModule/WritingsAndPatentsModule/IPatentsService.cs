using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule
{
    public interface IPatentsService
    {
        Task<PaginatedResult<PatentsResponseDTO>> GetAllPatentsAsync(
            PatentsSpecificationParameters parameters,
            string? facultyMemberEmail = null);

        Task<PatentsResponseDTO> GetPatentByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<PatentsResponseDTO> CreatePatentAsync(
            PatentsCreateDTO patentCreateDto,
            string? facultyMemberEmail = null);

        Task<PatentsResponseDTO> UpdatePatentAsync(
            int patentId,
            PatentsUpdateDTO patentUpdateDto,
            string? facultyMemberEmail = null);

        Task DeletePatentAsync(
            int patentId,
            string? facultyMemberEmail = null);
    }
}
