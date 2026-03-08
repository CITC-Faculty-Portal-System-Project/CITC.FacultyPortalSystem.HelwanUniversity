using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule
{
    public interface IAdministrativePositionsHelper
    {
        Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(
         AdministrativePositionsSpecificationParameters parameters,
         string facultyMemberEmail);

        Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(int id);

        Task<AdministrativePositionDto> CreateAdministrativePositionAsync(
            AdministrativePositionCreateDto administrativePositionCreateDto,
            string facultyMemberEmail);

        Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(
            int administrativePositionId,
            AdministrativePositionDto administrativePositionUpdateDto);

        Task DeleteAdministrativePositionAsync(int administrativePositionId);
    }
}
