using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberManifestationsOfScientificAppreciationManagementService
    {
        Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetFacultyMemberManifestationsOfScientificAppreciationAsync(
          ManifestationsOfScientificAppreciationSpecificationParameters parameters,
          string facultyMemberEmail);

        Task<ManifestationsOfScientificAppreciationResponseDTO> GetFacultyMemberManifestationOfScientificAppreciationByIdAsync(int id);

        Task<ManifestationsOfScientificAppreciationResponseDTO> CreateFacultyMemberManifestationOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationCreateDTO manifestationsOfScientificAppreciationCreateDto,
            string facultyMemberEmail);

        Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateFacultyMemberManifestationOfScientificAppreciationAsync(
            int manifestationsOfScientificAppreciationId,
            ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDto);

        Task DeleteFacultyMemberManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId);
    }
}
