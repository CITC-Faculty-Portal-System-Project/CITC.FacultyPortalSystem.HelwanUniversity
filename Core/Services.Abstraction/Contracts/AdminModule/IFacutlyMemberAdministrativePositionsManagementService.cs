using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacutlyMemberAdministrativePositionsManagementService
    {
        Task<PaginatedResult<AdministrativePositionDto>> GetFacultyMemberAdministrativePositionsAsync(
    AdministrativePositionsSpecificationParameters parameters,
    string facultyMemberEmail);

        Task<AdministrativePositionDto> GetFacultyMemberAdministrativePositionByIdAsync(int id);

        Task<AdministrativePositionDto> CreateFacultyMemberAdministrativePositionAsync(
            AdministrativePositionCreateDto administrativePositionCreateDto,
            string facultyMemberEmail);

        Task<AdministrativePositionDto> UpdateFacultyMemberAdministrativePositionAsync(
            int administrativePositionId,
            AdministrativePositionDto administrativePositionUpdateDto);

        Task DeleteFacultyMemberAdministrativePositionAsync(int administrativePositionId);
    }
}
