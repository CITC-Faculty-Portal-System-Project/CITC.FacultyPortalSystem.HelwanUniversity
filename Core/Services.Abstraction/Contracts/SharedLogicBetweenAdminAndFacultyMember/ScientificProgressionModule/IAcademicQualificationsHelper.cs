using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule
{
    public interface IAcademicQualificationsHelper
    {
        Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(
      AcademicQualificationsSpecificationParamters parameters,
      string facultyMemberEmail);

        Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(int id);

        Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(
            AcademicQualificationCreateDto academicQualificationCreateDto,
            string facultyMemberEmail);

        Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(
            int academicQualificationId,
            AcademicQualificationsUpdateDto academicQualificationsUpdateDto);

        Task DeleteAcademicQualificationAsync(int academicQualificationId);

    }
}
