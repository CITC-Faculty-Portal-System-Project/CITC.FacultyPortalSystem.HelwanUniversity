using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.AdminModule
{
    public class FacutlyMemberAdministrativePositionsManagementService(IAdministrativePositionsHelper _helper)
        :IFacutlyMemberAdministrativePositionsManagementService
    {

        public Task<PaginatedResult<AdministrativePositionDto>> GetFacultyMemberAdministrativePositionsAsync(
            AdministrativePositionsSpecificationParameters parameters,
            string facultyMemberEmail)
            => _helper.GetAllAdministrativePositionsAsync(parameters, facultyMemberEmail);

        public Task<AdministrativePositionDto> GetFacultyMemberAdministrativePositionByIdAsync(int id)
            => _helper.GetAdministrativePositionByIdAsync(id);

        public Task<AdministrativePositionDto> CreateFacultyMemberAdministrativePositionAsync(
            AdministrativePositionCreateDto administrativePositionCreateDto,
            string facultyMemberEmail)
            => _helper.CreateAdministrativePositionAsync(administrativePositionCreateDto, facultyMemberEmail);

        public Task<AdministrativePositionDto> UpdateFacultyMemberAdministrativePositionAsync(
            int administrativePositionId,
            AdministrativePositionDto administrativePositionUpdateDto)
            => _helper.UpdateAdministrativePositionAsync(administrativePositionId, administrativePositionUpdateDto);

        public Task DeleteFacultyMemberAdministrativePositionAsync(int administrativePositionId)
            => _helper.DeleteAdministrativePositionAsync(administrativePositionId);
    }
}
