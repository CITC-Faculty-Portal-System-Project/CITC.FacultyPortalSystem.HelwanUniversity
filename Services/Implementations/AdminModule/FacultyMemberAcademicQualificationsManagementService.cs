using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberAcademicQualificationsManagementService(IAcademicQualificationsHelper _helper)
        : IFacultyMemberAcademicQualificationsManagementService
    {
        public Task<PaginatedResult<AcademicQualificationResponseDto>> GetFacultyMemberAcademicQualificationsAsync(
    AcademicQualificationsSpecificationParamters parameters,
    string facultyMemberEmail)
    => _helper.GetAllAcademicQualificationsAsync(parameters, facultyMemberEmail);

        public Task<AcademicQualificationResponseDto> GetFacultyMemberAcademicQualificationByIdAsync(int id)
            => _helper.GetAcademicQualificationByIdAsync(id);

        public Task<AcademicQualificationResponseDto> CreateFacultyMemberAcademicQualificationAsync(
            AcademicQualificationCreateDto academicQualificationCreateDto,
            string facultyMemberEmail)
            => _helper.CreateAcademicQualificationAsync(academicQualificationCreateDto, facultyMemberEmail);

        public Task<AcademicQualificationResponseDto> UpdateFacultyMemberAcademicQualificationAsync(
            int academicQualificationId,
            AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
            => _helper.UpdateAcademicQualificationAsync(academicQualificationId, academicQualificationsUpdateDto);

        public Task DeleteFacultyMemberAcademicQualificationAsync(int academicQualificationId)
            => _helper.DeleteAcademicQualificationAsync(academicQualificationId);
    }
}
