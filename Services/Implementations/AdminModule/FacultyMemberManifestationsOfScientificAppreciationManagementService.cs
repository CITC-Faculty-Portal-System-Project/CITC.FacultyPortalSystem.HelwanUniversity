using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.PrizesModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberManifestationsOfScientificAppreciationManagementService(IManifestationsOfScientificAppreciationHelper _helper)
       :IFacultyMemberManifestationsOfScientificAppreciationManagementService
    {
        public Task<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>> GetFacultyMemberManifestationsOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationSpecificationParameters parameters,
            string facultyMemberEmail)
            => _helper.GetAllManifestationsOfScientificAppreciationAsync(parameters, facultyMemberEmail);

        public Task<ManifestationsOfScientificAppreciationResponseDTO> GetFacultyMemberManifestationOfScientificAppreciationByIdAsync(int id)
            => _helper.GetManifestationOfScientificAppreciationByIdAsync(id);

        public Task<ManifestationsOfScientificAppreciationResponseDTO> CreateFacultyMemberManifestationOfScientificAppreciationAsync(
            ManifestationsOfScientificAppreciationCreateDTO manifestationsOfScientificAppreciationCreateDto,
            string facultyMemberEmail)
            => _helper.CreateManifestationOfScientificAppreciationAsync(
                manifestationsOfScientificAppreciationCreateDto,
                facultyMemberEmail);

        public Task<ManifestationsOfScientificAppreciationResponseDTO> UpdateFacultyMemberManifestationOfScientificAppreciationAsync(
            int manifestationsOfScientificAppreciationId,
            ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDto)
            => _helper.UpdateManifestationOfScientificAppreciationAsync(
                manifestationsOfScientificAppreciationId,
                manifestationsOfScientificAppreciationUpdateDto);

        public Task DeleteFacultyMemberManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId)
            => _helper.DeleteManifestationOfScientificAppreciationAsync(manifestationsOfScientificAppreciationId);
    }
}
