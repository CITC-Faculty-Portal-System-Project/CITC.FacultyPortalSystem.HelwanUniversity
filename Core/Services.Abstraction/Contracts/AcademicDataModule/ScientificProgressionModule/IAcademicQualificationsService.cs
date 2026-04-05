using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule
{
    public interface IAcademicQualificationsService
    {
        Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(
         AcademicQualificationsSpecificationParamters parameters,
         string? facultyMemberEmail = null);

        Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(
            AcademicQualificationCreateDto dto,
            string? facultyMemberEmail = null);

        Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(
            int id,
            AcademicQualificationsUpdateDto dto,
            string? facultyMemberEmail = null);

        Task DeleteAcademicQualificationAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
