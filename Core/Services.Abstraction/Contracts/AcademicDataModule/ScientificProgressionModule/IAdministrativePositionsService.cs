using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule
{
    public interface IAdministrativePositionsService
    {
        Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(
        AdministrativePositionsSpecificationParameters parameters,
        string? facultyMemberEmail = null);

        Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<AdministrativePositionDto> CreateAdministrativePositionAsync(
            AdministrativePositionCreateDto dto,
            string? facultyMemberEmail = null);

        Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(
            int id,
            AdministrativePositionDto dto,
            string? facultyMemberEmail = null);

        Task DeleteAdministrativePositionAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
