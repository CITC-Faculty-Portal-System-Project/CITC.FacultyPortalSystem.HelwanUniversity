using Shared.Dtos.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule
{
    public interface IAdministrativePositionsService
    {
        public Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(AdministrativePositionsSpecificationParameters parameters);
        public Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(int id);
        public Task<AdministrativePositionDto> CreateAdministrativePositionAsync(AdministrativePositionCreateDto administrativePositionCreateDto);
        public Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(int administrativePositionId, AdministrativePositionDto administrativePositionUpdateDto);
        public Task DeleteAdministrativePositionAsync(int administrativePositionId);
    }
}
