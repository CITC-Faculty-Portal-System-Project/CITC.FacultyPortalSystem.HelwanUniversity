using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.PrizesModule
{
    public interface IManifestationsOfScientificAppreciationService
    {
        public Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetAllManifestationsOfScientificAppreciationAsync(ManifestationsOfScientificAppreciationSpecificationParameters parameters);
        public Task<ManifestationsOfScientificAppreciationResponseDTO> GetManifestationOfScientificAppreciationByIdAsync(int id);
        public Task<ManifestationsOfScientificAppreciationResponseDTO> CreateManifestationOfScientificAppreciationAsync(ManifestationsOfScientificAppreciationCreateDTO ManifestationsOfScientificAppreciationCreateDto);
        public Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId, ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDto);
        public Task DeleteManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId);
    }
}
