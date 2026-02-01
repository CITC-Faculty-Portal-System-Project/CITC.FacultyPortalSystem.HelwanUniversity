using Shared.Dtos.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule
{
    public interface IAcademicQualificationsService
    {
        public Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(AcademicQualificationsSpecificationParamters parameters);
        public Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(int id);
        public Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(AcademicQualificationCreateDto academicQualificationCreateDto);
        public Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(int academicQualificationId, AcademicQualificationsUpdateDto academicQualificationsUpdateDto);
        public Task DeleteAcademicQualificationAsync(int academicQualificationId);
    }
}
