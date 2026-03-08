using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.PrizesModule
{
    public interface IManifestationsOfScientificAppreciationHelper
    {
        Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetAllManifestationsOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationSpecificationParameters parameters,
            string facultyMemberEmail);

        Task<ManifestationsOfScientificAppreciationResponseDTO> GetManifestationOfScientificAppreciationByIdAsync(int id);

        Task<ManifestationsOfScientificAppreciationResponseDTO> CreateManifestationOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationCreateDTO manifestationsOfScientificAppreciationCreateDto,
            string facultyMemberEmail);

        Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateManifestationOfScientificAppreciationAsync(
            int manifestationsOfScientificAppreciationId,
            ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDto);

        Task DeleteManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId);
    }
}
