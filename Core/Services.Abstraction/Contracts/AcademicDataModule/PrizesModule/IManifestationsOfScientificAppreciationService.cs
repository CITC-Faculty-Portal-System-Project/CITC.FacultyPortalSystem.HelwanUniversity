using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.PrizesModule
{
    public interface IManifestationsOfScientificAppreciationService
    {
        Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetAllManifestationsOfScientificAppreciationAsync(
         ManifestationsOfScientificAppreciationSpecificationParameters parameters,
         string? facultyMemberEmail = null);

        Task<ManifestationsOfScientificAppreciationResponseDTO> GetManifestationOfScientificAppreciationByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ManifestationsOfScientificAppreciationResponseDTO> CreateManifestationOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationCreateDTO dto,
            string? facultyMemberEmail = null);

        Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateManifestationOfScientificAppreciationAsync(
            int id,
            ManifestationsOfScientificAppreciationUpdateDTO dto,
            string? facultyMemberEmail = null);

        Task DeleteManifestationOfScientificAppreciationAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
