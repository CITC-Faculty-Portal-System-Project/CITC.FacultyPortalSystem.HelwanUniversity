using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberSeminarsAndConferencesManagementService
    {
        Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetFacultyMemberSeminarsAndConferencesAsync(
           SeminarsAndConferncesSpecificationParameters parameters,
           string facultyMemberEmail);

        Task<ConferencesAndSeminarsResponseDto> GetFacultyMemberSeminarOrConferenceByIdAsync(int id);

        Task<ConferencesAndSeminarsResponseDto> CreateFacultyMemberSeminarOrConferenceAsync(
            ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto,
            string facultyMemberEmail);

        Task<ConferencesAndSeminarsResponseDto> UpdateFacultyMemberSeminarOrConferenceAsync(
            int id,
            ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto);

        Task DeleteFacultyMemberSeminarOrConferenceAsync(int id);
    }
}
