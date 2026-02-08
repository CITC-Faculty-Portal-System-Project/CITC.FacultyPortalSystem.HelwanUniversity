using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule
{
    public interface IScientificWritingsService
    {
        public Task<PaginatedResult<ScientificWritingsResponseDTO>> GetAllScientificWritingsAsync(ScientificWritingsSpecificationParameters parameters);
        public Task<ScientificWritingsResponseDTO> GetScientificWritingByIdAsync(int id);
        public Task<ScientificWritingsResponseDTO> CreateScientificWritingAsync(ScientificWritingsCreateDTO scientificWritingCreateDto);
        public Task<ScientificWritingsResponseDTO> UpdateScientificWritingAsync(int scientificWritingId, ScientificWritingsUpdateDTO scientificWritingUpdateDto);
        public Task DeleteScientificWritingAsync(int scientificWritingId);
    }
}
