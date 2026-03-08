using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberAcademicQualificationsManagementService
    {
        Task<PaginatedResult<AcademicQualificationResponseDto>> GetFacultyMemberAcademicQualificationsAsync(
    AcademicQualificationsSpecificationParamters parameters,
    string facultyMemberEmail);

        Task<AcademicQualificationResponseDto> GetFacultyMemberAcademicQualificationByIdAsync(int id);

        Task<AcademicQualificationResponseDto> CreateFacultyMemberAcademicQualificationAsync(
            AcademicQualificationCreateDto academicQualificationCreateDto,
            string facultyMemberEmail);

        Task<AcademicQualificationResponseDto> UpdateFacultyMemberAcademicQualificationAsync(
            int academicQualificationId,
            AcademicQualificationsUpdateDto academicQualificationsUpdateDto);

        Task DeleteFacultyMemberAcademicQualificationAsync(int academicQualificationId);
    }
}
