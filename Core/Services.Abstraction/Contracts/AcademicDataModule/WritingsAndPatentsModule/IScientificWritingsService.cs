using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule
{
    public interface IScientificWritingsService
    {
        Task<PaginatedResult<ScientificWritingsResponseDTO>> GetAllScientificWritingsAsync(
         ScientificWritingsSpecificationParameters parameters,
         string? facultyMemberEmail = null);

        Task<ScientificWritingsResponseDTO> GetScientificWritingByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ScientificWritingsResponseDTO> CreateScientificWritingAsync(
            ScientificWritingsCreateDTO scientificWritingCreateDto,
            string? facultyMemberEmail = null);

        Task<ScientificWritingsResponseDTO> UpdateScientificWritingAsync(
            int scientificWritingId,
            ScientificWritingsUpdateDTO scientificWritingUpdateDto,
            string? facultyMemberEmail = null);

        Task DeleteScientificWritingAsync(
            int scientificWritingId,
            string? facultyMemberEmail = null);
    }
}
